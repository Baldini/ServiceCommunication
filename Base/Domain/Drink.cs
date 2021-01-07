using System;
using System.Threading;

namespace Domain
{
    public class Drink
    {
        public Guid Id { get; set; }
        public DrinkType Type { get; set; }
        public DrinkFlavor Flavor { get; set; }
        public DrinkSize Size { get; set; }

        public static Drink MakeDrink(DrinkType type, DrinkFlavor flavor, DrinkSize size)
        {
            var delay = (flavor.GetHashCode() + size.GetHashCode() + (type.GetHashCode() * 2)) * 300;
            Thread.Sleep(delay);
            return new Drink
            {
                Id = Guid.NewGuid(),
                Flavor = flavor,
                Size = size,
                Type = type
            };
        }
    }

    public enum DrinkSize
    {
        Small,
        Medium,
        Large,
        Extra_Large
    }

    public enum DrinkType
    {
        Soda,
        Juice
    }

    public enum DrinkFlavor
    {
        Cola,
        Orange,
        Lemon,
        Guarana
    }
}
