using MediatR;

namespace ProjectManagement.CompanyAPI.Common;

public abstract class DomainEventBase : INotification
{
    public DateTime DateOccurred { get; protected set; } = DateTime.UtcNow;
}