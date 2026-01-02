using SchoolConnect.Calendar.Application.Services;
using SchoolConnect.Calendar.Domain.Interfaces;

namespace SchoolConnect.Calendar.Infrastructure.Services;

public class ReminderSchedulerService : IReminderSchedulerService
{
    private readonly IReminderRepository _reminderRepository;

    public ReminderSchedulerService(IReminderRepository reminderRepository)
    {
        _reminderRepository = reminderRepository;
    }

    public async Task ScheduleReminderAsync(Guid reminderId, DateTime reminderTime, CancellationToken cancellationToken = default)
    {
        // In a real implementation, this would integrate with a job scheduler like Hangfire or Quartz.NET
        // For now, we'll just ensure the reminder exists
        var reminder = await _reminderRepository.GetByIdAsync(reminderId, cancellationToken);
        if (reminder != null)
        {
            // Schedule the reminder for processing
            await Task.CompletedTask;
        }
    }

    public async Task CancelReminderAsync(Guid reminderId, CancellationToken cancellationToken = default)
    {
        var reminder = await _reminderRepository.GetByIdAsync(reminderId, cancellationToken);
        if (reminder != null)
        {
            reminder.Cancel();
            await _reminderRepository.UpdateAsync(reminder, cancellationToken);
        }
    }

    public async Task ProcessPendingRemindersAsync(CancellationToken cancellationToken = default)
    {
        var pendingReminders = await _reminderRepository.GetPendingRemindersAsync(DateTime.UtcNow, cancellationToken);
        
        foreach (var reminder in pendingReminders)
        {
            try
            {
                // Send the reminder via the appropriate channel
                // In a real implementation, this would integrate with notification services
                reminder.MarkAsSent();
                await _reminderRepository.UpdateAsync(reminder, cancellationToken);
            }
            catch (Exception ex)
            {
                reminder.MarkAsFailed(ex.Message);
                await _reminderRepository.UpdateAsync(reminder, cancellationToken);
            }
        }
    }
}
