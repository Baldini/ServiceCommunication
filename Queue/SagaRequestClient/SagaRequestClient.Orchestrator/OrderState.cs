using Automatonymous;
using MassTransit;
using MassTransit.Saga;
using SagaRequestClient.Commands;
using System;

namespace SagaRequestClient.Orchestrator
{
    public class OrderState : SagaStateMachineInstance, CorrelatedBy<Guid>, ISagaVersion
    {
        public Guid CorrelationId { get; set; }
        public int CurrentState { get; set; }
        public Burger Burger { get; set; }
        public Drink Drink { get; set; }
        public Fries Fries { get; set; }
        public int Version { get; set; }
        public string ResponseAddress { get; set; }
    }
}
