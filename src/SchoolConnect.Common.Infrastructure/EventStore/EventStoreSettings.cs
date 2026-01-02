namespace SchoolConnect.Common.Infrastructure.EventStore;

public class EventStoreSettings
{
    public string ConnectionString { get; set; } = string.Empty;
    public string DatabaseName { get; set; } = string.Empty;
    public string CollectionName { get; set; } = "Events";
}
