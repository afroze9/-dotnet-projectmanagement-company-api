using MediatR;
using ProjectManagement.Company.Api.Abstractions;
using ProjectManagement.Company.Api.Common;

namespace ProjectManagement.Company.Api.Services;

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