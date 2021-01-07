using Domain;
using System;

namespace Saga.Commands
{
    public class Fries
    {
        public Guid Id { get; set; }
        public FriesType Type { get; set; }
    }
}
