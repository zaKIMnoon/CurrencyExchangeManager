using System;
using System.Collections.Generic;
using System.Linq;
using System.Text;
using System.Threading.Tasks;

namespace CurrencyExchangeManagerLib.Settings
{
    public class BackgroundServiceSettings
    {
        public int DelayMilliseconds { get; set; }

        public static BackgroundServiceSettings NewBackgroundServiceSettings(int delayMilliseconds) { 
            return new BackgroundServiceSettings { DelayMilliseconds = delayMilliseconds };
        }  

    }
}
