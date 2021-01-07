using MassTransit;
using MassTransit.RedisIntegration;
using StackExchange.Redis;
using System;
using System.Threading.Tasks;

namespace SagaRequestClient.Orchestrator
{
    class Program
    {
        static async Task Main(string[] args)
        {
            var redisConnectionString = "localhost:6379";
            var redis = ConnectionMultiplexer.Connect(redisConnectionString);

            var repository = RedisSagaRepository<OrderState>.Create(() => redis.GetDatabase());
            var machine = new StateMachine();
            var busControl = Bus.Factory.CreateUsingRabbitMq(cfg =>
            {
                cfg.Host(new Uri("rabbitmq://localhost"), host =>
                {
                    host.Username("guest");
                    host.Password("guest");
                });

                cfg.ReceiveEndpoint("saga_order", e =>
                {
                    e.StateMachineSaga(machine, repository);
                });
            });

            await busControl.StartAsync();

            Console.WriteLine("Press any key to exit");
            await Task.Run(() => Console.ReadKey());
        }
    }
}
