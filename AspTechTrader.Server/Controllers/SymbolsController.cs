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
        public async Task<ActionResult<List<Symbol>>> Get()
        {
            List<Symbol> symbols = await _symbolsService.GetAllSymbols();

            return symbols;
        }

        // GET api/<SymbolsController>/5
        [HttpGet("{id}")]
        public string Get(int id)
        {
            return "value";
        }

        // POST api/<SymbolsController>
        [HttpPost]
        public void Post([FromBody] string value)
        {
        }

        // PUT api/<SymbolsController>/5
        [HttpPut("{id}")]
        public void Put(int id, [FromBody] string value)
        {
        }

        // DELETE api/<SymbolsController>/5
        [HttpDelete("{id}")]
        public void Delete(int id)
        {
        }
    }
}
