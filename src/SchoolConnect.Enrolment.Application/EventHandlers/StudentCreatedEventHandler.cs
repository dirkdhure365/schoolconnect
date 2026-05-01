using MediatR;
using Microsoft.Extensions.Logging;
using SchoolConnect.Enrolment.Domain.Events;

namespace SchoolConnect.Enrolment.Application.EventHandlers;

public class StudentCreatedEventHandler : INotificationHandler<StudentCreatedEvent>
{
    private readonly ILogger<StudentCreatedEventHandler> _logger;

    public StudentCreatedEventHandler(ILogger<StudentCreatedEventHandler> logger)
    {
        _logger = logger;
    }

    public Task Handle(StudentCreatedEvent notification, CancellationToken cancellationToken)
    {
        _logger.LogInformation(
            "Student created: {StudentCode} ({FirstName} {LastName}) at Institute {InstituteId}",
            notification.StudentCode,
            notification.FirstName,
            notification.LastName,
            notification.InstituteId);

        // Future: Trigger billing account creation, send welcome notification, etc.
        return Task.CompletedTask;
    }
}
