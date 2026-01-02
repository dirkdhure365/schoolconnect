using SchoolConnect.Calendar.Domain.Entities;

namespace SchoolConnect.Calendar.Domain.Interfaces;

public interface IReminderRepository
{
    Task<EventReminder?> GetByIdAsync(Guid id, CancellationToken cancellationToken = default);
    Task<IEnumerable<EventReminder>> GetByEventIdAsync(Guid eventId, CancellationToken cancellationToken = default);
    Task<IEnumerable<EventReminder>> GetPendingRemindersAsync(DateTime upToTime, CancellationToken cancellationToken = default);
    Task AddAsync(EventReminder reminder, CancellationToken cancellationToken = default);
    Task UpdateAsync(EventReminder reminder, CancellationToken cancellationToken = default);
    Task DeleteAsync(Guid id, CancellationToken cancellationToken = default);
}
