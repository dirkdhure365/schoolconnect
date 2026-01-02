using MediatR;
using SchoolConnect.Communication.Application.DTOs;

namespace SchoolConnect.Communication.Application.Queries.Feed;

public record GetFeedQuery(
    Guid UserId,
    int Page = 1,
    int PageSize = 50
) : IRequest<List<FeedItemDto>>;
