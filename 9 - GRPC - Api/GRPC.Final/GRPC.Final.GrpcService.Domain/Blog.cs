using System;

namespace GRPC.Final.GrpcService.Domain
{
    public class Blog
    {
        public Guid Id { get; private set; }
        public string AuthorId { get; private set; }
        public string Title { get; private set; }
        public string Content { get; private set; }

        private Blog(string authorId, string title, string content)
        {
            Id = Guid.NewGuid();
            AuthorId = authorId ?? throw new ArgumentNullException(nameof(authorId));
            Title = title ?? throw new ArgumentNullException(nameof(title));
            Content = content ?? throw new ArgumentNullException(nameof(content));
        }

        public static Blog Factory(string authorId, string title, string content)
        {
            return new Blog(authorId, title, content);
        }

        public void Update(string authorId, string title, string content)
        {
            AuthorId = authorId;
            Title = title;
            Content = content;
        }
    }
}
