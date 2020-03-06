using Domain;
using MassTransit;
using Orchestration.Commands;
using System;
using System.Diagnostics;
using System.Threading.Tasks;

namespace Orchestration.BurgerQueue
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

                cfg.ReceiveEndpoint("orchestration-burger-queue", re =>
                {
                    re.Consumer<BurgerConsumer>();
                });
            });
            await busControl.StartAsync();
        }
    }

    public class BurgerConsumer : IConsumer<BurguerCommand>
    {
        public Task Consume(ConsumeContext<BurguerCommand> context)
        {
            var sw = Stopwatch.StartNew();
            var burger = Burger.MakeBurger(context.Message.Type, context.Message.CheeseQuantity, context.Message.MeatQuantity);
            sw.Stop();
            Console.WriteLine($"Burger {burger.Id} with Cheese {burger.Cheese.ToString()} has made in {sw.ElapsedMilliseconds}");
            context.Publish(new BurgerResponse { Id = burger.Id });
            return Task.CompletedTask;
        }
    }
}
