using Computedavg;
using Grpc.Core;
using Longgreeting;
using System;
using System.Linq;
using System.Threading.Tasks;

namespace GRPCClientStreaming.Client
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

            //await CallLongGreetingService(channel);
            await CallComputeAvgService(channel);

            channel.ShutdownAsync().Wait();

            Console.ReadKey();
        }

        private static async Task CallComputeAvgService(Channel channel)
        {
            var client = new ComputedAvgService.ComputedAvgServiceClient(channel);

            var stream = client.ComputeAvg();

            foreach (int i in Enumerable.Range(1, 4))
            {
                var request = new NumberXRequest { NumberX = i };
                await stream.RequestStream.WriteAsync(request);
            }

            await stream.RequestStream.CompleteAsync();

            var response = await stream.ResponseAsync;

            Console.WriteLine("Result: ");
            Console.WriteLine(response.Result);

        }


        private static async Task CallLongGreetingService(Channel channel)
        {
            var client = new GreetingService.GreetingServiceClient(channel);

            var greeting = new Greeting()
            {
                FirstName = "Leandro",
                LastName = "Bevilaqua"
            };

            var request = new LongGreetRequest { Greeting = greeting };

            var stream = client.LongGreet();

            foreach (int i in Enumerable.Range(1, 10))
            {
                await stream.RequestStream.WriteAsync(request);
            }

            await stream.RequestStream.CompleteAsync();

            var response = await stream.ResponseAsync;

            Console.WriteLine("Result: ");
            Console.WriteLine(response.Result);

        }
    }
}
