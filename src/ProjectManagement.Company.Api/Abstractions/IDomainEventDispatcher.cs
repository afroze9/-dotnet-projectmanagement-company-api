using ProjectManagement.Company.Api.Common;

namespace ProjectManagement.Company.Api.Abstractions;

public interface IDomainEventDispatcher
{
    Task DispatchAndClearEvents(IEnumerable<EntityBase> entitiesWithEvents);
}