using System;
using System.Threading;

namespace Domain
{
    public class Fries
    {
        public Guid Id { get; set; }
        public FriesType Type { get; set; }

        public static Fries MakeFries(FriesType type)
        {
            var delay = (type.GetHashCode() + 1) * 1500;
            Thread.Sleep(delay);

            return new Fries
            {
                Id = Guid.NewGuid(),
                Type = type
            };
        }
    }

    public enum FriesType
    {
        Regular,
        CheeseFries,
        BaconCheeseFries
    }
}
