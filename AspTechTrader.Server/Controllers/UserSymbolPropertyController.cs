using AspTechTrader.Core.Domain.Entities;
using AspTechTrader.Core.DTO;
using AspTechTrader.Core.ServiceContracts;
using Microsoft.AspNetCore.Mvc;

namespace AspTechTrader.Api.Controllers
{
    [Route("api/[controller]")]
    [ApiController]
    public class UserSymbolPropertyController : ControllerBase
    {
        private readonly IUserSymbolPropertyService _userSymbolPropertyService;


        public UserSymbolPropertyController(IUserSymbolPropertyService userSymbolPropertyService)
        {

            _userSymbolPropertyService = userSymbolPropertyService;
        }


        [HttpPost("AddNewBoughtSymbol")]
        public async Task<ActionResult> AddNewBoughtSymbol(UserBoughtSymbolAddRequestDTO userBoughtSymbolAddRequest)
        {
            if (userBoughtSymbolAddRequest == null)
            {
                return BadRequest("userSymbolProperty was not supplied");
            }

            if (userBoughtSymbolAddRequest.UserId == Guid.Empty)
            {
                return BadRequest("userId was not supplied");
            }

            if (userBoughtSymbolAddRequest.SymbolId == Guid.Empty)
            {
                return BadRequest("symbolId was not supplied");
            }

            User user = await _userSymbolPropertyService.AddNewBoughtSymbol(userBoughtSymbolAddRequest);
            return Ok(User);

        }




    }
}
