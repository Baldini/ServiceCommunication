using Grpc.Core;
using System.Threading.Tasks;

namespace GRPC.BurgerService
{
    public class BurgerService : Burger.BurgerBase
    {
        public override Task<BurgerReply> MakeBurger(BurgerRequest request, ServerCallContext context)
        {
            var burger = Domain.Burger.MakeBurger((Domain.CheeseType)request.Cheese, request.CheeseQuantity, request.MeatQuantity);

            return Task.FromResult(new BurgerReply { BurgerId = burger.Id.ToString() });
        }
    }
}
