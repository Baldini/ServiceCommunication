using Automatonymous;
using MassTransit;
using MassTransit.RedisIntegration;
using Saga.Commands;
using System;

namespace Saga.Orchestrator
{
    public class OrderState : SagaStateMachineInstance, CorrelatedBy<Guid>, IVersionedSaga
    {
        public Guid CorrelationId { get; set; }
        public int CurrentState { get; set; }
        public Burger Burger { get; set; }
        public Drink Drink { get; set; }
        public Fries Fries { get; set; }
        public int Version { get; set; }
    }
}
