using MediatR;
using SchoolConnect.Communication.Domain.Enums;

namespace SchoolConnect.Communication.Application.Commands.Messages;

public record SendMessageCommand(
    Guid ConversationId,
    Guid SenderId,
    string SenderName,
    string Content,
    MessagePriority Priority = MessagePriority.Normal,
    string? SenderAvatarUrl = null,
    List<string>? AttachmentUrls = null,
    Guid? ReplyToMessageId = null
) : IRequest<Guid>;
