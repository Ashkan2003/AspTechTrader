using AspTechTrader.Core.Domain.Entities;
using AspTechTrader.Core.Domain.IdentityEntities;
using AspTechTrader.Core.DTO;
using AspTechTrader.Core.Enums;
using AspTechTrader.Core.ServiceContracts;
using Microsoft.AspNetCore.Authorization;
using Microsoft.AspNetCore.Identity;
using Microsoft.AspNetCore.Mvc;
using System.Security.Claims;

namespace AspTechTrader.Api.Controllers
{
    [AllowAnonymous]
    [Route("api/[controller]")]
    [ApiController]
    public class AccountController : ControllerBase
    {
        private readonly UserManager<ApplicationUser> _userManager;
        private readonly SignInManager<ApplicationUser> _signInManager;
        private readonly RoleManager<ApplicationRole> _roleManager;
        private readonly IJwtService _jwtService;
        private readonly IUserService _userService;


        public AccountController(
            UserManager<ApplicationUser> userManager,
            SignInManager<ApplicationUser> signInManager,
            RoleManager<ApplicationRole> roleManager,
            IJwtService jwtService,
            IUserService userService
            )
        {
            _userManager = userManager;
            _signInManager = signInManager;
            _roleManager = roleManager;
            _jwtService = jwtService;
            _userService = userService;
        }

        [HttpPost("Register")]
        public async Task<IActionResult> PostRegister(RegisterDTO registerDTO)
        {
            // validation
            if (ModelState.IsValid == false)
            {
                string errorMessage = string.Join(" | ",
                    ModelState.Values
                    .SelectMany(v => v.Errors)
                    .Select(e => e.ErrorMessage));

                return Problem(errorMessage);
            }

            //Create USer
            ApplicationUser user = new ApplicationUser()
            {
                Email = registerDTO.Email,
                PersonName = registerDTO.PersonName,
                PhoneNumber = registerDTO.PhoneNumber,
                UserName = registerDTO.Email
            };

            IdentityResult result = await _userManager.CreateAsync(user, registerDTO.Password);

            if (result.Succeeded)
            {
                //sign in
                await _signInManager.SignInAsync(user, isPersistent: false);


                // check for userRole
                // if userRole is "Admin"
                if (registerDTO.UserRole == UserRoleOptions.Admin)
                {
                    // create admin-role
                    if (await _roleManager.FindByNameAsync(UserRoleOptions.Admin.ToString()) is null)
                    {
                        // if the admin-row was not created in AspNetRoles-table so create it
                        ApplicationRole applicationRole = new ApplicationRole()
                        {
                            Name = UserRoleOptions.Admin.ToString()
                        };
                        await _roleManager.CreateAsync(applicationRole);
                    }
                    // Add the new user into AspNetUserRole-table
                    // with this code we are relating the current-user with current selected user-role
                    await _userManager.AddToRoleAsync(user, UserRoleOptions.Admin.ToString());
                }
                // if userRole is "user"
                else if (registerDTO.UserRole == UserRoleOptions.User)
                {
                    // create user-role
                    if (await _roleManager.FindByNameAsync(UserRoleOptions.User.ToString()) is null)
                    {
                        // if the user-role was not created in AspNetRoles-table so create it
                        ApplicationRole applicationRole = new ApplicationRole()
                        {
                            Name = UserRoleOptions.User.ToString()
                        };
                        await _roleManager.CreateAsync(applicationRole);
                    }
                    // Add the new user into AspNetUserRole-table
                    // with this code we are relating the current-user with current selected user-role
                    await _userManager.AddToRoleAsync(user, UserRoleOptions.User.ToString());
                }
                else
                {
                    return BadRequest("the user-role was not suppliyed");
                }


                //
                var roles = await _userManager.GetRolesAsync(user);

                // create a new jwt-token fo the registered user
                AuthenticationResponseDTO authenticationRespose = _jwtService.CreateJwtToken(user, roles);

                // save the newly genereted-refresh-token in the asp-user-db
                user.RefreshToken = authenticationRespose.RefreshToken;
                user.RefreshTokenExpirationDateTime = authenticationRespose.RefreshTokenExpirationDateTime;
                await _userManager.UpdateAsync(user);

                

                // create user // this is the main user of application and important
                // this user is diffrent from identity-user
                await _userService.AddUser(new UserAddRequestDTO()
                {
                    UserName = registerDTO.PersonName,
                    EmailAddress = registerDTO.Email,
                });

                return Ok(authenticationRespose);
            }
            else
            {
                // check for errors such duplicat user and return like erroe1 | error2
                string errorMessage = string.Join(" | ", result.Errors.Select(e => e.Description));
                return Problem(errorMessage);
            }

        }

