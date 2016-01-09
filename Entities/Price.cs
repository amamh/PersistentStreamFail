using Orleans.Concurrency;
using System;

namespace Entities
{

    [Serializable]
    public class Price : IPrice
    {
        public double? Ask
        {
            get; set;
        }

        public double? Bid
        {
            get; set;
        }

        public double? Mid
        {
            get; set;
        }

        public string RIC
        {
            get; set;
        }
    }
}
