using System;
using System.Collections.Generic;
using System.Threading.Tasks;

namespace GRPC.Final.GrpcService.Domain.Repositories
{
    public interface IBlogRepository
    {
        Task<IEnumerable<Blog>> ListAsync();

        Task<Blog> GetAsync(Guid id);

        Task AddAsync(Blog blog);

        Task UpdateAsync(Blog blog);

        Task DeleteAsync(Guid id);

    }
}
