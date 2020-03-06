using Domain;

namespace Orchestration.Commands
{
    public class DrinkCommand
    {
        public DrinkType Type { get; set; }
        public DrinkFlavor Flavor { get; set; }
        public DrinkSize Size { get; set; }
    }
}
