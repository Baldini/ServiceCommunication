using Grpc.Core;
using System.Threading.Tasks;

namespace GRPC.DrinkService
{
    public class DrinkService : Drink.DrinkBase
    {

        public override Task<DrinkReply> MakeDrink(DrinkRequest request, ServerCallContext context)
        {
            var drink = Domain.Drink.MakeDrink((Domain.DrinkType)request.Type, (Domain.DrinkFlavor)request.Flavor, (Domain.DrinkSize)request.Size);

            return Task.FromResult(new DrinkReply { DrinkId = drink.Id.ToString() });
        }
    }
}
