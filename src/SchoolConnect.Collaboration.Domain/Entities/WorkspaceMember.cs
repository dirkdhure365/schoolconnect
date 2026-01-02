using SchoolConnect.Common.Domain.Primitives;
using SchoolConnect.Collaboration.Domain.Enums;
using SchoolConnect.Collaboration.Domain.ValueObjects;
using SchoolConnect.Collaboration.Domain.Events;

namespace SchoolConnect.Collaboration.Domain.Entities;

public class WorkspaceMember : Entity
{
    public Guid WorkspaceId { get; private set; }
    public Guid UserId { get; private set; }
    public string UserName { get; private set; } = string.Empty;
    public string? UserEmail { get; private set; }
    public string? AvatarUrl { get; private set; }
    
    public MemberRole Role { get; private set; }
    public MemberPermissions Permissions { get; private set; } = MemberPermissions.DefaultMemberPermissions();
    
    public DateTime JoinedAt { get; private set; }
    public Guid InvitedBy { get; private set; }
    public DateTime? LastActiveAt { get; private set; }

    private WorkspaceMember() { }

    public static WorkspaceMember Create(
        Guid workspaceId,
        Guid userId,
        string userName,
        MemberRole role,
        Guid invitedBy,
        string? userEmail = null,
        string? avatarUrl = null)
    {
        var member = new WorkspaceMember
        {
            Id = Guid.NewGuid(),
            WorkspaceId = workspaceId,
            UserId = userId,
            UserName = userName,
            UserEmail = userEmail,
            AvatarUrl = avatarUrl,
            Role = role,
            Permissions = GetPermissionsForRole(role),
            JoinedAt = DateTime.UtcNow,
            InvitedBy = invitedBy,
            CreatedAt = DateTime.UtcNow,
            UpdatedAt = DateTime.UtcNow
        };

        return member;
    }

    public void UpdateRole(MemberRole newRole)
    {
        Role = newRole;
        Permissions = GetPermissionsForRole(newRole);
        UpdatedAt = DateTime.UtcNow;
    }

    public void UpdateLastActive()
    {
        LastActiveAt = DateTime.UtcNow;
    }

    private static MemberPermissions GetPermissionsForRole(MemberRole role) => role switch
    {
        MemberRole.Owner => MemberPermissions.OwnerPermissions(),
        MemberRole.Admin => MemberPermissions.AdminPermissions(),
        MemberRole.Member => MemberPermissions.DefaultMemberPermissions(),
        MemberRole.Guest => MemberPermissions.GuestPermissions(),
        _ => MemberPermissions.GuestPermissions()
    };
}
