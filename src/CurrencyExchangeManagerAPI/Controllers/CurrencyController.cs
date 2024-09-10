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
        public IEnumerable<Currency> Get()
        {
            return _currencyRepository.GetAll();
        }

        // GET api/<CurrencyController>/5
        [HttpGet("{currency_code}")]
        public Currency Get(string currency_code)
        {
            return _currencyRepository.GetByName(currency_code);
        }
    }
}
