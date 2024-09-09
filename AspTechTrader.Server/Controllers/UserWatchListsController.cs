using AspTechTrader.Core.Domain.Entities;
using AspTechTrader.Core.ServiceContracts;
using Microsoft.AspNetCore.Mvc;

namespace AspTechTrader.Api.Controllers
{
    [Route("api/[controller]")]
    [ApiController]
    public class UserWatchListsController : ControllerBase
    {

        private readonly IUserWatchListsService _userWatchListsService;

        public UserWatchListsController(IUserWatchListsService userWatchListsService)
        {
            _userWatchListsService = userWatchListsService;
        }

        [HttpGet("GetUserWithRelatedUserWatchListById")]
        public async Task<ActionResult> Get(string userId)
        {
            if (userId == null)
            {
                BadRequest("userId was not supplied");
            }

            User? MatchedUser = await _userWatchListsService.GetUserWithRelatedUserWatchListById(Guid.Parse(userId));


            if (MatchedUser == null)
            {
                return NotFound("no related userWatchList was founded with the given userId");
            }
            return Ok(MatchedUser);

        }


    }
}
