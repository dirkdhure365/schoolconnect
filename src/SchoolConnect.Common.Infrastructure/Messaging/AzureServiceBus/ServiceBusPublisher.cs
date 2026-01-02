using System.Text.Json;
using Azure.Messaging.ServiceBus;
using Microsoft.Extensions.Logging;
using Microsoft.Extensions.Options;
using SchoolConnect.Common.Infrastructure.Messaging.Contracts;

namespace SchoolConnect.Common.Infrastructure.Messaging.AzureServiceBus;

public class ServiceBusPublisher : IMessagePublisher, IAsyncDisposable
{
    private readonly ServiceBusSender _sender;
    private readonly ServiceBusClient _client;
    private readonly ILogger<ServiceBusPublisher> _logger;

    public ServiceBusPublisher(
        IOptions<ServiceBusSettings> settings,
        ILogger<ServiceBusPublisher> logger)
    {
        _logger = logger;
        _client = new ServiceBusClient(settings.Value.ConnectionString);
        _sender = _client.CreateSender(settings.Value.TopicName);
    }

    public async Task PublishAsync<T>(T @event, CancellationToken ct = default) where T : IntegrationEvent
    {
        var messageBody = JsonSerializer.Serialize(@event);
        var message = new ServiceBusMessage(messageBody)
        {
            Subject = @event.GetType().Name,
            MessageId = @event.EventId.ToString(),
            CorrelationId = @event.CorrelationId,
            ContentType = "application/json"
        };

        await _sender.SendMessageAsync(message, ct);

        _logger.LogInformation("Published event {EventType} with ID {EventId}", 
            @event.GetType().Name, @event.EventId);
    }

    public async Task PublishManyAsync<T>(IEnumerable<T> events, CancellationToken ct = default) where T : IntegrationEvent
    {
        var eventsList = events.ToList();
        if (!eventsList.Any()) return;

        var messages = eventsList.Select(e =>
        {
            var messageBody = JsonSerializer.Serialize(e);
            return new ServiceBusMessage(messageBody)
            {
                Subject = e.GetType().Name,
                MessageId = e.EventId.ToString(),
                CorrelationId = e.CorrelationId,
                ContentType = "application/json"
            };
        }).ToList();

        await _sender.SendMessagesAsync(messages, ct);

        _logger.LogInformation("Published {Count} events", eventsList.Count);
    }

    public async ValueTask DisposeAsync()
    {
        await _sender.DisposeAsync();
        await _client.DisposeAsync();
    }
}
