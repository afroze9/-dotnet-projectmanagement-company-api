using Ardalis.Specification.EntityFrameworkCore;
using ProjectManagement.Company.Api.Abstractions;

namespace ProjectManagement.Company.Api.Data;

public class EfRepository<T> : RepositoryBase<T>, IReadRepository<T>, IRepository<T>
    where T : class, IAggregateRoot
{
    public EfRepository(ApplicationDbContext context)
        :base(context)
    {
    }    
}