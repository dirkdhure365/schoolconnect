using MediatR;

namespace SchoolConnect.Communication.Application.Commands.Notifications;

public record MarkNotificationAsReadCommand(
    Guid NotificationId
) : IRequest<Unit>;
