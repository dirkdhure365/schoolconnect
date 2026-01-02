using SchoolConnect.Common.Domain.Primitives;

namespace SchoolConnect.Collaboration.Domain.Events;

public record CardCreatedEvent(Guid CardId, Guid ListId, Guid BoardId, string Title, Guid CreatedBy) : DomainEvent;
public record CardUpdatedEvent(Guid CardId, string Title) : DomainEvent;
public record CardMovedEvent(Guid CardId, Guid OldListId, Guid NewListId, int NewPosition) : DomainEvent;
public record CardArchivedEvent(Guid CardId, Guid BoardId) : DomainEvent;
public record CardDeletedEvent(Guid CardId, Guid ListId, Guid BoardId) : DomainEvent;
public record CardAssignedEvent(Guid CardId, Guid UserId) : DomainEvent;
public record CardUnassignedEvent(Guid CardId, Guid UserId) : DomainEvent;
public record CardLabelAddedEvent(Guid CardId, Guid LabelId) : DomainEvent;
public record CardLabelRemovedEvent(Guid CardId, Guid LabelId) : DomainEvent;
public record CardDueDateSetEvent(Guid CardId, DateTime DueDate) : DomainEvent;
public record CardDueDateApproachingEvent(Guid CardId, DateTime DueDate, List<Guid> AssigneeIds) : DomainEvent;
public record CardCommentAddedEvent(Guid CommentId, Guid CardId, Guid AuthorId) : DomainEvent;
public record CardCommentUpdatedEvent(Guid CommentId, Guid CardId) : DomainEvent;
public record CardCommentDeletedEvent(Guid CommentId, Guid CardId) : DomainEvent;
public record ChecklistCreatedEvent(Guid ChecklistId, Guid CardId, string Title) : DomainEvent;
public record ChecklistItemCompletedEvent(Guid ChecklistId, Guid ItemId, Guid CardId, Guid CompletedBy) : DomainEvent;
public record ResourceSharedEvent(Guid ResourceId, Guid WorkspaceId, Enums.ResourceType ResourceType, Guid OriginalResourceId, Guid SharedBy) : DomainEvent;
