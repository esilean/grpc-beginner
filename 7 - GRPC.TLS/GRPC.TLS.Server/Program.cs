using Grpc.Core;
using Sqrt;
using System;
using System.Collections.Generic;
using System.IO;

namespace GRPC.TLS.Server
{
    class Program
    {
        const int Port = 50051;

        static void Main(string[] args)
        {
            Grpc.Core.Server server = null;

            try
            {
                var serverCert = File.ReadAllText("ssl/server.crt");
                var serverKey = File.ReadAllText("ssl/server.key");
                var keyPair = new KeyCertificatePair(serverCert, serverKey);
                var caCrt = File.ReadAllText("ssl/ca.crt");

                var credentials = new SslServerCredentials(new List<KeyCertificatePair> { keyPair }, caCrt, true);


                server = new Grpc.Core.Server()
                {
                    Services =
                    {
                        SqrtService.BindService(new SqrtServiceImpl()),
                    },
                    Ports = { new ServerPort("localhost", Port, credentials) }
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
