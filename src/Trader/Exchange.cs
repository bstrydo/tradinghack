namespace Trader
{
    public interface Exchange
    {
        void Buy(string symbol, int numberOfShares, double price);
        void Sell(string symbol, int numberOfShares, double price);
    }
}
