using MassTransit;
using Orchestration.Commands;
using System;

namespace Orchestration.Order
{
    class Program
    {
        static void Main(string[] args)
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
                Console.WriteLine("Sending order");
                busControl.Publish(new MainCommand
                {
                    Burger = new BurguerCommand
                    {
                        CheeseQuantity = 2,
                        MeatQuantity = 1,
                        Type = Domain.CheeseType.Camembert
                    },
                    Drink = new DrinkCommand
                    {
                        Type = Domain.DrinkType.Juice,
                        Flavor = Domain.DrinkFlavor.Orange,
                        Size = Domain.DrinkSize.Extra_Large
                    },
                    Fries = new FriesCommand
                    {
                        Type = Domain.FriesType.Regular
                    }
                });
            }
        }
    }
}
