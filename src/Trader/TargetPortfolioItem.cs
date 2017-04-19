namespace Trader
{
    public class TargetPortfolioItem : PortfolioItem
    {
        public double Weight { get; private set; }

        public TargetPortfolioItem(Stock stock, double weight)
        {
            Stock = stock;
            Weight = weight;
        }
    }
}
