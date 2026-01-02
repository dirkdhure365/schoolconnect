using MediatR;

namespace SchoolConnect.Collaboration.Application.Commands.Cards;

public record CreateCardCommand(
    Guid ListId,
    Guid BoardId,
    string Title,
    Guid CreatedBy,
    int Position,
    string? Description = null
) : IRequest<Guid>;
