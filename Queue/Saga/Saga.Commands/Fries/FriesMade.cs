using MassTransit;
using System;

namespace Saga.Commands
{
    public class FriesMade : CorrelatedBy<Guid>
    {
        public Guid CorrelationId { get; set; }
        public Guid FriesId { get; set; }
    }
}
