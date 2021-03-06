using System.Collections.Generic;
using System.Linq;

namespace Trader
{
    public class Stock
    {
        public string Symbol { get; private set; }
        private List<HistoricPrice> historicPrices;
        private ForecastPrice forecastPrice;
        private double currentPrice;

        public Stock(string symbol, List<HistoricPrice> historicPrices, ForecastPrice forecastPrice)
        {
            Symbol = symbol;
            this.historicPrices = historicPrices;
            this.forecastPrice = forecastPrice;
        }

        public Stock(string symbol, double currentPrice)
        {
            Symbol = symbol;
            this.currentPrice = currentPrice;
        }

        public double MostRecentHistoricPrice()
        {
            return historicPrices.OrderByDescending(p => p.Date).First().Price;
        }

        public double LatestForecastPrice()
        {
            return forecastPrice.Price;
        }

        public double CurrentPrice()
        {
            return currentPrice;
        }

        public override bool Equals (object obj)
        {
            if (obj == null || GetType() != obj.GetType())
            {
                return false;
            }

            return Symbol == ((Stock) obj).Symbol;
        }

        public override int GetHashCode()
        {
            return Symbol.GetHashCode();
        }

        public static bool operator == (Stock a, Stock b)
        {
            return a.Equals(b);
        }

        public static bool operator != (Stock a, Stock b)
        {
            return !a.Equals(b);
        }
    }
}
