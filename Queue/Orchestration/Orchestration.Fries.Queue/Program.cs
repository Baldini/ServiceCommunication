using Domain;
using MassTransit;
using Orchestration.Commands;
using System;
using System.Diagnostics;
using System.Threading.Tasks;

namespace Orchestration.FriesQueue
{
    class Program
    {
        static async Task Main(string[] args)
        {
            Console.WriteLine(@" |\ /| /|_/|   ");
            Console.WriteLine(@"|\||-|\||-/|/| ");
            Console.WriteLine(@" \\|\|//||///  ");
            Console.WriteLine(@" |\/\||//||||  ");
            Console.WriteLine(@" |||\\|/\\ ||  ");
            Console.WriteLine(@" | './\_/.' |  ");
            Console.WriteLine(@" | .:.  .:. |  ");
            Console.WriteLine(@" | :  ::  : |  ");
            Console.WriteLine(@" | :  ''  : |  ");
            Console.WriteLine(@"  '.______.'   ");
            var busControl = Bus.Factory.CreateUsingRabbitMq(cfg =>
            {
                cfg.Host(new Uri("rabbitmq://localhost"), host =>
                {
                    host.Username("guest");
                    host.Password("guest");
                });

                cfg.ReceiveEndpoint("orchestration-fries-queue", re =>
                {
                    re.Consumer<FriesConsumer>();
                });
            });
            await busControl.StartAsync();
        }
    }

    public class FriesConsumer : IConsumer<FriesCommand>
    {
        public Task Consume(ConsumeContext<FriesCommand> context)
        {
            var sw = Stopwatch.StartNew();
            var fries = Fries.MakeFries(context.Message.Type);
            sw.Stop();
            Console.WriteLine($"Fries {fries.Type.ToString()} has made in {sw.ElapsedMilliseconds}");
            context.Publish(new FriesResponse { Id = fries.Id });
            return Task.CompletedTask;
        }
    }
}
