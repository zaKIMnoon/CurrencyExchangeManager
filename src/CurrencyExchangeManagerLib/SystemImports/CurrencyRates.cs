using System;
using System.Collections.Generic;
using System.Linq;
using System.Text;
using System.Threading.Tasks;

namespace CurrencyExchangeManagerLib.SystemImports
{
        public class CurrencyExchangeRates
        {
            public string Disclaimer { get; set; }
            public string License { get; set; }
            public long Timestamp { get; set; }
            public string Base { get; set; }
            public Dictionary<string, decimal> Rates { get; set; }
        }
}
