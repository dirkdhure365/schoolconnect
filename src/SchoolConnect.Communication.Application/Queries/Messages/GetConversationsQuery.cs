using MediatR;
using SchoolConnect.Communication.Application.DTOs;

namespace SchoolConnect.Communication.Application.Queries.Messages;

public record GetConversationsQuery(
    Guid UserId
) : IRequest<List<ConversationSummaryDto>>;
