using Grpc.Core;
using Sqrt;
using System;
using System.Threading.Tasks;

namespace GRPCHandlingError.Client
{
    class Program
    {
        const string target = "127.0.0.1:50051";

        static async Task Main(string[] args)
        {
            Channel channel = new Channel(target, ChannelCredentials.Insecure);

            await channel.ConnectAsync().ContinueWith((task) =>
            {
                if (task.Status == TaskStatus.RanToCompletion)
                    Console.WriteLine("Client connected succefully");
            });

            await CallSquareRootService(channel);

            channel.ShutdownAsync().Wait();

            Console.ReadKey();
        }

        private static async Task CallSquareRootService(Channel channel)
        {
            var client = new SqrtService.SqrtServiceClient(channel);

            int number = -1;

            try
            {
                var response = client.Sqrt(new SqrtRequest { Number = number });
                Console.WriteLine(response.SquareRoot);
            }
            catch (RpcException e)
            {
                Console.WriteLine("Error: " + e.Status.StatusCode);
                Console.WriteLine("Error: " + e.Status.Detail);
                Console.WriteLine("Error: " + e.Message);
            }

        }

    }
}
