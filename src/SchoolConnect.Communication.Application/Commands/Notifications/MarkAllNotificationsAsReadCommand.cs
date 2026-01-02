using MediatR;

namespace SchoolConnect.Communication.Application.Commands.Notifications;

public record MarkAllNotificationsAsReadCommand(
    Guid UserId
) : IRequest<Unit>;
