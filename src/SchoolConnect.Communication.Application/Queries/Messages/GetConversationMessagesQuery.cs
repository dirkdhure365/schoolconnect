using MediatR;
using SchoolConnect.Communication.Application.DTOs;

namespace SchoolConnect.Communication.Application.Queries.Messages;

public record GetConversationMessagesQuery(
    Guid ConversationId,
    int Page = 1,
    int PageSize = 50
) : IRequest<List<MessageDto>>;
