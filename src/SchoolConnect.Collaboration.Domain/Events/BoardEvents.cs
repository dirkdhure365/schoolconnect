using SchoolConnect.Common.Domain.Primitives;

namespace SchoolConnect.Collaboration.Domain.Events;

public record BoardCreatedEvent(Guid BoardId, Guid WorkspaceId, string Name, Guid CreatedBy) : DomainEvent;
public record BoardUpdatedEvent(Guid BoardId, string Name) : DomainEvent;
public record BoardArchivedEvent(Guid BoardId) : DomainEvent;
public record BoardDeletedEvent(Guid BoardId, Guid WorkspaceId) : DomainEvent;
