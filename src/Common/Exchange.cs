namespace Common
{
    public interface Exchange
    {
        void Buy(string symbol, int numberOfShares);
        void Sell(string symbol, int numberOfShares);
    }
}
