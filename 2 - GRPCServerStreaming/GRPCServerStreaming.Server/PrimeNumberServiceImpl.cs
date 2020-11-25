using Grpc.Core;
using Primenumber;
using System.Threading.Tasks;
using static Primenumber.PrimeNumberService;

namespace GRPCServerStreaming.Server
{
    public class PrimeNumberServiceImpl : PrimeNumberServiceBase
    {
        public override async Task CalculatePrimeNumber(PrimeNumberRequest request, IServerStreamWriter<PrimeNumberResponse> responseStream, ServerCallContext context)
        {

            System.Console.WriteLine("Server received request: ");
            System.Console.WriteLine(request.ToString());

            var k = 2;
            var n = request.PrimeNumber;

            while (n > 1)
            {
                if (n % k == 0)
                {
                    await responseStream.WriteAsync(new PrimeNumberResponse { Result = k });
                    n /= k;
                }
                else
                {
                    k += 1;
                }
            }
        }
    }
}
