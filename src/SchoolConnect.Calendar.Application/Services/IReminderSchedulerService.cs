namespace SchoolConnect.Calendar.Application.Services;

public interface IReminderSchedulerService
{
    Task ScheduleReminderAsync(Guid reminderId, DateTime reminderTime, CancellationToken cancellationToken = default);
    Task CancelReminderAsync(Guid reminderId, CancellationToken cancellationToken = default);
    Task ProcessPendingRemindersAsync(CancellationToken cancellationToken = default);
}
