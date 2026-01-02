using SchoolConnect.Common.Infrastructure.Messaging.Contracts;

namespace SchoolConnect.Common.Infrastructure.Messaging.AzureServiceBus;

public interface IMessageConsumer
{
    Task StartAsync(Func<IntegrationEvent, CancellationToken, Task> messageHandler, CancellationToken ct = default);
    Task StopAsync(CancellationToken ct = default);
}
