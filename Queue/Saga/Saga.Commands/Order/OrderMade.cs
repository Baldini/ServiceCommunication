using MassTransit;
using System;
using System.Collections.Generic;
using System.Text;

namespace Saga.Commands
{
    public class OrderMade : CorrelatedBy<Guid>
    {
        public Guid CorrelationId { get; set; }
        public Burger Burger { get; set; }
        public Fries Fries { get; set; }
        public Drink Drink { get; set; }
    }
}
