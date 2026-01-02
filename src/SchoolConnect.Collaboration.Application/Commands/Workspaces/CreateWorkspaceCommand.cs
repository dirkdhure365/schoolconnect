using MediatR;
using SchoolConnect.Collaboration.Domain.Enums;

namespace SchoolConnect.Collaboration.Application.Commands.Workspaces;

public record CreateWorkspaceCommand(
    Guid InstituteId,
    string Name,
    Guid OwnerId,
    string OwnerName,
    Guid? CentreId = null,
    string? Description = null,
    string? LogoUrl = null,
    string? Color = null,
    WorkspaceVisibility Visibility = WorkspaceVisibility.Private
) : IRequest<Guid>;
