using System;
using System.Collections.Generic;
using System.Linq;

namespace Trader
{
    public class PortfolioOptimiser
    {
        private Dictionary<string, double> portfolio;
        private List<HistoricPrice> historicPrices;
        private List<ForecastPrice> forecastPrices;

        public PortfolioOptimiser(List<HistoricPrice> historicPrices, List<ForecastPrice> forecastPrices)
        {
            this.historicPrices = historicPrices;
            this.forecastPrices = forecastPrices;
        }

        public void Optimise()
        {
            double totalChange = historicPrices.Zip(forecastPrices, (h, f) => f.Price / h.Price - 1).Sum();
            portfolio = historicPrices.Zip(forecastPrices, (h, f) => new Tuple<string, double>(h.Symbol, (f.Price / h.Price - 1) / totalChange))
                .ToDictionary(x => x.Item1, x => x.Item2);
        }

        public Dictionary<string, double> Profile { get { return portfolio; } }
    }
}
