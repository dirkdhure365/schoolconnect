using MediatR;
using SchoolConnect.Collaboration.Domain.Enums;

namespace SchoolConnect.Collaboration.Application.Commands.Workspaces;

public record AddWorkspaceMemberCommand(
    Guid WorkspaceId,
    Guid UserId,
    string UserName,
    MemberRole Role,
    Guid InvitedBy,
    string? UserEmail = null,
    string? AvatarUrl = null
) : IRequest<Guid>;
