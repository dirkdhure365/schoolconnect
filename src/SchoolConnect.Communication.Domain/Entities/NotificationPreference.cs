using SchoolConnect.Common.Domain.Primitives;
using SchoolConnect.Communication.Domain.Enums;
using SchoolConnect.Communication.Domain.Events;

namespace SchoolConnect.Communication.Domain.Entities;

public class NotificationPreference : AggregateRoot
{
    public Guid UserId { get; private set; }
    
    public List<ChannelPreference> ChannelPreferences { get; private set; } = [];
    public List<TypePreference> TypePreferences { get; private set; } = [];
    
    public bool GlobalMute { get; private set; }
    public TimeOnly? QuietHoursStart { get; private set; }
    public TimeOnly? QuietHoursEnd { get; private set; }
    public List<DayOfWeek>? QuietDays { get; private set; }
    
    public bool EmailDigestEnabled { get; private set; }
    public string? EmailDigestFrequency { get; private set; } // Daily, Weekly

    private NotificationPreference() { }

    public static NotificationPreference CreateDefault(Guid userId)
    {
        var preference = new NotificationPreference
        {
            Id = Guid.NewGuid(),
            UserId = userId,
            GlobalMute = false,
            EmailDigestEnabled = true,
            EmailDigestFrequency = "Daily",
            ChannelPreferences = 
            [
                new ChannelPreference { Channel = NotificationChannel.InApp, IsEnabled = true },
                new ChannelPreference { Channel = NotificationChannel.Push, IsEnabled = true },
                new ChannelPreference { Channel = NotificationChannel.Email, IsEnabled = true },
                new ChannelPreference { Channel = NotificationChannel.Sms, IsEnabled = false }
            ],
            TypePreferences = [],
            CreatedAt = DateTime.UtcNow,
            UpdatedAt = DateTime.UtcNow
        };

        return preference;
    }

    public void UpdateChannelPreference(NotificationChannel channel, bool isEnabled)
    {
        var pref = ChannelPreferences.FirstOrDefault(p => p.Channel == channel);
        if (pref != null)
        {
            pref.IsEnabled = isEnabled;
        }
        else
        {
            ChannelPreferences.Add(new ChannelPreference { Channel = channel, IsEnabled = isEnabled });
        }
        UpdatedAt = DateTime.UtcNow;
        Apply(new NotificationPreferenceUpdatedEvent(Id, UserId));
    }

    public void UpdateTypePreference(NotificationType type, bool isEnabled, List<NotificationChannel>? enabledChannels = null)
    {
        var pref = TypePreferences.FirstOrDefault(p => p.Type == type);
        if (pref != null)
        {
            pref.IsEnabled = isEnabled;
            if (enabledChannels != null)
            {
                pref.EnabledChannels = enabledChannels;
            }
        }
        else
        {
            TypePreferences.Add(new TypePreference 
            { 
                Type = type, 
                IsEnabled = isEnabled, 
                EnabledChannels = enabledChannels ?? [] 
            });
        }
        UpdatedAt = DateTime.UtcNow;
        Apply(new NotificationPreferenceUpdatedEvent(Id, UserId));
    }

    public void SetGlobalMute(bool mute)
    {
        GlobalMute = mute;
        UpdatedAt = DateTime.UtcNow;
        Apply(new NotificationPreferenceUpdatedEvent(Id, UserId));
    }

    public void SetQuietHours(TimeOnly? start, TimeOnly? end, List<DayOfWeek>? days = null)
    {
        QuietHoursStart = start;
        QuietHoursEnd = end;
        QuietDays = days;
        UpdatedAt = DateTime.UtcNow;
        Apply(new NotificationPreferenceUpdatedEvent(Id, UserId));
    }

    public void SetEmailDigest(bool enabled, string? frequency = null)
    {
        EmailDigestEnabled = enabled;
        if (frequency != null)
        {
            EmailDigestFrequency = frequency;
        }
        UpdatedAt = DateTime.UtcNow;
        Apply(new NotificationPreferenceUpdatedEvent(Id, UserId));
    }

    protected override void When(DomainEvent @event)
    {
        // Event sourcing implementation if needed
    }
}

public class ChannelPreference
{
    public NotificationChannel Channel { get; set; }
    public bool IsEnabled { get; set; }
}

public class TypePreference
{
    public NotificationType Type { get; set; }
    public bool IsEnabled { get; set; }
    public List<NotificationChannel> EnabledChannels { get; set; } = [];
}
