using System.Text.Json;
using Azure.Messaging.ServiceBus;
using Microsoft.Extensions.Logging;
using Microsoft.Extensions.Options;
using SchoolConnect.Common.Infrastructure.Messaging.Contracts;

namespace SchoolConnect.Common.Infrastructure.Messaging.AzureServiceBus;

public class ServiceBusConsumer : IMessageConsumer, IAsyncDisposable
{
    private readonly ServiceBusProcessor _processor;
    private readonly ServiceBusClient _client;
    private readonly ILogger<ServiceBusConsumer> _logger;
    private Func<IntegrationEvent, CancellationToken, Task>? _messageHandler;

    public ServiceBusConsumer(
        IOptions<ServiceBusSettings> settings,
        ILogger<ServiceBusConsumer> logger)
    {
        _logger = logger;
        _client = new ServiceBusClient(settings.Value.ConnectionString);
        _processor = _client.CreateProcessor(
            settings.Value.TopicName,
            settings.Value.SubscriptionName,
            new ServiceBusProcessorOptions
            {
                AutoCompleteMessages = false,
                MaxConcurrentCalls = 1
            });

        _processor.ProcessMessageAsync += ProcessMessageAsync;
        _processor.ProcessErrorAsync += ProcessErrorAsync;
    }

    public async Task StartAsync(Func<IntegrationEvent, CancellationToken, Task> messageHandler, CancellationToken ct = default)
    {
        _messageHandler = messageHandler;
        await _processor.StartProcessingAsync(ct);
        _logger.LogInformation("Service Bus consumer started");
    }

    public async Task StopAsync(CancellationToken ct = default)
    {
        await _processor.StopProcessingAsync(ct);
        _logger.LogInformation("Service Bus consumer stopped");
    }

    private async Task ProcessMessageAsync(ProcessMessageEventArgs args)
    {
        try
        {
            var body = args.Message.Body.ToString();
            var eventTypeName = args.Message.Subject;

            // In a real implementation, you would need a type registry to deserialize to the correct type
            // For now, this is a simplified version
            var integrationEvent = JsonSerializer.Deserialize<IntegrationEvent>(body);

            if (integrationEvent != null && _messageHandler != null)
            {
                await _messageHandler(integrationEvent, args.CancellationToken);
            }

            await args.CompleteMessageAsync(args.Message);

            _logger.LogInformation("Processed message {MessageId}", args.Message.MessageId);
        }
        catch (Exception ex)
        {
            _logger.LogError(ex, "Error processing message {MessageId}", args.Message.MessageId);
            await args.AbandonMessageAsync(args.Message);
        }
    }

    private Task ProcessErrorAsync(ProcessErrorEventArgs args)
    {
        _logger.LogError(args.Exception, "Error in Service Bus processor: {ErrorSource}", args.ErrorSource);
        return Task.CompletedTask;
    }

    public async ValueTask DisposeAsync()
    {
        await _processor.DisposeAsync();
        await _client.DisposeAsync();
    }
}
