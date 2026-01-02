using SchoolConnect.Communication.Domain.Enums;

namespace SchoolConnect.Communication.Domain.ValueObjects;

public class DeliveryChannel
{
    public NotificationChannel Channel { get; private set; }
    public bool IsEnabled { get; private set; }

    private DeliveryChannel() { }

    public static DeliveryChannel Create(NotificationChannel channel, bool isEnabled)
    {
        return new DeliveryChannel
        {
            Channel = channel,
            IsEnabled = isEnabled
        };
    }
}
