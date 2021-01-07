using Domain;
using System;

namespace SagaRequestClient.Commands
{
    public class Burger
    {
        public Guid? Id { get; set; }
        public int CheeseQuantity { get; set; }
        public CheeseType Cheese { get; set; }
        public int MeatQuantity { get; set; }
    }
}
