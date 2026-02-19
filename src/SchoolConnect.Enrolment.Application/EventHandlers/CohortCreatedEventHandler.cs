using MediatR;
using Microsoft.Extensions.Logging;
using SchoolConnect.Enrolment.Domain.Events;

namespace SchoolConnect.Enrolment.Application.EventHandlers;

public class CohortCreatedEventHandler : INotificationHandler<CohortCreatedEvent>
{
    private readonly ILogger<CohortCreatedEventHandler> _logger;

    public CohortCreatedEventHandler(ILogger<CohortCreatedEventHandler> logger)
    {
        _logger = logger;
    }

    public Task Handle(CohortCreatedEvent notification, CancellationToken cancellationToken)
    {
        _logger.LogInformation(
            "Cohort created: {Name} for Stream {StreamId} (AggregateId: {AggregateId})",
            notification.Name,
            notification.StreamId,
            notification.AggregateId);

        // Future: Initialize default classes, setup billing templates, notify staff
        return Task.CompletedTask;
    }
}
