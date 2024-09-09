using AspTechTrader.Core.Domain.Entities;
using AspTechTrader.Core.DTO;
using AspTechTrader.Core.ServiceContracts;
using Microsoft.AspNetCore.Mvc;

namespace AspTechTrader.Api.Controllers
{
    [Route("api/[controller]")]
    [ApiController]
    public class UsersController : ControllerBase
    {
        private readonly IUserService _userService;
        private readonly ILogger _logger;
        public UsersController(ILogger<UsersController> logger, IUserService userService)
        {
            _logger = logger;
            _userService = userService;
        }

        [HttpGet("getUserById")] // https://localhost:7007/api/Users/getUserById
        public async Task<ActionResult<User>> Get(string userId)
        {
            _logger.LogInformation("userId = {userId}", userId);

            if (userId == null)
            {
                // return 400
                return BadRequest("the userId was not suppliyed");
            }

            // convert the userId(type string) from fronEnd to Guid
            User? user = await _userService.GetUserById(Guid.Parse(userId));

            if (user == null)
            {
                // if the user doesnt exist in db return 404-error
                return NotFound("no user exists with this userId in dataBase");
            }

            // return res 200 with the user-obj
            return Ok(user);
        }

        [HttpPost("addUser")]
        public async Task<ActionResult<User>> Post(UserAddRequestDTO userAddRequest)
        {
            User AddedUser = await _userService.AddUser(userAddRequest);
            return Ok(AddedUser);
        }

        [HttpPut("updateUser")]
        public async Task<ActionResult<User>> Put(UserUpdateRequestDTO userUpdateRequest)
        {
            if (userUpdateRequest == null)
            {
                return BadRequest("user onject doesnt supplied");
            }

            if (userUpdateRequest.UserId == Guid.Empty)
            {
                return BadRequest("userId doesnt supplied");
            }



            User UpdatedUser = await _userService.UpdateUser(userUpdateRequest);

            return Ok(UpdatedUser);

        }

        [HttpDelete("deleteUserById")]
        public async Task<ActionResult> Delete(string userId)
        {
            if (userId == null)
            {
                // return 400
                return BadRequest("the userId was not suppliyed");
            }

            bool? isDeletedSuccess = await _userService.DeleteUserById(Guid.Parse(userId));

            if (isDeletedSuccess == false)
            {

                return Problem("cant delete user");
            }

            return Ok("user deleted successfully");
        }


        //[HttpPost("AddSymbolToUserSymbolList")]
        //public async Task<ActionResult> AddSymbol(UserSymbol userSymbol)
        //{
        //    if (userSymbol == null)
        //    {
        //        return BadRequest("userSymbol object not supplied");
        //    }

        //    if (userSymbol.UserId == Guid.Empty && userSymbol.SymbolId == Guid.Empty)
        //    {
        //        return BadRequest("userId or symbolId not supplied");
        //    }

        //    User updatedUser = await _userService.AddSymbolToUserSymbolList(userSymbol);

        //    return Ok(updatedUser);

        //}
    }
}
