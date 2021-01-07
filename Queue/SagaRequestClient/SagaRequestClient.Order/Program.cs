using MassTransit;
using SagaRequestClient.Commands;
using SagaRequestClient.Commands.Order;
using System;
using System.Threading.Tasks;

namespace SagaRequestClient.Order
{
    class Program
    {

        static async Task Main()
        {
            var busControl = Bus.Factory.CreateUsingRabbitMq(cfg =>
            {
                cfg.Host(new Uri("rabbitmq://localhost"), host =>
                {
                    host.Username("guest");
                    host.Password("guest");
                });
            });

            await busControl.StartAsync();
            while (true)
            {
                Console.ReadKey();
                var guid = Guid.NewGuid();
                Console.WriteLine($"Order {guid} sended");
                var client = busControl.CreateRequestClient<OrderMade>(10000000);
                var response = await client.GetResponse<OrderDelivery>(
                new OrderMade
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

                Console.WriteLine($"Order received Correlation:{response.Message.CorrelationId}");
            }
        }
    }
}
