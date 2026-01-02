using MediatR;
using SchoolConnect.Communication.Application.DTOs;

namespace SchoolConnect.Communication.Application.Queries.Notifications;

public record GetNotificationsQuery(
    Guid UserId,
    int Page = 1,
    int PageSize = 50
) : IRequest<List<NotificationDto>>;
