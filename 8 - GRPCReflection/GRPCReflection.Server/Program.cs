using System;
using System.IO;
using Calc;
using Greet;
using Grpc.Core;
using Grpc.Reflection;
using Grpc.Reflection.V1Alpha;

namespace GRPCReflection.Server
{
    class Program
    {
        const int Port = 50051;

        static void Main(string[] args)
        {
            Grpc.Core.Server server = null;

            try
            {

                var reflectionServiceImpl = new ReflectionServiceImpl(GreetingService.Descriptor, NumbersService.Descriptor, ServerReflection.Descriptor);

                server = new Grpc.Core.Server()
                {
                    Services =
                    {
                        GreetingService.BindService(new GreetingServiceImpl()),
                        NumbersService.BindService(new NumbersServiceImpl()),
                        ServerReflection.BindService(reflectionServiceImpl)
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
