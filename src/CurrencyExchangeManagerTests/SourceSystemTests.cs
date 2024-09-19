using NUnit.Framework;
using Moq;
using CurrencyExchangeManagerAPI.Controllers;
using CurrencyExchangeManagerLib.Repositories;
using Microsoft.Extensions.Caching.Distributed;
using CurrencyExchangeManagerLib.Models;


namespace CurrencyExchangeManagerTests
{
    [TestFixture]
    public class SourceSystemTests
    {
        private Mock<SourceSystemRepository> _mocksourceSystemRepository;
        private Mock<IDistributedCache> _cache;
        private SourceSystemController _mocksourceSystemController;

        [SetUp]
        public void Setup() 
        { 
            _mocksourceSystemRepository = new Mock<SourceSystemRepository>();
            var sourceSystem = new SourceSystem() { Created_Date = DateTime.Now, Source_System_Id = 1, Source_System_Name = "Test", Source_System_Url = "http://www.test.com" };
            IEnumerable<SourceSystem> sourceSystemList = new List<SourceSystem>() { sourceSystem };

            _mocksourceSystemRepository.Setup(repo => repo.GetAllAsync()).Returns((Task<IEnumerable<SourceSystem>>)sourceSystemList);
            
            _cache = new Mock<IDistributedCache>();
            _mocksourceSystemController = new SourceSystemController(_mocksourceSystemRepository.Object,_cache.Object);
        }

        [TestCase("123")]
        public void GetSourceSourceSystemFromDB_ReturnsOk_WithSourceSystems(object actual)
        {            
            //act
            var result = _mocksourceSystemController.Get().Result;
            
            //assert
            Assert.AreEqual(result, actual);
        }
    }
}
