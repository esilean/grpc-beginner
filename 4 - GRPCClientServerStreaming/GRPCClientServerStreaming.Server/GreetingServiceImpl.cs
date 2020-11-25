using Greet;
using Grpc.Core;
using System;
using System.Threading.Tasks;
using static Greet.GreetingService;

namespace GRPCClientServerStreaming.Server
{
    public class GreetingServiceImpl : GreetingServiceBase
    {
        public override async Task GreetEveryone(IAsyncStreamReader<GreetEveryoneRequest> requestStream, IServerStreamWriter<GreetEveryoneResponse> responseStream, ServerCallContext context)
        {
            int i = 1;
            while (await requestStream.MoveNext())
            {
                var result = string.Format("Hello {0} {1} {2} {3}",
                    requestStream.Current.Greeting.FirstName,
                    requestStream.Current.Greeting.LastName,
                    i,
                    Environment.NewLine);
                i += 1;

                Console.WriteLine("Received: " + result);
                await responseStream.WriteAsync(new GreetEveryoneResponse { Result = result });
                await Task.Delay(100);
            }
        }
    }
}
