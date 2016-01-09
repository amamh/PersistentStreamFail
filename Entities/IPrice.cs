using Orleans.Concurrency;

namespace Entities
{
    public interface IPrice
    {
        string RIC { get; }

        double? Ask { get; }

        double? Bid { get; }

        double? Mid { get; }
    }
}
