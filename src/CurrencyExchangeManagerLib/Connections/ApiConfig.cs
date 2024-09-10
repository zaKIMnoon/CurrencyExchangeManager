using Org.BouncyCastle.Asn1.Ocsp;
using System;
using System.Collections.Generic;
using System.Linq;
using System.Text;
using System.Threading.Tasks;

namespace CurrencyExchangeManagerLib.Connections
{
    public class ApiConfig
    {
        public string? APIKey {  get; set; }
        public string baseurl { get; private set; } = "https://openexchangerates.org/api/";
        public string currencypath { get; private set; } = $"currencies.json?";
        public string ratespath { get; private set; } = "latest.json?base=USD&symbols=ZAR%2CEUR%2CAUD&";
        
        public Dictionary<string, string> requestParams = new Dictionary<string, string>(); 
        private string GetCurrenciesRequestParams() 
        {
            return string.Join("&", requestParams.Select(kv => $"{kv.Key}={kv.Value}"));
        }

        public string GetCurrencyFullPathRequest() {
            return baseurl + currencypath + GetCurrenciesRequestParams();
        }

        public string GetCurrencyRateFullRequest() { 
            return baseurl + ratespath + "&base=USD&" + GetCurrenciesRequestParams();
        }

        public ApiConfig(string apiKey) 
        {
            APIKey = apiKey;
            requestParams.Add("prettyprint", "false");
            requestParams.Add("show_alternative", "false");
            requestParams.Add("show_inactive", "false");
            requestParams.Add("app_id", APIKey);
        }
        public static ApiConfig NewAPIConfig(string APIKey)
        {
            return new ApiConfig(APIKey);
        }
    }
}
