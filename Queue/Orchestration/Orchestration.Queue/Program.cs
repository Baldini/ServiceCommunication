using MassTransit;
using Orchestration.Commands;
using System;
using System.Threading.Tasks;

namespace Orchestration.Queue
{
    class Program
    {
        static async Task Main(string[] args)
        {
            Console.WriteLine("Read to Orchestrate");
            var busControl = Bus.Factory.CreateUsingRabbitMq(cfg =>
            {
                cfg.Host(new Uri("rabbitmq://localhost"), host =>
                {
                    host.Username("guest");
                    host.Password("guest");
                });

                cfg.ReceiveEndpoint("orchestration-burger-response-queue", re =>
                {
                    re.Consumer<BurgerConsumer>();
                });
                cfg.ReceiveEndpoint("orchestration-drink-response-queue", re =>
                {
                    re.Consumer<DrinkConsumer>();
                });
                cfg.ReceiveEndpoint("orchestration-fries-response-queue", re =>
                {
                    re.Consumer<FriesConsumer>();
                });
                cfg.ReceiveEndpoint("orchestration-main-queue", re =>
                {
                    re.Consumer<MainConsumer>();
                });

            });
            await busControl.StartAsync();
        }
    }

    public class MainConsumer : IConsumer<MainCommand>
    {
        public Task Consume(ConsumeContext<MainCommand> context)
        {
            Console.WriteLine("Sending Burguer");
            context.Publish(context.Message.Burger);

            Console.WriteLine("Sending Drink");
            context.Publish(context.Message.Drink);

            Console.WriteLine("Sending Fries");
            context.Publish(context.Message.Fries);

            return Task.CompletedTask;

        }
    }

    public class BurgerConsumer : IConsumer<BurgerResponse>
    {
        public Task Consume(ConsumeContext<BurgerResponse> context)
        {
            Console.WriteLine($"Receive Burger ID {context.Message.Id}");
            return Task.CompletedTask;
        }
    }

    public class DrinkConsumer : IConsumer<DrinkResponse>
    {
        public Task Consume(ConsumeContext<DrinkResponse> context)
        {
            Console.WriteLine($"Receive Drink ID {context.Message.Id}");
            return Task.CompletedTask;
        }
    }

    public class FriesConsumer : IConsumer<FriesResponse>
    {
        public Task Consume(ConsumeContext<FriesResponse> context)
        {
            Console.WriteLine($"Receive Fries ID {context.Message.Id}");
            return Task.CompletedTask;
        }
    }
}
