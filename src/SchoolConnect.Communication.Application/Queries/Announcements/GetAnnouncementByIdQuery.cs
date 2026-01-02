using MediatR;
using SchoolConnect.Communication.Application.DTOs;

namespace SchoolConnect.Communication.Application.Queries.Announcements;

public record GetAnnouncementByIdQuery(
    Guid AnnouncementId
) : IRequest<AnnouncementDto?>;
