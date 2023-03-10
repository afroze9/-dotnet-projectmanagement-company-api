using System.Collections;
using System.ComponentModel.DataAnnotations.Schema;

namespace ProjectManagement.Company.Api.Common;

public abstract class EntityBase
{
    public int Id { get; set; }

    private List<DomainEventBase> _domainEvents = new ();

    [NotMapped]
    public IEnumerable<DomainEventBase> DomainEvents => _domainEvents.AsReadOnly();

    protected void RegisterDomainEvent(DomainEventBase domainEvent) => _domainEvents.Add(domainEvent);

    internal void ClearDomainEvents() => _domainEvents.Clear();
}