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
    public class SourceSystemController : ControllerBase
    {   
        private readonly SourceSystemRepository _sourceSystemRepository;
        private readonly IDistributedCache _cache;
        public SourceSystemController(SourceSystemRepository sourceSystemRepository, IDistributedCache cache)
        {
            _sourceSystemRepository = sourceSystemRepository;
            _cache = cache;
        }

        // GET: api/<SourceSystemController>
        [HttpGet]
        public async Task<IEnumerable<SourceSystem>> Get()
        {
            var cacheKey = "SourceSystemList";
            var cachedData = await _cache.GetAsync(cacheKey);

            if (cachedData != null) {
                Stream stream = new MemoryStream(cachedData);
                return await JsonSerializer.DeserializeAsync<IEnumerable<SourceSystem>>(stream).ConfigureAwait(false);
            }

            var sourceSystemList = await _sourceSystemRepository.GetAllAsync();
            var serializedData = JsonSerializer.Serialize(sourceSystemList);
            var cacheOptions = new DistributedCacheEntryOptions { AbsoluteExpirationRelativeToNow = TimeSpan.FromMinutes(15) };
            await _cache.SetStringAsync(cacheKey, serializedData,cacheOptions);

            return sourceSystemList;
        }

        // GET api/<SourceSystemController>/test
        [ProducesResponseType(StatusCodes.Status200OK)]
        [ProducesResponseType(StatusCodes.Status404NotFound)]
        [HttpGet("{source_system_name}")]
        public async Task<IActionResult> Get(string source_system_name)
        {

            var source_system = await _sourceSystemRepository.GetByNameAsync(source_system_name);
         
            if (source_system is null)
            {
                return NotFound($"{source_system_name} was not found.");
            }

            return Ok(source_system);
        }
    }
}
