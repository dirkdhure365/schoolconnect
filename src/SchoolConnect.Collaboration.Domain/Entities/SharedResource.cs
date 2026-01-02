using SchoolConnect.Common.Domain.Primitives;
using SchoolConnect.Collaboration.Domain.Enums;
using SchoolConnect.Collaboration.Domain.Events;

namespace SchoolConnect.Collaboration.Domain.Entities;

public class SharedResource : AggregateRoot
{
    public Guid WorkspaceId { get; private set; }
    
    public ResourceType ResourceType { get; private set; }
    public Guid ResourceId { get; private set; }
    public string ResourceName { get; private set; } = string.Empty;
    public string? ResourceUrl { get; private set; }
    
    public Guid SharedBy { get; private set; }
    public string SharedByName { get; private set; } = string.Empty;
    public DateTime SharedAt { get; private set; }
    
    public string? Notes { get; private set; }
    public List<string> Tags { get; private set; } = [];
    
    public int ViewCount { get; private set; }
    public int DownloadCount { get; private set; }

    private SharedResource() { }

    public static SharedResource Create(
        Guid workspaceId,
        ResourceType resourceType,
        Guid resourceId,
        string resourceName,
        Guid sharedBy,
        string sharedByName,
        string? resourceUrl = null,
        string? notes = null,
        List<string>? tags = null)
    {
        var resource = new SharedResource
        {
            Id = Guid.NewGuid(),
            WorkspaceId = workspaceId,
            ResourceType = resourceType,
            ResourceId = resourceId,
            ResourceName = resourceName,
            ResourceUrl = resourceUrl,
            SharedBy = sharedBy,
            SharedByName = sharedByName,
            SharedAt = DateTime.UtcNow,
            Notes = notes,
            Tags = tags ?? [],
            ViewCount = 0,
            DownloadCount = 0,
            CreatedAt = DateTime.UtcNow,
            UpdatedAt = DateTime.UtcNow
        };

        resource.Apply(new ResourceSharedEvent(resource.Id, workspaceId, resourceType, resourceId, sharedBy));
        return resource;
    }

    public void IncrementViewCount()
    {
        ViewCount++;
        UpdatedAt = DateTime.UtcNow;
    }

    public void IncrementDownloadCount()
    {
        DownloadCount++;
        UpdatedAt = DateTime.UtcNow;
    }

    protected override void When(DomainEvent @event)
    {
        // Event sourcing implementation if needed
    }
}
