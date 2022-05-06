using System;
using System.Collections.Generic;
using System.Linq;
using System.Text;
using System.Threading.Tasks;

namespace Core.Entities
{
    public class CurrentPrice
    {
        public double eur { get; set; }
        public double pln { get; set; }
        public double usd { get; set; }
    }

    
    public class MarketData
    {
        public CurrentPrice current_price { get; set; }
    }


    public class CryptocurrencyData
    {
        public string id { get; set; }
        public string symbol { get; set; }
        public string name { get; set; }
        public MarketData market_data { get; set; }

    }
}
