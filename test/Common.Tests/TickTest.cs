using System;
using Xunit;
using Common;


namespace Common.Test
{
    public class TickTest
    {
        [Fact]
        public void GetPriceShouldReturnThePricePassedAtConstruction()
        {
            var tick = new Tick(new DateTime(), 1.92389);
            Assert.Equal(1.92389, tick.Price);
        }

        [Fact]
        public void TimeStampShouldContainTheDateTimeAtTickConstruction()
        {
            var datetime = new DateTime();
            var tick = new Tick(datetime, 1.92389);
            Assert.Equal(datetime, tick.TimeStamp);
        }
    }
}
