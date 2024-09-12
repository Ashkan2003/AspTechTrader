using AspTechTrader.Core.Domain.IdentityEntities;
using AspTechTrader.Core.DTO;
using AspTechTrader.Core.ServiceContracts;
using Microsoft.AspNetCore.Authorization;
using Microsoft.AspNetCore.Identity;
using Microsoft.AspNetCore.Mvc;

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
        public AccountController(
            UserManager<ApplicationUser> userManager,
            SignInManager<ApplicationUser> signInManager,
            RoleManager<ApplicationRole> roleManager,
            IJwtService jwtService
            )
        {
            _userManager = userManager;
            _signInManager = signInManager;
            _roleManager = roleManager;
            _jwtService = jwtService;
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

                AuthenticationResponseDTO authenticationRespose = _jwtService.CreateJwtToken(user);

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

                AuthenticationResponseDTO authenticationRespose = _jwtService.CreateJwtToken(user);

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
    }
}
