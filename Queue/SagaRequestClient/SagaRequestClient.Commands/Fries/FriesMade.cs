using MassTransit;
using System;

namespace SagaRequestClient.Commands
{
    public class FriesMade : CorrelatedBy<Guid>
    {
        public Guid CorrelationId { get; set; }
        public Guid FriesId { get; set; }
    }
}
