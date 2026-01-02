using MediatR;
using SchoolConnect.Communication.Application.DTOs;

namespace SchoolConnect.Communication.Application.Queries.Announcements;

public record GetAnnouncementsQuery(
    Guid InstituteId,
    int Page = 1,
    int PageSize = 50
) : IRequest<List<AnnouncementDto>>;
