using Ardalis.Specification;

namespace ProjectManagement.Company.Api.Abstractions;

public interface IReadRepository<T> : IReadRepositoryBase<T>
    where T : class, IAggregateRoot
{
}