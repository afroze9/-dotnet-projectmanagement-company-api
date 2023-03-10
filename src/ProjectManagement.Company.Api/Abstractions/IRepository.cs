using Ardalis.Specification;

namespace ProjectManagement.Company.Api.Abstractions;

public interface IRepository<T> : IRepositoryBase<T>
    where T : class, IAggregateRoot
{
}