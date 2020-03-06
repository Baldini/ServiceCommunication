using Domain;

namespace Orchestration.Commands
{
    public class BurguerCommand
    {
        public CheeseType Type { get; set; }
        public int CheeseQuantity { get; set; }
        public int MeatQuantity { get; set; }
    }
}
