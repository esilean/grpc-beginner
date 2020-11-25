using Computedavg;
using Grpc.Core;
using System.Threading.Tasks;
using static Computedavg.ComputedAvgService;

namespace GRPCClientStreaming.Server
{
    public class ComputeAvgServiceImpl : ComputedAvgServiceBase
    {
        public override async Task<NumberXResponse> ComputeAvg(IAsyncStreamReader<NumberXRequest> requestStream, ServerCallContext context)
        {
            double result = 0;
            int total = 0;

            while (await requestStream.MoveNext())
            {
                total += 1;
                result += requestStream.Current.NumberX;
            }

            return new NumberXResponse { Result = result / total };
        }
    }
}
