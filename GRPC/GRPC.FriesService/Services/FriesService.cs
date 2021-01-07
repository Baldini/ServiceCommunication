using Grpc.Core;
using System.Threading.Tasks;

namespace GRPC.FriesService
{
    public class FriesService : Fries.FriesBase
    {
        public override Task<FriesReply> MakeFries(FriesRequest request, ServerCallContext context)
        {
            var fries = Domain.Fries.MakeFries((Domain.FriesType)request.FriesType);
            return Task.FromResult(new FriesReply
            {
                FriesId = fries.Id.ToString()
            });
        }
    }
}
