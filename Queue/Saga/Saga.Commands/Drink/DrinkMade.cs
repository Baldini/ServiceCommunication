using MassTransit;
using System;

namespace Saga.Commands
{
    public class DrinkMade : CorrelatedBy<Guid>
    {
        public Guid CorrelationId { get; set; }
        public Guid DrinkId { get; set; }
    }
}
