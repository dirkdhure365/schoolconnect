using SchoolConnect.Common.Infrastructure.Messaging.Contracts;

namespace SchoolConnect.Common.Infrastructure.Messaging.AzureServiceBus;

public interface IMessagePublisher
{
    Task PublishAsync<T>(T @event, CancellationToken ct = default) where T : IntegrationEvent;
    Task PublishManyAsync<T>(IEnumerable<T> events, CancellationToken ct = default) where T : IntegrationEvent;
}
