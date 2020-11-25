using Grpc.Core;
using Longgreeting;
using System;
using System.Threading.Tasks;
using static Longgreeting.GreetingService;

namespace GRPCClientStreaming.Server
{
    public class GreetingServiceImpl : GreetingServiceBase
    {
        public override async Task<LongGreetResponse> LongGreet(IAsyncStreamReader<LongGreetRequest> requestStream, ServerCallContext context)
        {
            string result = "";
            int i = 1;

            while (await requestStream.MoveNext())
            {
                Console.WriteLine("Moving next...");
                result += string.Format("Hello {0} {1} {2} {3}",
                                requestStream.Current.Greeting.FirstName,
                                requestStream.Current.Greeting.LastName,
                                i,
                                Environment.NewLine);

                Console.WriteLine(result);

                await Task.Delay(200);
                i += 1;
            }

            return new LongGreetResponse { Result = result };
        }
    }
}
