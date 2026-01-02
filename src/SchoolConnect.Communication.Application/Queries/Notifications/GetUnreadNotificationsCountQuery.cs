using MediatR;

namespace SchoolConnect.Communication.Application.Queries.Notifications;

public record GetUnreadNotificationsCountQuery(
    Guid UserId
) : IRequest<int>;
