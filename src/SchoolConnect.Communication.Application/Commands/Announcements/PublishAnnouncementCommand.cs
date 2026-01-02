using MediatR;

namespace SchoolConnect.Communication.Application.Commands.Announcements;

public record PublishAnnouncementCommand(
    Guid AnnouncementId,
    int TargetCount
) : IRequest<Unit>;
