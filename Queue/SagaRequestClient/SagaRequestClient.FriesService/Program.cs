using MassTransit;
using SagaRequestClient.Commands;
using System;
using System.Diagnostics;
using System.Threading.Tasks;

namespace SagaRequestClient.FriesService
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

                cfg.ReceiveEndpoint("saga-fries-queue", re =>
                {
                    re.Consumer<FriesConsumer>();
                });
            });
            await busControl.StartAsync();

            Console.WriteLine("Press any key to exit");
            await Task.Run(() => Console.ReadKey());
        }
    }

    public class FriesConsumer : IConsumer<FriesToMake>
    {
        public Task Consume(ConsumeContext<FriesToMake> context)
        {
            var sw = Stopwatch.StartNew();
            var fries = Domain.Fries.MakeFries(context.Message.Type);
            sw.Stop();
            Console.WriteLine($"Fries {fries.Type} has made in {sw.ElapsedMilliseconds}");
            context.Publish(new FriesMade { CorrelationId = context.Message.CorrelationId, FriesId = fries.Id });
            return Task.CompletedTask;
        }
    }
}
