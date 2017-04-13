using System.Collections.Generic;
using System.Linq;

namespace Trader
{
    public class Stock
    {
        public string Symbol { get; private set; }
        private List<HistoricPrice> historicPrices;
        private ForecastPrice forecastPrice;

        public Stock(string symbol, List<HistoricPrice> historicPrices, ForecastPrice forecastPrice)
        {
            Symbol = symbol;
            this.historicPrices = historicPrices;
            this.forecastPrice = forecastPrice;
        }

        public double MostRecentHistoricPrice { get { return historicPrices.OrderByDescending(p => p.Date).First().Price; } }
        public double LatestForecastPrice { get { return forecastPrice.Price; } }
    }
}