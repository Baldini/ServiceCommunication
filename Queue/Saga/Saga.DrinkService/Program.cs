using MassTransit;
using Saga.Commands;
using System;
using System.Diagnostics;
using System.Threading.Tasks;

namespace Saga.DrinkService
{
    class Program
    {
        static async Task Main(string[] args)
        {
            Console.WriteLine(@"       __");
            Console.WriteLine(@"     /   ");
            Console.WriteLine(@"    /    ");
            Console.WriteLine(@".- / -.  ");
            Console.WriteLine(@"| '-' |  ");
            Console.WriteLine(@"|     |  ");
            Console.WriteLine(@"|     |  ");
            Console.WriteLine(@"|     |  ");
            Console.WriteLine(@"|     |  ");
            Console.WriteLine(@"\_____/  ");
            var busControl = Bus.Factory.CreateUsingRabbitMq(cfg =>
            {
                cfg.Host(new Uri("rabbitmq://localhost"), host =>
                {
                    host.Username("guest");
                    host.Password("guest");
                });

                cfg.ReceiveEndpoint("saga-drink-queue", re =>
                {
                    re.Consumer<DrinkConsumer>();
                });
            });
            await busControl.StartAsync();
        }
    }

    public class DrinkConsumer : IConsumer<DrinkToMake>
    {
        public Task Consume(ConsumeContext<DrinkToMake> context)
        {
            var sw = Stopwatch.StartNew();
            var drink = Domain.Drink.MakeDrink(context.Message.Type, context.Message.Flavor, context.Message.Size);
            sw.Stop();
            Console.WriteLine($"Drink {drink.Type.ToString()} with flavor {drink.Flavor.ToString()} sized {drink.Size.ToString()} has made in {sw.ElapsedMilliseconds}");
            context.Publish(new DrinkMade { CorrelationId = context.Message.CorrelationId, DrinkId = drink.Id });
            return Task.CompletedTask;
        }
    }
}
