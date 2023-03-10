using Ardalis.Specification.EntityFrameworkCore;
using ProjectManagement.CompanyAPI.Abstractions;

namespace ProjectManagement.CompanyAPI.Data;

public class EfRepository<T> : RepositoryBase<T>, IReadRepository<T>, IRepository<T>
    where T : class, IAggregateRoot
{
    public EfRepository(ApplicationDbContext context)
        : base(context)
    {
    }
}