using SchoolConnect.Common.Domain.Primitives;
using SchoolConnect.Collaboration.Domain.Enums;
using SchoolConnect.Collaboration.Domain.Events;

namespace SchoolConnect.Collaboration.Domain.Entities;

public class Workspace : AggregateRoot
{
    public Guid InstituteId { get; private set; }
    public Guid? CentreId { get; private set; }
    
    public string Name { get; private set; } = string.Empty;
    public string? Description { get; private set; }
    public string? LogoUrl { get; private set; }
    public string? Color { get; private set; }
    
    public WorkspaceVisibility Visibility { get; private set; }
    public WorkspaceStatus Status { get; private set; }
    
    public Guid OwnerId { get; private set; }
    public string OwnerName { get; private set; } = string.Empty;
    
    public WorkspaceSettings Settings { get; private set; } = new();
    
    public int BoardCount { get; private set; }
    public int MemberCount { get; private set; }

    private Workspace() { }

    public static Workspace Create(
        Guid instituteId,
        string name,
        Guid ownerId,
        string ownerName,
        Guid? centreId = null,
        string? description = null,
        string? logoUrl = null,
        string? color = null,
        WorkspaceVisibility visibility = WorkspaceVisibility.Private,
        WorkspaceSettings? settings = null)
    {
        var workspace = new Workspace
        {
            Id = Guid.NewGuid(),
            InstituteId = instituteId,
            CentreId = centreId,
            Name = name,
            Description = description,
            LogoUrl = logoUrl,
            Color = color,
            Visibility = visibility,
            Status = WorkspaceStatus.Active,
            OwnerId = ownerId,
            OwnerName = ownerName,
            Settings = settings ?? new WorkspaceSettings(),
            BoardCount = 0,
            MemberCount = 1,
            CreatedAt = DateTime.UtcNow,
            UpdatedAt = DateTime.UtcNow
        };

        workspace.Apply(new WorkspaceCreatedEvent(workspace.Id, instituteId, name, ownerId));
        return workspace;
    }

    public void Update(
        string? name = null,
        string? description = null,
        string? logoUrl = null,
        string? color = null,
        WorkspaceVisibility? visibility = null)
    {
        if (name != null) Name = name;
        if (description != null) Description = description;
        if (logoUrl != null) LogoUrl = logoUrl;
        if (color != null) Color = color;
        if (visibility.HasValue) Visibility = visibility.Value;
        
        UpdatedAt = DateTime.UtcNow;
        Apply(new WorkspaceUpdatedEvent(Id, Name));
    }

    public void Delete()
    {
        Status = WorkspaceStatus.Deleted;
        UpdatedAt = DateTime.UtcNow;
        Apply(new WorkspaceDeletedEvent(Id));
    }

    public void IncrementBoardCount()
    {
        BoardCount++;
        UpdatedAt = DateTime.UtcNow;
    }

    public void DecrementBoardCount()
    {
        if (BoardCount > 0) BoardCount--;
        UpdatedAt = DateTime.UtcNow;
    }

    public void IncrementMemberCount()
    {
        MemberCount++;
        UpdatedAt = DateTime.UtcNow;
    }

    public void DecrementMemberCount()
    {
        if (MemberCount > 0) MemberCount--;
        UpdatedAt = DateTime.UtcNow;
    }

    protected override void When(DomainEvent @event)
    {
        // Event sourcing implementation if needed
    }
}
