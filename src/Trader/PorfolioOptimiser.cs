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
            var orderedHistoricPrices = historicPrices.OrderBy(p => p.Symbol);
            var orderedForecastPrices = forecastPrices.OrderBy(p => p.Symbol);
            double totalChange = orderedHistoricPrices.Zip(orderedForecastPrices, (h, f) => f.Price / h.Price - 1).Sum();
            portfolio = orderedHistoricPrices.Zip(orderedForecastPrices, (h, f) => new Tuple<string, double>(h.Symbol, (f.Price / h.Price - 1) / totalChange))
                .ToDictionary(x => x.Item1, x => x.Item2);
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
