namespace SchoolConnect.Identity.Application.DTOs;

public class RoleDto
{
    public Guid Id { get; set; }
    public string Name { get; set; } = string.Empty;
    public string? Description { get; set; }
    public bool IsSystemRole { get; set; }
    public List<Guid> PermissionIds { get; set; } = [];
    public DateTime CreatedAt { get; set; }
}
