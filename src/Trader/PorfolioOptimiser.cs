using System;
using System.Collections.Generic;
using System.Linq;

namespace Trader
{
    public class PortfolioOptimiser
    {
        private Dictionary<string, double> portfolio;
        private Dictionary<string, List<HistoricPrice>> historicPrices;
        private Dictionary<string, List<ForecastPrice>> forecastPrices;
        private List<Stock> stocks;

        public PortfolioOptimiser(Dictionary<string, List<HistoricPrice>> historicPrices, Dictionary<string, List<ForecastPrice>> forecastPrices)
        {
            this.historicPrices = historicPrices;
            this.forecastPrices = forecastPrices;
        }

        public PortfolioOptimiser(List<Stock> stocks)
        {
            this.stocks = stocks;
        }

        public void Optimise()
        {
            double totalChange = stocks.Sum(s => s.LatestForecastPrice / s.MostRecentHistoricPrice -1);
            portfolio = stocks.ToDictionary(s => s.Symbol, s => (s.LatestForecastPrice / s.MostRecentHistoricPrice -1) / totalChange);
        }

        public Dictionary<string, double> Portfolio
        {
            get
            {
                if (portfolio == null) throw new InvalidOperationException("Can't retrieve portfolio before optimising");
                return portfolio;
            }
        }
    }
}
