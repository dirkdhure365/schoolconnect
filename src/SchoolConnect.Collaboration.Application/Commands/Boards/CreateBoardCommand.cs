using MediatR;

namespace SchoolConnect.Collaboration.Application.Commands.Boards;

public record CreateBoardCommand(
    Guid WorkspaceId,
    string Name,
    Guid CreatedBy,
    string? Description = null
) : IRequest<Guid>;
