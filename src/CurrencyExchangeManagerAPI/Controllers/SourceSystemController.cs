using CurrencyExchangeManagerLib.Models;
using CurrencyExchangeManagerLib.Repositories;
using Microsoft.AspNetCore.Mvc;

// For more information on enabling Web API for empty projects, visit https://go.microsoft.com/fwlink/?LinkID=397860

namespace CurrencyExchangeManagerAPI.Controllers
{
    [Route("api/[controller]")]
    [ApiController]
    public class SourceSystemController : ControllerBase
    {   
        private readonly SourceSystemRepository _sourceSystemRepository;

        public SourceSystemController(SourceSystemRepository sourceSystemRepository)
        {
            _sourceSystemRepository = sourceSystemRepository;
        }

        // GET: api/<SourceSystemController>
        [HttpGet]
        public IEnumerable<SourceSystem> Get()
        {
            return _sourceSystemRepository.GetAll();
        }

        // GET api/<SourceSystemController>/test
        [HttpGet("{source_system_name}")]
        public SourceSystem Get(string source_system_name)
        {
            return _sourceSystemRepository.GetByName(source_system_name);
        }
    }
}
