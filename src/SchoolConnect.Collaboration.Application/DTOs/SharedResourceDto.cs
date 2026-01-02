using SchoolConnect.Collaboration.Domain.Enums;

namespace SchoolConnect.Collaboration.Application.DTOs;

public class SharedResourceDto
{
    public Guid Id { get; set; }
    public Guid WorkspaceId { get; set; }
    public ResourceType ResourceType { get; set; }
    public Guid ResourceId { get; set; }
    public string ResourceName { get; set; } = string.Empty;
    public string? ResourceUrl { get; set; }
    public Guid SharedBy { get; set; }
    public string SharedByName { get; set; } = string.Empty;
    public DateTime SharedAt { get; set; }
    public string? Notes { get; set; }
    public List<string> Tags { get; set; } = [];
    public int ViewCount { get; set; }
    public int DownloadCount { get; set; }
}
