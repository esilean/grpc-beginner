using Greet;
using Grpc.Core;
using Primenumber;
using System;
using System.IO;

namespace GRPCServerStreaming.Server
{
    class Program
    {
        const int Port = 50051;

        static void Main(string[] args)
        {
            Grpc.Core.Server server = null;

            try
            {
                server = new Grpc.Core.Server()
                {
                    Services =
                    {
                        PrimeNumberService.BindService(new PrimeNumberServiceImpl()),
                        GreetingService.BindService(new GreetingServiceImpl())
                    },
                    Ports = { new ServerPort("localhost", Port, ServerCredentials.Insecure) }
                };

                server.Start();

                Console.WriteLine($"Server is listening on port {Port}");
                Console.ReadKey();
            }
            catch (IOException e)
            {
                Console.WriteLine("Server failed to start: " + e.Message);
                throw;
            }
            finally
            {
                if (server != null)
                    server.ShutdownAsync().Wait();
            }
        }
    }
}
