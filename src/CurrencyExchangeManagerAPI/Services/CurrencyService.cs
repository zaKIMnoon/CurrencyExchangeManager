
using CurrencyExchangeManagerLib.Connections;
using CurrencyExchangeManagerLib.Models;
using CurrencyExchangeManagerLib.Repositories;
using CurrencyExchangeManagerLib.Settings;
using CurrencyExchangeManagerLib.SystemImports;
using Microsoft.Extensions.Options;
using System.Text.Json;

namespace CurrencyExchangeManagerAPI.Services
{
    public class CurrencyService : BackgroundService
    {
        private readonly BackgroundServiceSettings _settings;

        private readonly SourceSystemRepository _sourceSystemRepository;

        private readonly CurrencyRepository _currencyRepository;
        private readonly CurrencyRateRepository _currencyRateRepository;
        private SourceSystem sourceSystem { get; set; }

        private static readonly HttpClient client = new HttpClient();

        protected readonly ApiConfig _apiconfig;

        public bool SystemEntryExists = false;
        public CurrencyService(IOptions<BackgroundServiceSettings> settings, ApiConfig apiconfig, SourceSystemRepository sourceSystemRepository, CurrencyRepository currencyRepository, CurrencyRateRepository currencyRateRepository) { 
            _settings = settings.Value;
            _apiconfig = apiconfig;
            _sourceSystemRepository = sourceSystemRepository;
            _currencyRepository = currencyRepository;
            _currencyRateRepository = currencyRateRepository;
        }

        public async Task<SourceSystem> GetSourceSystem()
        {
            sourceSystem = new SourceSystem() { Source_System_Name = "openexchangerates", Source_System_Url = _apiconfig.baseurl };
            var dbSourceSystem = await _sourceSystemRepository.GetByNameAsync(sourceSystem.Source_System_Name);

            if (dbSourceSystem == null)
            {
                dbSourceSystem = await _sourceSystemRepository.AddAsync(sourceSystem);
            }
            return dbSourceSystem;
        }

        private async void ImportCurrencyRates() {
            string url = _apiconfig.GetCurrencyRateFullRequest();
            client.DefaultRequestHeaders.Add("Accept", "application/json");
            HttpResponseMessage response = await client.GetAsync(url).ConfigureAwait(true);
            response.EnsureSuccessStatusCode();
            string responseBody = await response.Content.ReadAsStringAsync().ConfigureAwait(true);
            
            var options = new JsonSerializerOptions
            {
                PropertyNameCaseInsensitive = true
            };

            var currencyexchangerates = JsonSerializer.Deserialize<CurrencyExchangeRates>(responseBody, options);

            var dbCurrencies = await _currencyRepository.GetAllAsync();
            var sourcesystem = await GetSourceSystem();
            var baseCurrency = dbCurrencies.Where(c => c.currency_code == currencyexchangerates.Base).FirstOrDefault();

            var newCurrencyRateList = new List<CurrencyRate>();
            
            currencyexchangerates.Rates.ToList().ForEach(c => {
                var targetCurrency = dbCurrencies.Where(dbCurr => dbCurr.currency_code == c.Key).FirstOrDefault();
                newCurrencyRateList.Add(new CurrencyRate() { source_currency_id = baseCurrency.currency_id, target_currency_id = targetCurrency.currency_id, rate = c.Value, source_system_id = sourcesystem.Source_System_Id });
            });

            _currencyRateRepository.Add(newCurrencyRateList);

        }

        private async void ImportCurrency() 
        {
            try
            {
                string url = _apiconfig.GetCurrencyFullPathRequest(); // Replace with your API URL
                client.DefaultRequestHeaders.Add("Accept", "application/json");
                HttpResponseMessage response = await client.GetAsync(url).ConfigureAwait(true);
                response.EnsureSuccessStatusCode();
                string responseBody = await response.Content.ReadAsStringAsync().ConfigureAwait(true);
                var currencies = JsonSerializer.Deserialize<Dictionary<string, string>>(responseBody);
                var dbCurrencies = await _currencyRepository.GetAllAsync();

                var missingCurrencylist = new List<Currency>();

                currencies.ToList().ForEach(currency => {
                    
                    var tempCurrency = dbCurrencies.Where(c => c.currency_code == currency.Key).FirstOrDefault();
                    
                    if (tempCurrency == null) 
                    {
                        missingCurrencylist.Add(new Currency() { currency_code = currency.Key, currency_name= currency.Value });
                    }
                });
                
                missingCurrencylist.ForEach(async currency => {
                    await _currencyRepository.AddAsync(currency);
                });
            }
            catch (HttpRequestException e)
            {

            }
        }

        protected override async Task ExecuteAsync(CancellationToken stoppingToken)
        {
            while (!stoppingToken.IsCancellationRequested)
            {
                // Your background task logic here
                SourceSystem sourcesystem = await GetSourceSystem();
                //ImportCurrency();
                //ImportCurrencyRates();
                await Task.Delay(_settings.DelayMilliseconds, stoppingToken); // Delay from settings
            }
        }
    }
}
