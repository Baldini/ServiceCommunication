using Domain;
using MassTransit;
using System;

namespace Saga.Commands
{
    public class DrinkToMake : CorrelatedBy<Guid>
    {
        public Guid CorrelationId { get; set; }
        public DrinkType Type { get; set; }
        public DrinkFlavor Flavor { get; set; }
        public DrinkSize Size { get; set; }
    }
}
