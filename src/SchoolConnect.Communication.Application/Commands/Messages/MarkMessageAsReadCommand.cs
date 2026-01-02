using MediatR;

namespace SchoolConnect.Communication.Application.Commands.Messages;

public record MarkMessageAsReadCommand(
    Guid MessageId,
    Guid UserId
) : IRequest<Unit>;
