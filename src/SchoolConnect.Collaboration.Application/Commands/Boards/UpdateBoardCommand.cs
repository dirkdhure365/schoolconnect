using MediatR;

namespace SchoolConnect.Collaboration.Application.Commands.Boards;

public record UpdateBoardCommand(
    Guid Id,
    string? Name = null,
    string? Description = null
) : IRequest<Unit>;
