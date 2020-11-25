using Grpc.Core;
using Sqrt;
using System;
using System.IO;
using System.Threading.Tasks;

namespace GRPC.TLS.Client
{
    class Program
    {

        static async Task Main(string[] args)
        {
            var clientCert = File.ReadAllText("ssl/client.crt");
            var clientKey = File.ReadAllText("ssl/client.key");
            var caCrt = File.ReadAllText("ssl/ca.crt");

            var channelCredencials =
                    new SslCredentials(caCrt,
                                        new KeyCertificatePair(clientCert, clientKey));

            Channel channel = new Channel("localhost", 50051, channelCredencials);

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

            int number = 4;

            try
            {
                var response = client.Sqrt(
                        new SqrtRequest { Number = number },
                        deadline: DateTime.UtcNow.AddMilliseconds(500));

                Console.WriteLine(response.SquareRoot);
            }
            catch (RpcException e) when (e.StatusCode == StatusCode.DeadlineExceeded)
            {
                Console.WriteLine("Error: " + e.Status.StatusCode);
                Console.WriteLine("Error: " + e.Status.Detail);
                Console.WriteLine("Error: " + e.Message);
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
