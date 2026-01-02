namespace SchoolConnect.Calendar.Application.IntegrationEvents;

public record TimetableChangedIntegrationEvent(
    Guid TimetableId,
    Guid ChangeId,
    Guid TimetableSlotId,
    string ChangeType,
    Guid? AffectedTeacherId,
    Guid? AffectedClassId,
    DateTime ChangedAt);
