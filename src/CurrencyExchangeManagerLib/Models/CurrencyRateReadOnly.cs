using Microsoft.VisualBasic;
using System;
using System.Collections.Generic;
using System.Linq;
using System.Text;
using System.Threading.Tasks;

namespace CurrencyExchangeManagerLib.Models
{
    public class CurrencyRateReadOnly
    {
        public string currency_code { get; set; }   
        public string currency_name { get; set; }
        public DateTime created_date { get; set; }
        public decimal rate { get; set; }
    }
}
