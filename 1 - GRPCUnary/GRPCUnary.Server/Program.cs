using System;
using System.IO;
using Calc;
using Greet;
using Grpc.Core;

namespace GRPCUnary.Server
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
                        GreetingService.BindService(new GreetingServiceImpl()),
                        NumbersService.BindService(new NumbersServiceImpl())
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
