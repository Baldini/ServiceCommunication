using System;
using System.Threading;

namespace Domain
{
    public class Burger
    {

        public Guid Id { get; set; }
        public int CheeseQuantity { get; set; }
        public CheeseType Cheese { get; set; }
        public int MeatQuantity { get; set; }

        public static Burger MakeBurger(CheeseType cheeseType, int cheeseQuantity, int meatQuantity)
        {
            var delay = ((cheeseType.GetHashCode() * cheeseQuantity) + meatQuantity) * 500;
            Thread.Sleep(delay);
            return new Burger
            {
                Id = Guid.NewGuid(),
                Cheese = cheeseType,
                CheeseQuantity = cheeseQuantity,
                MeatQuantity = meatQuantity
            };
        }
    }

    public enum CheeseType
    {
        Cheddar,
        BlueCheese,
        Camembert,
        Gruyere,
        Parmesan,
        Mozzarella
    }
}
