using Grpc.Core;
using Maxnumber;
using System.Collections.Generic;
using System.Linq;
using System.Threading.Tasks;
using static Maxnumber.MaxNumberService;

namespace GRPCClientServerStreaming.Server
{
    public class GetMaxNumberService : MaxNumberServiceBase
    {
        public override async Task GetMaxNumber(IAsyncStreamReader<RandomNumberRequest> requestStream, IServerStreamWriter<MaxNumberResponse> responseStream, ServerCallContext context)
        {
            int? max = null;

            while (await requestStream.MoveNext())
            {
                if (max == null || max < requestStream.Current.Number)
                {
                    max = requestStream.Current.Number;
                    await responseStream.WriteAsync(new MaxNumberResponse { Number = max.Value });
                }
            }
        }
    }
}
