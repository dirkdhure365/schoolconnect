using SchoolConnect.Communication.Domain.Entities;

namespace SchoolConnect.Communication.Domain.Interfaces;

public interface IMessageRepository
{
    Task<Message?> GetByIdAsync(Guid id, CancellationToken cancellationToken = default);
    Task<List<Message>> GetByConversationIdAsync(Guid conversationId, int page = 1, int pageSize = 50, CancellationToken cancellationToken = default);
    Task<List<Message>> SearchAsync(string searchTerm, Guid userId, CancellationToken cancellationToken = default);
    Task AddAsync(Message message, CancellationToken cancellationToken = default);
    Task UpdateAsync(Message message, CancellationToken cancellationToken = default);
    Task DeleteAsync(Guid id, CancellationToken cancellationToken = default);
}
