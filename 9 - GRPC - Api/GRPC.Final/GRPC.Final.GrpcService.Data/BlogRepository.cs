using GRPC.Final.GrpcService.Domain;
using GRPC.Final.GrpcService.Domain.Repositories;
using MongoDB.Bson;
using MongoDB.Driver;
using System;
using System.Collections.Generic;
using System.Threading.Tasks;

namespace GRPC.Final.GrpcService.Data
{
    public class BlogRepository : IBlogRepository
    {
        private readonly MongoContext _context;
        public BlogRepository(MongoContext context)
        {
            _context = context;
        }

        public async Task<IEnumerable<Blog>> ListAsync()
        {
            return await _context.Blogs.Find(new BsonDocument()).ToListAsync();
        }

        public async Task<Blog> GetAsync(Guid id)
        {
            return await _context.Blogs.Find(x => x.Id == id).FirstOrDefaultAsync();
        }

        public async Task AddAsync(Blog blog)
        {
            await _context.Blogs.InsertOneAsync(blog);
        }

        public async Task UpdateAsync(Blog blog)
        {
            await _context.Blogs.ReplaceOneAsync(x => x.Id == blog.Id, blog);
        }

        public async Task DeleteAsync(Guid id)
        {
            await _context.Blogs.DeleteOneAsync(x => x.Id == id);
        }


    }
}
