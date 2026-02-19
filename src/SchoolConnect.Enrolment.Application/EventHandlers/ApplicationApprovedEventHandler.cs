using MediatR;
using Microsoft.Extensions.Logging;
using SchoolConnect.Enrolment.Domain.Events;

namespace SchoolConnect.Enrolment.Application.EventHandlers;

public class ApplicationApprovedEventHandler : INotificationHandler<ApplicationApprovedEvent>
{
    private readonly ILogger<ApplicationApprovedEventHandler> _logger;
    private readonly IMediator _mediator;

    public ApplicationApprovedEventHandler(
        ILogger<ApplicationApprovedEventHandler> logger,
        IMediator mediator)
    {
        _logger = logger;
        _mediator = mediator;
    }

    public Task Handle(ApplicationApprovedEvent notification, CancellationToken cancellationToken)
    {
        _logger.LogInformation(
            "Application {ApplicationNumber} approved by {ApprovedBy} at {ApprovedAt}. Creating student record...",
            notification.ApplicationNumber,
            notification.ApprovedBy,
            notification.ApprovedAt);

        // Future: Dispatch a CreateStudentCommand here to complete the workflow
        // await _mediator.Send(new CreateStudentCommand { ... }, cancellationToken);
        
        return Task.CompletedTask;
    }
}
