using Grpc.Core;
using GRPC.Final.GrpcService.Domain.Repositories;
using Microsoft.Extensions.Logging;
using System;
using System.Threading.Tasks;

namespace GRPC.Final.GrpcService.Services
{
    public class BlogService : Blog.BlogService.BlogServiceBase
    {
        private readonly ILogger<BlogService> _logger;
        private readonly IBlogRepository _blogRepository;

        public BlogService(
                            ILogger<BlogService> logger,
                            IBlogRepository blogRepository)
        {
            _logger = logger;
            _blogRepository = blogRepository;
        }

        public override async Task ListBlog(Blog.ListBlogRequest request, IServerStreamWriter<Blog.ListBlogResponse> responseStream, ServerCallContext context)
        {
            _logger.LogInformation("Listing blogs...");

            var blogs = await _blogRepository.ListAsync();

            foreach (var blog in blogs)
            {
                var blogResponse = new Blog.ListBlogResponse
                {
                    Blog = new Blog.Blog
                    {
                        Id = blog.Id.ToString(),
                        AuthorId = blog.AuthorId,
                        Title = blog.Title,
                        Content = blog.Content
                    }
                };

                await responseStream.WriteAsync(blogResponse);
            }
        }

        public override async Task<Blog.ReadBlogResponse> ReadBlog(Blog.ReadBlogRequest request, ServerCallContext context)
        {
            _logger.LogInformation("Reading blog: " + request.Id.ToString());

            if (!Guid.TryParse(request.Id, out Guid blogId))
                throw new RpcException(new Status(StatusCode.InvalidArgument, $"Blog Id {request.Id} is invalid"));

            var blog = await _blogRepository.GetAsync(blogId);

            if (blog == null)
                throw new RpcException(new Status(StatusCode.NotFound, $"Blog Id {request.Id} not found"));

            var blogResponse = new Blog.ReadBlogResponse
            {
                Blog = new Blog.Blog
                {
                    AuthorId = blog.AuthorId,
                    Title = blog.Title,
                    Content = blog.Content
                }
            };

            return await Task.FromResult(blogResponse);
        }

        public override async Task<Blog.CreateBlogResponse> CreateBlog(Blog.CreateBlogRequest request, ServerCallContext context)
        {
            var blog = Domain.Blog.Factory(request.Blog.AuthorId,
                                            request.Blog.Title,
                                            request.Blog.Content);

            _logger.LogInformation("Saving blog: " + blog);

            await _blogRepository.AddAsync(blog);

            var blogResponse = new Blog.CreateBlogResponse
            {
                Blog = new Blog.Blog
                {
                    Id = blog.Id.ToString(),
                    AuthorId = blog.AuthorId,
                    Title = blog.Title,
                    Content = blog.Content
                }
            };

            return await Task.FromResult(blogResponse);
        }

        public override async Task<Blog.UpdateBlogResponse> UpdateBlog(Blog.UpdateBlogRequest request, ServerCallContext context)
        {
            _logger.LogInformation("Updating blog: " + request.Blog.Id.ToString());

            if (!Guid.TryParse(request.Blog.Id, out Guid blogId))
                throw new RpcException(new Status(StatusCode.InvalidArgument, $"Blog Id {request.Blog.Id} is invalid"));

            var blog = await _blogRepository.GetAsync(blogId);

            if (blog == null)
                throw new RpcException(new Status(StatusCode.NotFound, $"Blog Id {request.Blog.Id} not found"));

            blog.Update(request.Blog.AuthorId, request.Blog.Title, request.Blog.Content);

            await _blogRepository.UpdateAsync(blog);

            var blogResponse = new Blog.UpdateBlogResponse
            {
                Blog = new Blog.Blog
                {
                    Id = blog.Id.ToString(),
                    AuthorId = blog.AuthorId,
                    Title = blog.Title,
                    Content = blog.Content
                }
            };

            return await Task.FromResult(blogResponse);

        }

        public override async Task<Blog.DeleteBlogResponse> DeleteBlog(Blog.DeleteBlogRequest request, ServerCallContext context)
        {
            _logger.LogInformation("Deleting blog: " + request.Id.ToString());

            if (!Guid.TryParse(request.Id, out Guid blogId))
                throw new RpcException(new Status(StatusCode.InvalidArgument, $"Blog Id {request.Id} is invalid"));

            var blog = await _blogRepository.GetAsync(blogId);

            if (blog == null)
                throw new RpcException(new Status(StatusCode.NotFound, $"Blog Id {request.Id} not found"));

            await _blogRepository.DeleteAsync(blogId);

            var blogResponse = new Blog.DeleteBlogResponse
            {
                Success = true
            };

            return await Task.FromResult(blogResponse);
        }
    }
}
