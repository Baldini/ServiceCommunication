using MassTransit;
using System;

namespace SagaRequestClient.Commands.Order
{
    public class OrderDelivery : CorrelatedBy<Guid>
    {
        public Guid CorrelationId { get; set; }
    }
}
