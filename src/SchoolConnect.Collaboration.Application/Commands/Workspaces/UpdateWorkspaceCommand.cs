using MediatR;
using SchoolConnect.Collaboration.Domain.Enums;

namespace SchoolConnect.Collaboration.Application.Commands.Workspaces;

public record UpdateWorkspaceCommand(
    Guid Id,
    string? Name = null,
    string? Description = null,
    string? LogoUrl = null,
    string? Color = null,
    WorkspaceVisibility? Visibility = null
) : IRequest<Unit>;
