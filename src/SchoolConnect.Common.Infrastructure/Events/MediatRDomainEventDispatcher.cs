using MediatR;
using Microsoft.Extensions.Logging;
using SchoolConnect.Common.Domain.Primitives;

namespace SchoolConnect.Common.Infrastructure.Events;

public class MediatRDomainEventDispatcher : IDomainEventDispatcher
{
    private readonly IMediator _mediator;
    private readonly ILogger<MediatRDomainEventDispatcher> _logger;

    public MediatRDomainEventDispatcher(IMediator mediator, ILogger<MediatRDomainEventDispatcher> logger)
    {
        _mediator = mediator;
        _logger = logger;
    }

    public async Task DispatchEventsAsync(IEnumerable<DomainEvent> events, CancellationToken ct = default)
    {
        foreach (var domainEvent in events)
        {
            _logger.LogInformation("Dispatching domain event {EventType} for aggregate {AggregateId}", 
                domainEvent.GetType().Name, domainEvent.AggregateId);
            
            await _mediator.Publish(domainEvent, ct);
        }
    }
}
