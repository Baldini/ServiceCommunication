using MassTransit;
using System;

namespace SagaRequestClient.Commands
{
    public class DrinkMade : CorrelatedBy<Guid>
    {
        public Guid CorrelationId { get; set; }
        public Guid DrinkId { get; set; }
    }
}
