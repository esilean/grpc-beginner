using Greet;
using Grpc.Core;
using System.Threading.Tasks;
using static Greet.GreetingService;

namespace GRPCUnary.Server
{
    public class GreetingServiceImpl : GreetingServiceBase
    {
        public override Task<GreetingResponse> Greet(GreetingRequest request, ServerCallContext context)
        {
            string result = string.Format("Hello {0} {1}", request.Greeting.FirstName, request.Greeting.LastName);

            return Task.FromResult(new GreetingResponse { Result = result });
        }
    }
}
