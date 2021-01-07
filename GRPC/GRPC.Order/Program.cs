using Grpc.Net.Client;
using System;
using System.Threading.Tasks;

namespace GRPC.Order
{
    class Program
    {
        static async Task Main(string[] args)
        {
            var burgerChannel = GrpcChannel.ForAddress("https://localhost:5001");
            var burgerClient = new BurgerService.Burger.BurgerClient(burgerChannel);
            var friesChannel = GrpcChannel.ForAddress("https://localhost:5002");
            var friesClient = new FriesService.Fries.FriesClient(friesChannel);
            var drinkChannel = GrpcChannel.ForAddress("https://localhost:5003");
            var drinkClient = new DrinkService.Drink.DrinkClient(drinkChannel);

            while (true)
            {
                var burger = await burgerClient.MakeBurgerAsync(new BurgerService.BurgerRequest { Cheese = BurgerService.BurgerRequest.Types.CheeseType.BlueCheese, CheeseQuantity = 1, MeatQuantity = 1 });
                var fries = await friesClient.MakeFriesAsync(new FriesService.FriesRequest { FriesType = FriesService.FriesRequest.Types.FriesType.BaconCheeseFries });
                var drink = await drinkClient.MakeDrinkAsync(new DrinkService.DrinkRequest { Flavor = DrinkService.DrinkRequest.Types.DrinkFlavor.Cola, Size = DrinkService.DrinkRequest.Types.DrinkSize.Large, Type = DrinkService.DrinkRequest.Types.DrinkType.Soda });

                Console.WriteLine($"Burger {burger} made");
                Console.WriteLine($"Fries {fries} made");
                Console.WriteLine($"Drink {drink} made");

                Console.ReadKey();
            }
        }
    }
}
