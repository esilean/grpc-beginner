using Calc;
using Greet;
using Grpc.Core;
using System;
using System.Threading.Tasks;

namespace GRPCReflection.Client
{
    class Program
    {
        const string target = "127.0.0.1:50051";

        static void Main(string[] args)
        {
            Channel channel = new Channel(target, ChannelCredentials.Insecure);

            channel.ConnectAsync().ContinueWith((task) =>
            {
                if (task.Status == TaskStatus.RanToCompletion)
                    Console.WriteLine("Client connected succefully");
            });

            //var client = new DummyService.DummyServiceClient(channel);
            CallGreetingService(channel);
            CallNumbersService(channel);

            channel.ShutdownAsync().Wait();

            Console.ReadKey();
        }

        private static void CallNumbersService(Channel channel)
        {
            var client = new NumbersService.NumbersServiceClient(channel);

            var numbers = new Numbers()
            {
                NumberA = 15,
                NumberB = 10
            };

            var request = new NumbersRequest { Numbers = numbers };

            var response = client.Sum(request);

            Console.WriteLine("Total Sum is: " + response.Result);
        }


        private static void CallGreetingService(Channel channel)
        {
            var client = new GreetingService.GreetingServiceClient(channel);

            var greeting = new Greeting()
            {
                FirstName = "Leandro",
                LastName = "Bevilaqua"
            };

            var request = new GreetingRequest { Greeting = greeting };

            var response = client.Greet(request);

            Console.WriteLine(response.Result);
        }
    }
}
