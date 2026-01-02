using SchoolConnect.Common.Domain.Primitives;

namespace SchoolConnect.Collaboration.Domain.Events;

public record WorkspaceCreatedEvent(Guid WorkspaceId, Guid InstituteId, string Name, Guid OwnerId) : DomainEvent;
public record WorkspaceUpdatedEvent(Guid WorkspaceId, string Name) : DomainEvent;
public record WorkspaceDeletedEvent(Guid WorkspaceId) : DomainEvent;
public record WorkspaceMemberAddedEvent(Guid WorkspaceId, Guid UserId, Guid AddedBy) : DomainEvent;
public record WorkspaceMemberRemovedEvent(Guid WorkspaceId, Guid UserId, Guid RemovedBy) : DomainEvent;
public record WorkspaceMemberRoleChangedEvent(Guid WorkspaceId, Guid UserId, string NewRole, Guid ChangedBy) : DomainEvent;
