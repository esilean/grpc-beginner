﻿using Grpc.Core;
using Sqrt;
using System;
using System.Threading.Tasks;
using static Sqrt.SqrtService;

namespace GRPC.TLS.Server
{
    public class SqrtServiceImpl : SqrtServiceBase
    {
        public override async Task<SqrtResponse> Sqrt(SqrtRequest request, ServerCallContext context)
        {
            await Task.Delay(300);

            int number = request.Number;

            if (number >= 0)
                return new SqrtResponse { SquareRoot = Math.Sqrt(number) };
            else
                throw new RpcException(new Status(StatusCode.InvalidArgument, "number < 0"));
        }
    }
}
