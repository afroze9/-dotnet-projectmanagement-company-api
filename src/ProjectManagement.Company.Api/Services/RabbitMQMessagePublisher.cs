using System.Text.Json;
using ProjectManagement.CompanyAPI.Abstractions;
using ProjectManagement.CompanyAPI.Contracts;
using Steeltoe.Messaging.RabbitMQ.Core;

namespace ProjectManagement.CompanyAPI.Services;

/// <summary>
///     Represents a message publisher for RabbitMQ.
/// </summary>
public class RabbitMQMessagePublisher : IMessagePublisher
{
    private readonly RabbitTemplate _rabbitTemplate;
    private readonly ILogger<RabbitMQMessagePublisher> _logger;

    /// <summary>
    ///     Initializes a new instance of the <see cref="RabbitMQMessagePublisher" /> class.
    /// </summary>
    /// <param name="rabbitTemplate">The RabbitMQ template used to send messages.</param>
    /// <param name="logger">The logger used for logging.</param>
    public RabbitMQMessagePublisher(RabbitTemplate rabbitTemplate, ILogger<RabbitMQMessagePublisher> logger)
    {
        _rabbitTemplate = rabbitTemplate;
        _logger = logger;
    }

    /// <summary>
    ///     Publishes an integration event to RabbitMQ.
    /// </summary>
    /// <param name="event">The integration event to publish.</param>
    /// <returns>A task that represents the asynchronous operation.</returns>
    /// <exception cref="ArgumentNullException">Thrown when the <paramref name="event" /> is null.</exception>
    public async Task PublishAsync(IntegrationEvent @event)
    {
        if (@event == null)
        {
            throw new ArgumentNullException(nameof(@event));
        }

        try
        {
            await _rabbitTemplate.ConvertAndSendAsync(@event.GetType().Name, JsonSerializer.Serialize(@event));
        }
        catch (Exception ex)
        {
            _logger.LogError(ex, "Error publishing message to RabbitMQ");
            throw;
        }
    }

    /// <summary>
    ///     Publishes an integration event to RabbitMQ using the specified routing key.
    /// </summary>
    /// <param name="event">The integration event to publish.</param>
    /// <param name="routingKey">The routing key to use.</param>
    /// <returns>A task that represents the asynchronous operation.</returns>
    /// <exception cref="ArgumentNullException">Thrown when the <paramref name="event" /> is null.</exception>
    public async Task PublishAsync(IntegrationEvent @event, string routingKey)
    {
        if (@event == null)
        {
            throw new ArgumentNullException(nameof(@event));
        }

        try
        {
            await _rabbitTemplate.ConvertAndSendAsync(@event.GetType().Name, routingKey,
                JsonSerializer.Serialize(@event));
        }
        catch (Exception ex)
        {
            _logger.LogError(ex, "Error publishing message to RabbitMQ");
            throw;
        }
    }
}
