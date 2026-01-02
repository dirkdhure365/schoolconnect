using SchoolConnect.Common.Domain.Primitives;

namespace SchoolConnect.Collaboration.Domain.Events;

public record ListCreatedEvent(Guid ListId, Guid BoardId, string Name, int Position) : DomainEvent;
public record ListUpdatedEvent(Guid ListId, string Name) : DomainEvent;
public record ListArchivedEvent(Guid ListId, Guid BoardId) : DomainEvent;
public record ListMovedEvent(Guid ListId, Guid BoardId, int NewPosition) : DomainEvent;
