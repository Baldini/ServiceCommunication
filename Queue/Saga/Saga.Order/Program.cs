using MassTransit;
using Saga.Commands;
using System;
using System.Threading.Tasks;

namespace Saga.Order
{
    class Program
    {
        static async Task Main(string[] args)
        {
            var busControl = Bus.Factory.CreateUsingRabbitMq(cfg =>
            {
                cfg.Host(new Uri("rabbitmq://localhost"), host =>
                {
                    host.Username("guest");
                    host.Password("guest");
                });
            });
            while (true)
            {
                Console.ReadKey();
                var guid = Guid.NewGuid();
                Console.WriteLine($"Order {guid} sended");
                await busControl.Publish(new OrderMade
                {
                    CorrelationId = guid,
                    Burger = new Burger
                    {
                        CheeseQuantity = 2,
                        MeatQuantity = 1,
                        Cheese = Domain.CheeseType.Camembert
                    },
                    Drink = new Drink
                    {
                        Type = Domain.DrinkType.Juice,
                        Flavor = Domain.DrinkFlavor.Orange,
                        Size = Domain.DrinkSize.Extra_Large
                    },
                    Fries = new Fries
                    {
                        Type = Domain.FriesType.Regular
                    }
                });
            }
        }
    }
}
