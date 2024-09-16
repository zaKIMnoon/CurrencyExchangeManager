using CurrencyExchangeManagerLib.Models;
using CurrencyExchangeManagerLib.Repositories;
using Microsoft.AspNetCore.Mvc;

// For more information on enabling Web API for empty projects, visit https://go.microsoft.com/fwlink/?LinkID=397860

namespace CurrencyExchangeManagerAPI.Controllers
{
    [Route("api/[controller]")]
    [ApiController]
    public class CurrencyRateController : ControllerBase
    {

        private readonly CurrencyRateRepository _currencyRateRepository;

        public CurrencyRateController(CurrencyRateRepository currencyRateRepository)
        {
            _currencyRateRepository = currencyRateRepository;
        }

        // GET api/<CurrencyRateController>/5
        [ProducesResponseType(StatusCodes.Status200OK)]
        [ProducesResponseType(StatusCodes.Status404NotFound)]
        [HttpGet("{source_currency_name}/{target_currency_name}")]
        public async Task<IActionResult> Get(string source_currency_name, string target_currency_name)
        {
            var currencyRate = await _currencyRateRepository.GetCurrencySourceTargetRate(source_currency_name, target_currency_name).ConfigureAwait(true);

            if (currencyRate is null) 
            {
                return NotFound("Currency rate not found.");
            }

            return Ok(currencyRate);
        }
    }
}
