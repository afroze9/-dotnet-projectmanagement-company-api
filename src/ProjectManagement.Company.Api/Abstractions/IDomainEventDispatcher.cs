using ProjectManagement.CompanyAPI.Contracts;

namespace ProjectManagement.CompanyAPI.Abstractions;

public interface IDomainEventDispatcher
{
    Task DispatchAndClearEvents(IEnumerable<EntityBase> entitiesWithEvents);
}