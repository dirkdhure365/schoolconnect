using MediatR;
using Microsoft.Extensions.Logging;
using SchoolConnect.Enrolment.Domain.Events;

namespace SchoolConnect.Enrolment.Application.EventHandlers;

public class ApplicationRejectedEventHandler : INotificationHandler<ApplicationRejectedEvent>
{
    private readonly ILogger<ApplicationRejectedEventHandler> _logger;

    public ApplicationRejectedEventHandler(ILogger<ApplicationRejectedEventHandler> logger)
    {
        _logger = logger;
    }

    public Task Handle(ApplicationRejectedEvent notification, CancellationToken cancellationToken)
    {
        _logger.LogInformation(
            "Application {ApplicationNumber} rejected by {RejectedBy} at {RejectedAt}. Reason: {Reason}",
            notification.ApplicationNumber,
            notification.RejectedBy,
            notification.RejectedAt,
            notification.Reason);

        // Future: Send rejection notification to applicant
        return Task.CompletedTask;
    }
}
