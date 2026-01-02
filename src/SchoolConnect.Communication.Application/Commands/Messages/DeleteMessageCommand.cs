using MediatR;

namespace SchoolConnect.Communication.Application.Commands.Messages;

public record DeleteMessageCommand(
    Guid MessageId,
    Guid UserId
) : IRequest<Unit>;
