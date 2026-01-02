using SchoolConnect.Common.Domain.Primitives;
using SchoolConnect.Calendar.Domain.Enums;

namespace SchoolConnect.Calendar.Domain.ValueObjects;

public class ReminderSettings : ValueObject
{
    public List<int> MinutesBeforeOptions { get; private set; } = [15, 30, 60, 1440];
    public ReminderChannel DefaultChannel { get; private set; }
    public bool EnableEmailReminders { get; private set; }
    public bool EnablePushReminders { get; private set; }
    public bool EnableSmsReminders { get; private set; }

    private ReminderSettings() { }

    public static ReminderSettings Create(
        ReminderChannel defaultChannel,
        bool enableEmailReminders = true,
        bool enablePushReminders = true,
        bool enableSmsReminders = false,
        List<int>? minutesBeforeOptions = null)
    {
        return new ReminderSettings
        {
            DefaultChannel = defaultChannel,
            EnableEmailReminders = enableEmailReminders,
            EnablePushReminders = enablePushReminders,
            EnableSmsReminders = enableSmsReminders,
            MinutesBeforeOptions = minutesBeforeOptions ?? [15, 30, 60, 1440]
        };
    }

    protected override IEnumerable<object> GetEqualityComponents()
    {
        yield return string.Join(",", MinutesBeforeOptions);
        yield return DefaultChannel;
        yield return EnableEmailReminders;
        yield return EnablePushReminders;
        yield return EnableSmsReminders;
    }
}
