using CurrencyExchangeManagerLib.Models;
using CurrencyExchangeManagerLib.Repositories;
using Microsoft.AspNetCore.Mvc;
using Microsoft.Extensions.Caching.Distributed;
using System.Text.Json;

// For more information on enabling Web API for empty projects, visit https://go.microsoft.com/fwlink/?LinkID=397860

namespace CurrencyExchangeManagerAPI.Controllers
{
    [Route("api/[controller]")]
    [ApiController]
    public class CurrencyRateController : ControllerBase
    {

        private readonly CurrencyRateRepository _currencyRateRepository;
        private readonly IDistributedCache _cache;
        public CurrencyRateController(CurrencyRateRepository currencyRateRepository, IDistributedCache cache)
        {
            _currencyRateRepository = currencyRateRepository;
            _cache = cache;
        }

        // GET api/<CurrencyRateController>/5
        [ProducesResponseType(StatusCodes.Status200OK)]
        [ProducesResponseType(StatusCodes.Status404NotFound)]
        [HttpGet("{source_currency_name}/{target_currency_name}")]
        public async Task<IActionResult> Get(string source_currency_name, string target_currency_name)
        {

            var cacheKey = $"{source_currency_name}_{target_currency_name}";
            var cachedData = await _cache.GetAsync(cacheKey);

            if (cachedData != null)
            {
                Stream stream = new MemoryStream(cachedData);
                return (IActionResult)await JsonSerializer.DeserializeAsync<IEnumerable<CurrencyRateReadOnly>>(stream).ConfigureAwait(false);
            }

            var currencyRate = await _currencyRateRepository.GetCurrencySourceTargetRate(source_currency_name, target_currency_name).ConfigureAwait(true);

            if (currencyRate is null) 
            {
                return NotFound("Currency rate not found.");
            }

            var serializedData = JsonSerializer.Serialize(currencyRate);
            var cacheOptions = new DistributedCacheEntryOptions { AbsoluteExpirationRelativeToNow = TimeSpan.FromMinutes(15) };

            await _cache.SetStringAsync(cacheKey, serializedData, cacheOptions);


            return Ok(currencyRate);
        }
    }
}
