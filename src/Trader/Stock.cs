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

        public double MostRecentHistoricPrice { get { return historicPrices.OrderByDescending(p => p.Date).First().Price; } }
        public double LatestForecastPrice { get { return forecastPrice.Price; } }
        public double ComputeValue(int quantity)
        {
            return currentPrice * quantity;
        }
    }
}
