using MediatR;
using Microsoft.Extensions.Logging;
using SchoolConnect.Enrolment.Domain.Events;

namespace SchoolConnect.Enrolment.Application.EventHandlers;

public class StudentUpdatedEventHandler : INotificationHandler<StudentUpdatedEvent>
{
    private readonly ILogger<StudentUpdatedEventHandler> _logger;

    public StudentUpdatedEventHandler(ILogger<StudentUpdatedEventHandler> logger)
    {
        _logger = logger;
    }

    public Task Handle(StudentUpdatedEvent notification, CancellationToken cancellationToken)
    {
        _logger.LogInformation(
            "Student updated: {StudentCode} (AggregateId: {AggregateId})",
            notification.StudentCode,
            notification.AggregateId);

        // Future: Sync updated student data to other services if needed
        return Task.CompletedTask;
    }
}
