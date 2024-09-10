using CurrencyExchangeManagerLib.Connections;
using System;
using System.Collections.Generic;
using System.Data;
using System.Linq;
using System.Text;
using System.Text.Json;
using System.Threading.Tasks;

namespace CurrencyExchangeManagerLib.SystemImports
{
    public class ImportedCurrency 
    { 
        public string Code { get; set; }
        public string Name { get; set; }    
    }
}
