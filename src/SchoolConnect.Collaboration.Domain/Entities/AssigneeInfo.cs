namespace SchoolConnect.Collaboration.Domain.Entities;

public class AssigneeInfo
{
    public Guid UserId { get; set; }
    public string Name { get; set; } = string.Empty;
    public string? AvatarUrl { get; set; }
}
