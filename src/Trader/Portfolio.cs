using System.Collections.Generic;

namespace Trader
{
    public abstract class Portfolio
    {
        public List<PortfolioItem> PortfolioItems { get; protected set; }
    }
}
