using Domain;
using MassTransit;
using System;

namespace SagaRequestClient.Commands
{
    public class FriesToMake : CorrelatedBy<Guid>
    {
        public Guid CorrelationId { get; set; }
        public FriesType Type { get; set; }
    }
}
