﻿using AspTechTrader.Core.Domain.Entities;
using AspTechTrader.Core.DTO;
using AspTechTrader.Core.ServiceContracts;
using AspTechTrader.Infrastructure.AppDbContext;
using Microsoft.AspNetCore.Mvc;
using Microsoft.EntityFrameworkCore;

namespace AspTechTrader.Api.Controllers
{
    [Route("api/[controller]")]
    [ApiController]
    public class UserWatchListsController : ControllerBase
    {
        private readonly ApplicationDbContext _db;

        private readonly IUserWatchListsService _userWatchListsService;

        public UserWatchListsController(IUserWatchListsService userWatchListsService, ApplicationDbContext db)
        {
            _userWatchListsService = userWatchListsService;
            _db = db;
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

        [HttpPost("AddNewWatchList")]
        public async Task<ActionResult> Post(UserWatchListAddRequestDTO userWatchListAddRequest)
        {
            if (userWatchListAddRequest == null)
            {
                return BadRequest("UserWatchListAddRequest was not supplied");
            }
            if (userWatchListAddRequest.UserId == Guid.Empty)
            {
                BadRequest("UserId was not supplied");
            }
            User? user = await _db.Users
                              .Include(u => u.UserWatchLists)
                              .FirstOrDefaultAsync(u => u.UserId == userWatchListAddRequest.UserId);

            user.UserWatchLists.Add(new UserWatchList()
            {
                userWatchListName = userWatchListAddRequest.userWatchListName,

                UserId = user.UserId,
            });

            await _db.SaveChangesAsync();

            return Ok(user);
        }

    }
}
