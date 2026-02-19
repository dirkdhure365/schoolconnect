using MediatR;
using Microsoft.Extensions.Logging;
using SchoolConnect.Enrolment.Domain.Events;

namespace SchoolConnect.Enrolment.Application.EventHandlers;

public class ApplicationApprovedEventHandler : INotificationHandler<ApplicationApprovedEvent>
{
    private readonly ILogger<ApplicationApprovedEventHandler> _logger;

    public ApplicationApprovedEventHandler(ILogger<ApplicationApprovedEventHandler> logger)
    {
        _logger = logger;
    }

    public Task Handle(ApplicationApprovedEvent notification, CancellationToken cancellationToken)
    {
        _logger.LogInformation(
            "Application {ApplicationNumber} approved by {ApprovedBy} at {ApprovedAt}",
            notification.ApplicationNumber,
            notification.ApprovedBy,
            notification.ApprovedAt);

        // Future: Dispatch a CreateStudentCommand here to complete the workflow
        
        return Task.CompletedTask;
    }
}
