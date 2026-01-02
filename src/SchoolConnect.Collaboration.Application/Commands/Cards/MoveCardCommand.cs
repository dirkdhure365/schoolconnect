using MediatR;

namespace SchoolConnect.Collaboration.Application.Commands.Cards;

public record MoveCardCommand(
    Guid Id,
    Guid NewListId,
    int NewPosition
) : IRequest<Unit>;
