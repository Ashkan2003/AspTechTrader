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
        private readonly IUserService _userService;

        public UserSymbolPropertyController(IUserSymbolPropertyService userSymbolPropertyService, IUserService userService)
        {

            _userSymbolPropertyService = userSymbolPropertyService;
            _userService = userService;
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

        [HttpPut("SaleSymbol")]
        public async Task<ActionResult> SaleSymbol(SymbolSaleRequestDTO symbolSaleRequestDTO)
        {
            if (symbolSaleRequestDTO == null)
            {
                return BadRequest("symbolSaleRequestDTO not supplied");
            }
            // validation
            if (ModelState.IsValid == false)
            {
                string errorMessage = string.Join(" | ",
                    ModelState.Values
                    .SelectMany(v => v.Errors)
                    .Select(e => e.ErrorMessage));

                return Problem(errorMessage);
            }

            bool isSuccess = await _userSymbolPropertyService.SaleSymbol(symbolSaleRequestDTO);

            if (isSuccess == false)
            {
                return Problem("cant sale symbol");
            }
            else
            {
                User? user = await _userService.GetUserById(symbolSaleRequestDTO.UserId);
                int userNewProperty = user.UserProperty + (symbolSaleRequestDTO.SymbolSalePrice * symbolSaleRequestDTO.SymbolSaleQuantity);

                bool isSuccessUpdateUserProperty = await _userService.UpdateUserProperty(new UserPropertyUpdateRequestDTO()
                {
                    UserId = symbolSaleRequestDTO.UserId,
                    UserProperty = userNewProperty
                });

                return Ok(isSuccessUpdateUserProperty);
            }


        }


    }
}
