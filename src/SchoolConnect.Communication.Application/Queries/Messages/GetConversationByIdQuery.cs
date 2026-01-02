using MediatR;
using SchoolConnect.Communication.Application.DTOs;

namespace SchoolConnect.Communication.Application.Queries.Messages;

public record GetConversationByIdQuery(
    Guid ConversationId
) : IRequest<ConversationDto?>;
