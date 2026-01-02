using MediatR;
using SchoolConnect.Communication.Domain.Enums;

namespace SchoolConnect.Communication.Application.Commands.Conversations;

public record CreateConversationCommand(
    ConversationType Type,
    List<Guid> ParticipantUserIds,
    string? Title = null,
    Guid? InstituteId = null,
    Guid? ClassId = null
) : IRequest<Guid>;
