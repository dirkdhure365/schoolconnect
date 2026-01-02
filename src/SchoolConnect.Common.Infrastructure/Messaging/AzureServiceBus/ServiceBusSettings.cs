namespace SchoolConnect.Common.Infrastructure.Messaging.AzureServiceBus;

public class ServiceBusSettings
{
    public string ConnectionString { get; set; } = string.Empty;
    public string TopicName { get; set; } = "schoolconnect-events";
    public string SubscriptionName { get; set; } = string.Empty;
}
