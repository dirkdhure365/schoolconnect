using MediatR;

namespace SchoolConnect.Communication.Application.Commands.Announcements;

public record AcknowledgeAnnouncementCommand(
    Guid AnnouncementId,
    Guid UserId,
    string UserName,
    string? UserRole = null,
    string? IpAddress = null,
    string? DeviceInfo = null
) : IRequest<Unit>;
