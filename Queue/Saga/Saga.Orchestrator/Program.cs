using MassTransit;
using MassTransit.RedisIntegration;
using StackExchange.Redis;
using System;

namespace Saga.Orchestrator
{
    class Program
    {
        static void Main(string[] args)
        {
            var redisConnectionString = "localhost:6379";
            var redis = ConnectionMultiplexer.Connect(redisConnectionString);

            var repository = new RedisSagaRepository<OrderState>(() => redis.GetDatabase());
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

            busControl.Start();
        }
    }
}
