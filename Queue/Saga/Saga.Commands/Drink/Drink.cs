﻿using Domain;
using System;

namespace Saga.Commands
{
    public class Drink
    {
        public Guid? Id { get; set; }
        public DrinkType Type { get; set; }
        public DrinkFlavor Flavor { get; set; }
        public DrinkSize Size { get; set; }
    }
}
