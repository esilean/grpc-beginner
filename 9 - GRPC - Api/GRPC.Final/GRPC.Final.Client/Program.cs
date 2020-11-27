using Blog;
using Grpc.Core;
using System;
using System.Threading.Tasks;

namespace GRPC.Final.Client
{
    class Program
    {

        static async Task Main(string[] args)
        {
            Channel channel = new Channel("localhost", 5001, ChannelCredentials.Insecure);

            await channel.ConnectAsync().ContinueWith((task) =>
            {
                if (task.Status == TaskStatus.RanToCompletion)
                    Console.WriteLine("Client connected succefully");
            });

            var client = new BlogService.BlogServiceClient(channel);

            //AddBlogService(client);
            //AddBlogService(client);
            //AddBlogService(client);
            await ListBlogService(client);
            //ReadBlogService(client);
            //UpdateBlogService(client);
            //DeleteBlogService(client);



            channel.ShutdownAsync().Wait();

            Console.ReadKey();
        }


        private static async Task ListBlogService(BlogService.BlogServiceClient client)
        {
            var request = new ListBlogRequest { };

            try
            {
                var response = client.ListBlog(request);
                while (await response.ResponseStream.MoveNext())
                {
                    Console.WriteLine(response.ResponseStream.Current.Blog.ToString());
                }
            }
            catch (RpcException e)
            {
                Console.WriteLine(e.Status.Detail);
            }

        }
        private static void AddBlogService(BlogService.BlogServiceClient client)
        {

            var request = new CreateBlogRequest
            {
                Blog = new Blog.Blog
                {
                    AuthorId = "lebevila",
                    Title = "Saiba mais sobre gRpc",
                    Content = "Saiba mais sobre gRpc e tudo mais..."
                }
            };

            var response = client.CreateBlog(request);

            Console.WriteLine(response.Blog);
        }
        private static void UpdateBlogService(BlogService.BlogServiceClient client)
        {

            var request = new UpdateBlogRequest
            {
                Blog = new Blog.Blog
                {
                    Id = "d21c4751-2078-4583-9692-8f7f0a0fbbb2",
                    AuthorId = "lebevila V2",
                    Title = "Saiba mais sobre gRpc V2",
                    Content = "Saiba mais sobre gRpc e tudo mais... V2"
                }
            };

            try
            {
                var response = client.UpdateBlog(request);
                Console.WriteLine(response.Blog);
            }
            catch (RpcException e)
            {
                Console.WriteLine(e.Status.Detail);
            }


        }
        private static void DeleteBlogService(BlogService.BlogServiceClient client)
        {

            var request = new DeleteBlogRequest
            {
                Id = "d21c4751-2078-4583-9692-8f7f0a0fbbb2"
            };

            try
            {
                var response = client.DeleteBlog(request);
                Console.WriteLine(response.Success);
            }
            catch (RpcException e)
            {
                Console.WriteLine(e.Status.Detail);
            }


        }
    }
}
