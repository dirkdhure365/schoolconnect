using SchoolConnect.Collaboration.Domain.Enums;

namespace SchoolConnect.Collaboration.Application.DTOs;

public class WorkspaceDto
{
    public Guid Id { get; set; }
    public Guid InstituteId { get; set; }
    public Guid? CentreId { get; set; }
    public string Name { get; set; } = string.Empty;
    public string? Description { get; set; }
    public string? LogoUrl { get; set; }
    public string? Color { get; set; }
    public WorkspaceVisibility Visibility { get; set; }
    public WorkspaceStatus Status { get; set; }
    public Guid OwnerId { get; set; }
    public string OwnerName { get; set; } = string.Empty;
    public int BoardCount { get; set; }
    public int MemberCount { get; set; }
    public DateTime CreatedAt { get; set; }
    public DateTime UpdatedAt { get; set; }
}

public class WorkspaceSummaryDto
{
    public Guid Id { get; set; }
    public string Name { get; set; } = string.Empty;
    public string? LogoUrl { get; set; }
    public string? Color { get; set; }
    public int BoardCount { get; set; }
    public int MemberCount { get; set; }
}

public class WorkspaceMemberDto
{
    public Guid Id { get; set; }
    public Guid WorkspaceId { get; set; }
    public Guid UserId { get; set; }
    public string UserName { get; set; } = string.Empty;
    public string? UserEmail { get; set; }
    public string? AvatarUrl { get; set; }
    public MemberRole Role { get; set; }
    public DateTime JoinedAt { get; set; }
    public DateTime? LastActiveAt { get; set; }
}
