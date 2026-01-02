using MediatR;

namespace SchoolConnect.Collaboration.Application.Commands.Workspaces;

public record DeleteWorkspaceCommand(Guid Id) : IRequest<Unit>;
