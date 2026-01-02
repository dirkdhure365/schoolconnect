using SchoolConnect.Calendar.Application.Services;
using SchoolConnect.Calendar.Domain.Entities;
using SchoolConnect.Calendar.Domain.Interfaces;

namespace SchoolConnect.Calendar.Infrastructure.Services;

public class ReminderSchedulerService : IReminderSchedulerService
{
    private readonly IReminderRepository _reminderRepository;

    public ReminderSchedulerService(IReminderRepository reminderRepository)
    {
        _reminderRepository = reminderRepository;
    }

    public async Task ScheduleReminderAsync(EventReminder reminder, CancellationToken cancellationToken = default)
    {
        await _reminderRepository.AddAsync(reminder, cancellationToken);
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
        var pendingReminders = await _reminderRepository.GetPendingRemindersAsync(cancellationToken);
        
        foreach (var reminder in pendingReminders)
        {
            try
            {
                // Send reminder through appropriate channel (email, push, sms)
                // This would integrate with Communication service
                
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
