using Calc;
using Grpc.Core;
using System.Threading.Tasks;
using static Calc.NumbersService;

namespace GRPCUnary.Server
{
    public class NumbersServiceImpl : NumbersServiceBase
    {

        public override Task<NumbersResponse> Sum(NumbersRequest request, ServerCallContext context)
        {
            var result = request.Numbers.NumberA + request.Numbers.NumberB;

            return Task.FromResult(new NumbersResponse { Result = result });
        }
    }
}
