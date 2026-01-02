using MediatR;
using SchoolConnect.Communication.Domain.Enums;

namespace SchoolConnect.Communication.Application.Commands.Notifications;

public record SendNotificationCommand(
    Guid UserId,
    NotificationType Type,
    string Title,
    string Body,
    List<NotificationChannel> Channels,
    Guid? InstituteId = null,
    string? ImageUrl = null,
    string? ActionUrl = null,
    string? ActionText = null,
    DateTime? ExpiresAt = null,
    Guid? SourceId = null,
    string? SourceType = null
) : IRequest<Guid>;
