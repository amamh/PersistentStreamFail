using Orleans.Concurrency;
using System;

namespace Entities
{

    [Immutable]
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
