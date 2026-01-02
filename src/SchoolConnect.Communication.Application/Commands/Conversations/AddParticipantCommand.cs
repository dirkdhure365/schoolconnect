using MediatR;

namespace SchoolConnect.Communication.Application.Commands.Conversations;

public record AddParticipantCommand(
    Guid ConversationId,
    Guid UserId,
    string UserName,
    Guid AddedBy,
    string? AvatarUrl = null
) : IRequest<Unit>;
