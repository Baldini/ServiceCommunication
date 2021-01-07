using MassTransit;
using Saga.Commands;
using System;
using System.Diagnostics;
using System.Threading.Tasks;

namespace Saga.BurgerService
{
    class Program
    {
        static async Task Main(string[] args)
        {
            Console.WriteLine(@"    _..----.._    ");
            Console.WriteLine(@"  .'     o    '.  ");
            Console.WriteLine(@" /   o       o  \ ");
            Console.WriteLine(@"|o        o     o|");
            Console.WriteLine(@"/'-.._o     __.-'\");
            Console.WriteLine(@"\      `````     /");
            Console.WriteLine(@"|``--........--'`|");
            Console.WriteLine(@" \              /");
            Console.WriteLine(@"  `'----------'`");

            var busControl = Bus.Factory.CreateUsingRabbitMq(cfg =>
            {
                cfg.Host(new Uri("rabbitmq://localhost"), host =>
                {
                    host.Username("guest");
                    host.Password("guest");
                });

                cfg.ReceiveEndpoint("saga-burger-queue", re =>
                {
                    re.Consumer<BurguerConsumer>();
                });
            });
            await busControl.StartAsync();
        }
    }

    public class BurguerConsumer : IConsumer<BurgerToMake>
    {
        public Task Consume(ConsumeContext<BurgerToMake> context)
        {
            var sw = Stopwatch.StartNew();
            var burger = Domain.Burger.MakeBurger(context.Message.Cheese, context.Message.CheeseQuantity, context.Message.MeatQuantity);
            sw.Stop();
            Console.WriteLine($"Burger {burger.Id} with Cheese {burger.Cheese.ToString()} has made in {sw.ElapsedMilliseconds}");
            context.Publish(new BurgerMade { BurgerId = burger.Id, CorrelationId = context.Message.CorrelationId });
            return Task.CompletedTask;
        }
    }
}
