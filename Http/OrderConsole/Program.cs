using Domain;
using Flurl.Http;
using System;
using System.Threading.Tasks;

namespace OrderConsole
{
    class Program
    {
        static async Task Main(string[] args)
        {
            Console.ReadKey();
            Console.WriteLine("Ordering a Burger");
            var burger = await "http://localhost:5000/burger".PostJsonAsync(new BurgerCommand
            {
                CheeseQuantity = 1,
                MeatQuantity = 1,
                Type = CheeseType.Blue_Cheese
            }).ReceiveString();

            Console.WriteLine("Ordering a Drink");
            var drink = await "http://localhost:5001/drink".PostJsonAsync(new DrinkCommand
            {
                Type = DrinkType.Soda,
                Flavor = DrinkFlavor.Guarana,
                Size = DrinkSize.Extra_Large
            }).ReceiveString();

            Console.WriteLine("Ordering a Fries");
            var fries = await "http://localhost:5002/fries".PostJsonAsync(new FriesCommand
            {
                Type = FriesType.BaconCheeseFries
            }).ReceiveString();

            Console.WriteLine($"Receive all orders Burger:{burger} Drink:{drink} Fries:{fries}");
            Console.ReadKey();
        }
    }
    public class FriesCommand
    {
        public FriesType Type { get; set; }
    }

    public class DrinkCommand
    {
        public DrinkType Type { get; set; }
        public DrinkFlavor Flavor { get; set; }
        public DrinkSize Size { get; set; }
    }

    public class BurgerCommand
    {
        public CheeseType Type { get; set; }
        public int CheeseQuantity { get; set; }
        public int MeatQuantity { get; set; }
    }
}
