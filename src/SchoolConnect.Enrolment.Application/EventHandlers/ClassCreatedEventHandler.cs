using MediatR;
using Microsoft.Extensions.Logging;
using SchoolConnect.Enrolment.Domain.Events;

namespace SchoolConnect.Enrolment.Application.EventHandlers;

public class ClassCreatedEventHandler : INotificationHandler<ClassCreatedEvent>
{
    private readonly ILogger<ClassCreatedEventHandler> _logger;

    public ClassCreatedEventHandler(ILogger<ClassCreatedEventHandler> logger)
    {
        _logger = logger;
    }

    public Task Handle(ClassCreatedEvent notification, CancellationToken cancellationToken)
    {
        _logger.LogInformation(
            "Class created: {Name} for Subject {SubjectId} in Cohort {CohortId} with capacity {Capacity}",
            notification.Name,
            notification.SubjectId,
            notification.CohortId,
            notification.Capacity);

        // Future: Setup class resources, notify curriculum service, create timetable slots
        return Task.CompletedTask;
    }
}
