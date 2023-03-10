using Ardalis.Specification;

namespace ProjectManagement.CompanyAPI.Abstractions;

public interface IReadRepository<T> : IReadRepositoryBase<T>
    where T : class, IAggregateRoot
{
}