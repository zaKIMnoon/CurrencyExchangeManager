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
        public int exchange_rate_id {  get; set; }
        public string Source_Currency { get; set; }   
        public string Target_Currency { get; set; }
        public int source_system_id { get; set; }   
        public decimal rate { get; set; }
        public DateTime created_date { get; set; }
    }
}
