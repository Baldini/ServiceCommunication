using Domain;
using MassTransit;
using System;

namespace SagaRequestClient.Commands
{
    public class BurgerToMake : CorrelatedBy<Guid>
    {
        public Guid CorrelationId { get; set; }
        public int CheeseQuantity { get; set; }
        public CheeseType Cheese { get; set; }
        public int MeatQuantity { get; set; }
    }
}
