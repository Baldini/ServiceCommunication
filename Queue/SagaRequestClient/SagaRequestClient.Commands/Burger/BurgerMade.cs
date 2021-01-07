using MassTransit;
using System;

namespace SagaRequestClient.Commands
{
    public class BurgerMade : CorrelatedBy<Guid>
    {
        public Guid CorrelationId { get; set; }
        public Guid BurgerId { get; set; }
    }
}
