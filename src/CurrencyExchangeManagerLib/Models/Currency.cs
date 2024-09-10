using System;
using System.Collections.Generic;
using System.Linq;
using System.Text;
using System.Threading.Tasks;

namespace CurrencyExchangeManagerLib.Models
{
    public class Currency
    {
        public int currency_id { get; set; }    
        public string currency_code { get; set; }
        public string currency_name { get; set; }
        public DateTime Created_Date { get; set; } 
    }
}
