using Domain;
using MassTransit;
using System;

namespace Saga.Commands
{
    public class FriesToMake : CorrelatedBy<Guid>
    {
        public Guid CorrelationId { get; set; }
        public FriesType Type { get; set; }
    }
}
