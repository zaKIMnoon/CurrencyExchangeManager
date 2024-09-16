using CurrencyExchangeManagerLib.Models;
using CurrencyExchangeManagerLib.Repositories;
using CurrencyExchangeManagerLib.SystemImports;
using Microsoft.AspNetCore.Mvc;

// For more information on enabling Web API for empty projects, visit https://go.microsoft.com/fwlink/?LinkID=397860

namespace CurrencyExchangeManagerAPI.Controllers
{
    [Route("api/[controller]")]
    [ApiController]
    public class CurrencyController : ControllerBase
    {
        private readonly CurrencyRepository _currencyRepository;

        public CurrencyController(CurrencyRepository currencyRepository)
        {
            _currencyRepository = currencyRepository;
        }
        // GET: api/<CurrencyController>
        [HttpGet]
        public async Task<IEnumerable<Currency>> Get()
        {
            return await _currencyRepository.GetAllAsync();
        }

        // GET api/<CurrencyController>/5
        [HttpGet("{currency_code}")]
        [ProducesResponseType(StatusCodes.Status200OK)]
        [ProducesResponseType(StatusCodes.Status404NotFound)]
        public async Task<IActionResult> Get(string currency_code)
        {
            var currency = await _currencyRepository.GetByNameAsync(currency_code);

            if (currency is null)
            {
                return NotFound($"{currency_code} was not found.");
            }

            return Ok(currency);
        }
    }
}
