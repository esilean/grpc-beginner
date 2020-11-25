using Greet;
using Grpc.Core;
using Primenumber;
using System;
using System.Threading.Tasks;

namespace GRPCServerStreaming.Client
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

            await CallPrimeNumberService(channel);
            await CallGreetingService(channel);

            channel.ShutdownAsync().Wait();

            Console.ReadKey();
        }

        private static async Task CallPrimeNumberService(Channel channel)
        {
            var client = new PrimeNumberService.PrimeNumberServiceClient(channel);

            var request = new PrimeNumberRequest()
            {
                PrimeNumber = 120
            };

            var response = client.CalculatePrimeNumber(request);

            while (await response.ResponseStream.MoveNext())
            {
                Console.WriteLine(response.ResponseStream.Current.Result);
                await Task.Delay(200);
            }

        }

        private static async Task CallGreetingService(Channel channel)
        {
            var client = new GreetingService.GreetingServiceClient(channel);

            var greeting = new Greeting()
            {
                FirstName = "Leandro",
                LastName = "Bevilaqua"
            };

            var request = new GreetingManyTimesRequest { Greeting = greeting };

            var response = client.Greet(request);

            while (await response.ResponseStream.MoveNext())
            {
                Console.WriteLine(response.ResponseStream.Current.Result);
                await Task.Delay(200);
            }

        }
    }
}
