using SchoolConnect.Communication.Domain.Enums;

namespace SchoolConnect.Communication.Domain.ValueObjects;

public class DeliveryChannel
{
    public NotificationChannel Channel { get; set; }
    public bool IsEnabled { get; set; }
}
