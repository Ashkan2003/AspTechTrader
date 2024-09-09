using AspTechTrader.Core.Domain.Entities;
using AspTechTrader.Core.ServiceContracts;
using Microsoft.AspNetCore.Mvc;

// For more information on enabling Web API for empty projects, visit https://go.microsoft.com/fwlink/?LinkID=397860

namespace AspTechTrader.Api.Controllers
{
    [Route("api/[controller]")]
    [ApiController]
    public class SymbolsController : ControllerBase
    {

        // private fields
        private readonly ISymbolsService _symbolsService;

        //constructure
        public SymbolsController(ISymbolsService symbolsService)
        {
            _symbolsService = symbolsService;
        }




        [HttpGet("getSymbols")] // https://localhost:7007/api/Symbols/getSymbols
        public async Task<ActionResult<List<Symbol>>> GetAllSymbols()
        {
            List<Symbol> symbols = await _symbolsService.GetAllSymbols();

            return symbols;
        }

        [HttpGet("getSymbolsWithRelatedUserSymbolProperty")] // https://localhost:7007/api/Symbols/getSymbols
        public async Task<ActionResult<List<Symbol>>> GetsymbolsWithRelatedUserSymbolProperty()
        {
            List<Symbol> symbolsWithRelatedUserSymbolProperty = await _symbolsService.GetAllSymbolsWithRelatedUserSymbolProperty();

            return symbolsWithRelatedUserSymbolProperty;
        }


    }
}
