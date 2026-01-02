using MediatR;

namespace SchoolConnect.Communication.Application.Queries.Messages;

public record GetUnreadCountQuery(
    Guid UserId
) : IRequest<int>;
