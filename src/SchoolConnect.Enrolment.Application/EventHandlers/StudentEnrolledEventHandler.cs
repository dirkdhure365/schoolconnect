using MediatR;
using Microsoft.Extensions.Logging;
using SchoolConnect.Enrolment.Domain.Events;

namespace SchoolConnect.Enrolment.Application.EventHandlers;

public class StudentEnrolledEventHandler : INotificationHandler<StudentEnrolledEvent>
{
    private readonly ILogger<StudentEnrolledEventHandler> _logger;

    public StudentEnrolledEventHandler(ILogger<StudentEnrolledEventHandler> logger)
    {
        _logger = logger;
    }

    public Task Handle(StudentEnrolledEvent notification, CancellationToken cancellationToken)
    {
        _logger.LogInformation(
            "Student {StudentId} enrolled in Stream {StreamId}, Cohort {CohortId} at {EnrolledAt}",
            notification.StudentId,
            notification.StreamId,
            notification.CohortId,
            notification.EnrolledAt);

        // Future: Create billing account for the enrolled student
        // This demonstrates cross-aggregate orchestration via events
        return Task.CompletedTask;
    }
}
