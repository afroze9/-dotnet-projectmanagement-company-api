using MediatR;

namespace ProjectManagement.CompanyAPI.Contracts;

public abstract class DomainEventBase : INotification
{
    protected DomainEventBase()
    {
        DateOccurred = DateTime.UtcNow;
    }

    public DateTime DateOccurred { get; }
}