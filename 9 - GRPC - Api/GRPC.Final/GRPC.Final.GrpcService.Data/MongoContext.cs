using GRPC.Final.GrpcService.Domain;
using Microsoft.Extensions.Options;
using MongoDB.Driver;

namespace GRPC.Final.GrpcService.Data
{
    public class MongoContext
    {
        private readonly IMongoDatabase _database;

        public MongoContext(IOptions<MongoConfig> options)
        {
            var mongoClient = new MongoClient(options.Value.ConnectionString);
            _database = mongoClient.GetDatabase(options.Value.Database);
        }

        public IMongoCollection<Blog> Blogs => _database.GetCollection<Blog>("Blogs");
    }
}
