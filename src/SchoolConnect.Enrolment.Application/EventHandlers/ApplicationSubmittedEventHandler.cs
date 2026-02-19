using MediatR;
using Microsoft.Extensions.Logging;
using SchoolConnect.Enrolment.Domain.Events;

namespace SchoolConnect.Enrolment.Application.EventHandlers;

public class ApplicationSubmittedEventHandler : INotificationHandler<ApplicationSubmittedEvent>
{
    private readonly ILogger<ApplicationSubmittedEventHandler> _logger;

    public ApplicationSubmittedEventHandler(ILogger<ApplicationSubmittedEventHandler> logger)
    {
        _logger = logger;
    }

    public Task Handle(ApplicationSubmittedEvent notification, CancellationToken cancellationToken)
    {
        _logger.LogInformation(
            "Application {ApplicationNumber} submitted by {FirstName} {LastName} for Program {ProgramOfferingId} at {SubmittedAt}",
            notification.ApplicationNumber,
            notification.FirstName,
            notification.LastName,
            notification.ProgramOfferingId,
            notification.SubmittedAt);

        // Future: Send confirmation email, notify admissions officers
        return Task.CompletedTask;
    }
}
