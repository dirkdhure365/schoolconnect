using MediatR;
using Microsoft.Extensions.Logging;
using SchoolConnect.Enrolment.Domain.Events;

namespace SchoolConnect.Enrolment.Application.EventHandlers;

public class StudentWithdrawnEventHandler : INotificationHandler<StudentWithdrawnEvent>
{
    private readonly ILogger<StudentWithdrawnEventHandler> _logger;

    public StudentWithdrawnEventHandler(ILogger<StudentWithdrawnEventHandler> logger)
    {
        _logger = logger;
    }

    public Task Handle(StudentWithdrawnEvent notification, CancellationToken cancellationToken)
    {
        _logger.LogInformation(
            "Student {StudentId} withdrawn at {WithdrawnAt}. Reason: {Reason}",
            notification.StudentId,
            notification.WithdrawnAt,
            notification.Reason);

        // Future: Update billing accounts, notify relevant parties, update class rosters
        return Task.CompletedTask;
    }
}
