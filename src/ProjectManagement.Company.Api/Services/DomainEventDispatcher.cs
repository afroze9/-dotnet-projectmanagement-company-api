using MediatR;
using ProjectManagement.CompanyAPI.Abstractions;
using ProjectManagement.CompanyAPI.Contracts;

namespace ProjectManagement.CompanyAPI.Services;

public class DomainEventDispatcher : IDomainEventDispatcher
{
    private readonly IMediator _mediator;

    public DomainEventDispatcher(IMediator mediator)
    {
        _mediator = mediator;
    }

    public async Task DispatchAndClearEvents(IEnumerable<EntityBase> entitiesWithEvents)
    {
        foreach (EntityBase entity in entitiesWithEvents)
        {
            DomainEventBase[] events = entity.DomainEvents.ToArray();
            entity.ClearDomainEvents();

            foreach (DomainEventBase @event in events)
            {
                await _mediator.Publish(@event).ConfigureAwait(false);
            }
        }
    }
}