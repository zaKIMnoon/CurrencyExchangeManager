using System;
using System.Collections.Generic;
using System.Linq;
using System.Text;
using System.Threading.Tasks;

namespace CurrencyExchangeManagerLib.Models
{
    public class CurrencyRate
    {
        public int exchange_rate_id { get;set; }
        public int source_currency_id { get; set; }
        public int target_currency_id{ get; set; }
        public int source_system_id { get; set; }   
        public decimal rate { get; set; }
        public DateTime create_date { get; set; }   
    }
}
