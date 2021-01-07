using Domain;
using System;

namespace SagaRequestClient.Commands
{
    public class Fries
    {
        public Guid Id { get; set; }
        public FriesType Type { get; set; }
    }
}
