using Microsoft.Extensions.DependencyInjection;
using Microsoft.Extensions.Logging;
using SchoolConnect.Common.Domain.Interfaces;
using SchoolConnect.Common.Domain.Primitives;

namespace SchoolConnect.Common.Infrastructure.EventDispatcher;

public class DomainEventDispatcher : IDomainEventDispatcher
{
    private readonly IServiceProvider _serviceProvider;
    private readonly ILogger<DomainEventDispatcher> _logger;

    public DomainEventDispatcher(
        IServiceProvider serviceProvider,
        ILogger<DomainEventDispatcher> logger)
    {
        _serviceProvider = serviceProvider;
        _logger = logger;
    }

    public async Task DispatchAsync(IEnumerable<DomainEvent> events, CancellationToken ct = default)
    {
        foreach (var @event in events)
        {
            await DispatchAsync(@event, ct);
        }
    }

    public async Task DispatchAsync(DomainEvent @event, CancellationToken ct = default)
    {
        try
        {
            _logger.LogInformation(
                "Dispatching domain event {EventType} for aggregate {AggregateId}",
                @event.GetType().Name,
                @event.AggregateId);

            var eventType = @event.GetType();
            var handlerType = typeof(IDomainEventHandler<>).MakeGenericType(eventType);

            using var scope = _serviceProvider.CreateScope();
            var handlers = scope.ServiceProvider.GetServices(handlerType);

            if (!handlers.Any())
            {
                _logger.LogWarning(
                    "No handlers registered for event type {EventType}",
                    eventType.Name);
                return;
            }

            foreach (var handler in handlers)
            {
                var handleMethod = handlerType.GetMethod("Handle");
                if (handleMethod != null)
                {
                    var task = (Task?)handleMethod.Invoke(handler, new object?[] { @event, ct });
                    if (task != null)
                    {
                        await task;
                    }
                }
            }

            _logger.LogInformation(
                "Successfully dispatched domain event {EventType} to {HandlerCount} handler(s)",
                eventType.Name,
                handlers.Count());
        }
        catch (Exception ex)
        {
            _logger.LogError(
                ex,
                "Error dispatching domain event {EventType} for aggregate {AggregateId}",
                @event.GetType().Name,
                @event.AggregateId);
            throw;
        }
    }
}
