using System;

namespace Common
{
    public class Tick
    {
        public DateTime TimeStamp {get; }
        public double Price {get;}

        public Tick(DateTime timeStamp, double price)
        {
            this.TimeStamp = timeStamp;
            this.Price = price;
        }
    }
}
