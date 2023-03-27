using ProjectManagement.CompanyAPI.Contracts;

namespace ProjectManagement.CompanyAPI.Abstractions;

/// <summary>
///     Represents a message publisher that can publish integration events.
/// </summary>
public interface IMessagePublisher
{
    /// <summary>
    ///     Publishes an integration event.
    /// </summary>
    /// <param name="event">The integration event to publish.</param>
    /// <returns>A task that represents the asynchronous operation.</returns>
    Task PublishAsync(IntegrationEvent @event);

    /// <summary>
    ///     Publishes an integration event using the specified routing key.
    /// </summary>
    /// <param name="event">The integration event to publish.</param>
    /// <param name="routingKey">The routing key to use.</param>
    /// <returns>A task that represents the asynchronous operation.</returns>
    Task PublishAsync(IntegrationEvent @event, string routingKey);
}