using Greet;
using Grpc.Core;
using Maxnumber;
using System;
using System.Collections.Generic;
using System.Threading.Tasks;

namespace GRPCClientServerStreaming.Client
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

            //await CallBidirectionalGreetingService(channel);
            await CallMaxNumberService(channel);

            channel.ShutdownAsync().Wait();

            Console.ReadKey();
        }

        private static async Task CallMaxNumberService(Channel channel)
        {
            var client = new MaxNumberService.MaxNumberServiceClient(channel);

            var stream = client.GetMaxNumber();

            var responseReaderTask = Task.Run(async () =>
            {
                while (await stream.ResponseStream.MoveNext())
                {
                    Console.WriteLine("Received: " + stream.ResponseStream.Current.Number);
                }
            });

            var numbers = new List<int>();
            for (int i = 0; i < 10; i++)
            {
                numbers.Add(new Random().Next(1, 100));
            }

            foreach (var n in numbers)
            {
                Console.WriteLine("Sending: " + n.ToString());
                await stream.RequestStream.WriteAsync(
                        new RandomNumberRequest { Number = n });
            }

            await stream.RequestStream.CompleteAsync();
            Console.WriteLine("");
            await responseReaderTask;
        }

        private static async Task CallBidirectionalGreetingService(Channel channel)
        {
            var client = new GreetingService.GreetingServiceClient(channel);

            var stream = client.GreetEveryone();

            var responseReaderTask = Task.Run(async () =>
            {
                while (await stream.ResponseStream.MoveNext())
                {
                    Console.WriteLine("Received: " + stream.ResponseStream.Current.Result);
                }
            });

            Greeting[] greetings =
            {
                new Greeting{FirstName = "John", LastName = "Doe"},
                new Greeting{FirstName = "Jane", LastName = "Dae"},
                new Greeting{FirstName = "Peter", LastName = "Park"}
            };

            foreach (var greeting in greetings)
            {
                Console.WriteLine("Sending: " + greeting.ToString());
                await Task.Delay(300);
                await stream.RequestStream.WriteAsync(
                        new GreetEveryoneRequest { Greeting = greeting });
            }

            await stream.RequestStream.CompleteAsync();

            await responseReaderTask;
        }
    }
}
