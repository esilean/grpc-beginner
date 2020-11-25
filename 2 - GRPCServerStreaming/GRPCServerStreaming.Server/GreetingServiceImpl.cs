using Greet;
using Grpc.Core;
using System.Linq;
using System.Threading.Tasks;
using static Greet.GreetingService;

namespace GRPCServerStreaming.Server
{
    public class GreetingServiceImpl : GreetingServiceBase
    {
        public override async Task Greet(GreetingManyTimesRequest request, IServerStreamWriter<GreetingManyTimesResponse> responseStream, ServerCallContext context)
        {
            System.Console.WriteLine("Server received request: ");
            System.Console.WriteLine(request.ToString());

            string result = string.Format("Hello {0} {1}", request.Greeting.FirstName, request.Greeting.LastName);

            foreach (int i in Enumerable.Range(1, 10))
            {
                await responseStream.WriteAsync(new GreetingManyTimesResponse { Result = result });
            }
        }
    }
}
