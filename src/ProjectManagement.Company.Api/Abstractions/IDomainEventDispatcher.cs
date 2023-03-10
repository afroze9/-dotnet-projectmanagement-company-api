using ProjectManagement.CompanyAPI.Common;

namespace ProjectManagement.CompanyAPI.Abstractions;

public interface IDomainEventDispatcher
{
    Task DispatchAndClearEvents(IEnumerable<EntityBase> entitiesWithEvents);
}