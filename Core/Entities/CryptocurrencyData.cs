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

    public class Ath
    {
        public double eur { get; set; }
        public double pln { get; set; }
        public double usd { get; set; }
    }

    public class AthChangePercentage
    {
        public double eur { get; set; }
        public double pln { get; set; }
        public double usd { get; set; }
    }

    public class MarketCap
    {
        public double eur { get; set; }
        public double pln { get; set; }
        public double usd { get; set; }
    }

    public class High24h
    {
        public double eur { get; set; }
        public double pln { get; set; }
        public double usd { get; set; }
    }

    public class Low24h
    {
        public double eur { get; set; }
        public double pln { get; set; }
        public double usd { get; set; }
    }

    public class PriceChangePercentage24hInCurrency
    {
        public double eur { get; set; }
        public double pln { get; set; }
        public double usd { get; set; }
    }

    public class PriceChangePercentage7dInCurrency
    {
        public double eur { get; set; }
        public double pln { get; set; }
        public double usd { get; set; }
    }

    public class PriceChangePercentage14dInCurrency
    {
        public double eur { get; set; }
        public double pln { get; set; }
        public double usd { get; set; }
    }

    public class PriceChangePercentage30dInCurrency
    {
        public double eur { get; set; }
        public double pln { get; set; }
        public double usd { get; set; }
    }

    public class PriceChangePercentage60dInCurrency
    {
        public double eur { get; set; }
        public double pln { get; set; }
        public double usd { get; set; }
    }

    public class PriceChangePercentage200dInCurrency
    {
        public double eur { get; set; }
        public double pln { get; set; }
        public double usd { get; set; }
    }

    public class PriceChangePercentage1yInCurrency
    {
        public double eur { get; set; }
        public double pln { get; set; }
        public double usd { get; set; }
    }
    public class MarketData
    {
        public CurrentPrice current_price { get; set; }
        public Ath ath { get; set; }
        public AthChangePercentage ath_change_percentage { get; set; }
        public MarketCap market_cap { get; set; }
        public High24h high_24h { get; set; }
        public Low24h low_24h { get; set; }
        public PriceChangePercentage24hInCurrency price_change_percentage_24h_in_currency { get; set; }
        public PriceChangePercentage7dInCurrency price_change_percentage_7d_in_currency { get; set; }
        public PriceChangePercentage14dInCurrency price_change_percentage_14d_in_currency { get; set; }
        public PriceChangePercentage30dInCurrency price_change_percentage_30d_in_currency { get; set; }
        public PriceChangePercentage60dInCurrency price_change_percentage_60d_in_currency { get; set; }
        public PriceChangePercentage200dInCurrency price_change_percentage_200d_in_currency { get; set; }
        public PriceChangePercentage1yInCurrency price_change_percentage_1y_in_currency { get; set; }
    }

    public class Market
    {
        public string name { get; set; }
        public string identifier { get; set; }
        public bool has_trading_incentive { get; set; }
    }


    public class CryptocurrencyData
    {
        public string id { get; set; }
        public string symbol { get; set; }
        public string name { get; set; }
        public double sentiment_votes_up_percentage { get; set; }
        public double sentiment_votes_down_percentage { get; set; }
        public int market_cap_rank { get; set; }
        public MarketData market_data { get; set; }

    }
}
