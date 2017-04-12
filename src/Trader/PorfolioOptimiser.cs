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

        public PortfolioOptimiser(Dictionary<string, List<HistoricPrice>> historicPrices, Dictionary<string, List<ForecastPrice>> forecastPrices)
        {
            this.historicPrices = historicPrices;
            this.forecastPrices = forecastPrices;
        }

        public void Optimise()
        {
            double totalChange = historicPrices.Keys.Sum(s => forecastPrices[s][0].Price / historicPrices[s][0].Price - 1);
            portfolio = historicPrices.Keys.ToDictionary(s => s, s => (forecastPrices[s][0].Price / historicPrices[s][0].Price - 1) / totalChange);
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
