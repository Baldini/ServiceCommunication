using MassTransit;
using System;

namespace SagaRequestClient.Commands
{
    public class OrderMade : CorrelatedBy<Guid>
    {
        public Guid CorrelationId { get; set; }
        public Burger Burger { get; set; }
        public Fries Fries { get; set; }
        public Drink Drink { get; set; }
    }
}
