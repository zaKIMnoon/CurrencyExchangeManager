using System;
using System.Collections.Generic;
using System.ComponentModel.DataAnnotations;
using System.Linq;
using System.Text;
using System.Threading.Tasks;

namespace CurrencyExchangeManagerLib.Models
{
    public class SourceSystem
    {
        public int Source_System_Id {  get; set; }
        public string? Source_System_Name { get; set; }
        public string? Source_System_Url { get; set; }
        public DateTime Created_Date { get; set; }

    }
}
