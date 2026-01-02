using MediatR;

namespace SchoolConnect.Collaboration.Application.Commands.Lists;

public record CreateListCommand(
    Guid BoardId,
    string Name,
    int Position,
    string? Color = null
) : IRequest<Guid>;