        [HttpPost("Login")]
        public async Task<ActionResult<ApplicationUser>> PostLogin(LoginDTO loginDTO)
        {
            // validation
            if (ModelState.IsValid == false)
            {
                string errorMessage = string.Join(" | ",
                    ModelState.Values
                    .SelectMany(v => v.Errors)
                    .Select(e => e.ErrorMessage));

                return Problem(errorMessage);
            }


            var result = await _signInManager.PasswordSignInAsync(
                  loginDTO.Email,
                  loginDTO.Password,
                  isPersistent: false, // when true when user login and close the browser and then inter the app its automatically logged in
                  lockoutOnFailure: false  // when the user try to submit many time and fails, then lock its account // we dont whant to do that
              );

            if (result.Succeeded)
            {

                ApplicationUser? user = await _userManager.FindByEmailAsync(loginDTO.Email);

                if (user == null)
                {
                    return NoContent();
                }

                //sign in
                await _signInManager.SignInAsync(user, isPersistent: false);

                //
                var roles = await _userManager.GetRolesAsync(user);

                // create a new jwt-token fo the logedd in user
                AuthenticationResponseDTO authenticationRespose = _jwtService.CreateJwtToken(user, roles);

                // save the newly genereted-refresh-token in the asp-user-db
                user.RefreshToken = authenticationRespose.RefreshToken;
                user.RefreshTokenExpirationDateTime = authenticationRespose.RefreshTokenExpirationDateTime;
                await _userManager.UpdateAsync(user);


                return Ok(authenticationRespose);

            }
            else
            {
                return Problem("invalid email or password");
            }

        }

        [HttpGet("LogOut")]
        public async Task<ActionResult> GetLogOut()
        {
            await _signInManager.SignOutAsync();
            return NoContent();
        }

        [HttpPost("GenerateNewToken")]
        public async Task<IActionResult> GenerateNewAccessToken(GenerateNewJwtTokenRequestDTO generateNewJwtTokenRequestDTO)
        {
            if (generateNewJwtTokenRequestDTO == null)
            {
                return BadRequest("Invalid client request");

            }

            ClaimsPrincipal? principal = _jwtService.GetPrincipalFormJwtToken(generateNewJwtTokenRequestDTO.Token);

            if (principal == null)
            {
                return BadRequest("Invalid jwt-access-token");
            }

            string? email = principal.FindFirstValue(ClaimTypes.Email);

            ApplicationUser? user = await _userManager.FindByEmailAsync(email);

            if (
                user == null ||
                user.RefreshToken != generateNewJwtTokenRequestDTO.refreshToken ||
                user.RefreshTokenExpirationDateTime <= DateTime.Now
                )
            {
                return BadRequest("invalid refreshToken");
            }

            //
            var roles = await _userManager.GetRolesAsync(user);

            AuthenticationResponseDTO authenticationResponseDTO = _jwtService.CreateJwtToken(user, roles);

            user.RefreshToken = authenticationResponseDTO.RefreshToken;
            user.RefreshTokenExpirationDateTime = authenticationResponseDTO.RefreshTokenExpirationDateTime;

            await _userManager.UpdateAsync(user);

            return Ok(authenticationResponseDTO);

        }


        // we want to get the current loggendIn user by the token that is stored in his local storage
        // the use we returned is the trade-account-user
        [HttpGet("GetCurrentLoggedInUser")]
        public async Task<IActionResult> GetUser(string Token)
        {
            if (string.IsNullOrEmpty(Token))
            {
                return BadRequest("the token is null or not supplied");
            }

            ClaimsPrincipal? principal = _jwtService.GetPrincipalFormJwtToken(Token);

            if (principal == null)
            {
                return BadRequest("Invalid jwt-token");
            }

            //in the _jwtService.CreateJwtToken we pass the user email as a one of the claims, so we can get than email from decoding the jwt token
            string? email = principal.FindFirstValue(ClaimTypes.Email);

            if (email == null)
            {
                return NotFound("no email founded in the given jwt-token with email-claim");
            }

            //ApplicationUser? user = await _userManager.FindByEmailAsync(email);

            User? user = await _userService.GetUserByEmail(email);

            if (user == null)
            {
                return NotFound("no user founded with the email provided");
            }

            return Ok(user);
        }
    }
}
