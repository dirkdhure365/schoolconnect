using SchoolConnect.Calendar.Domain.Entities;

namespace SchoolConnect.Calendar.Application.Services;

public interface IReminderSchedulerService
{
    Task ScheduleReminderAsync(EventReminder reminder, CancellationToken cancellationToken = default);
    Task CancelReminderAsync(Guid reminderId, CancellationToken cancellationToken = default);
    Task ProcessPendingRemindersAsync(CancellationToken cancellationToken = default);
}
