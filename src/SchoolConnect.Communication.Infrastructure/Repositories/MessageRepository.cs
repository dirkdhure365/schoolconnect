using MongoDB.Driver;
using SchoolConnect.Communication.Domain.Entities;
using SchoolConnect.Communication.Domain.Interfaces;
using SchoolConnect.Communication.Infrastructure.Persistence;

namespace SchoolConnect.Communication.Infrastructure.Repositories;

public class MessageRepository : IMessageRepository
{
    private readonly CommunicationDbContext _context;

    public MessageRepository(CommunicationDbContext context)
    {
        _context = context;
    }

    public async Task<Message?> GetByIdAsync(Guid id, CancellationToken cancellationToken = default)
    {
        return await _context.Messages
            .Find(m => m.Id == id && !m.IsDeleted)
            .FirstOrDefaultAsync(cancellationToken);
    }

    public async Task<List<Message>> GetByConversationIdAsync(
        Guid conversationId, 
        int page = 1, 
        int pageSize = 50, 
        CancellationToken cancellationToken = default)
    {
        return await _context.Messages
            .Find(m => m.ConversationId == conversationId && !m.IsDeleted)
            .SortByDescending(m => m.SentAt)
            .Skip((page - 1) * pageSize)
            .Limit(pageSize)
            .ToListAsync(cancellationToken);
    }

    public async Task<List<Message>> SearchAsync(
        string searchTerm, 
        Guid userId, 
        CancellationToken cancellationToken = default)
    {
        var filter = Builders<Message>.Filter.And(
            Builders<Message>.Filter.Text(searchTerm),
            Builders<Message>.Filter.Eq(m => m.IsDeleted, false)
        );

        return await _context.Messages
            .Find(filter)
            .Limit(50)
            .ToListAsync(cancellationToken);
    }

    public async Task AddAsync(Message message, CancellationToken cancellationToken = default)
    {
        await _context.Messages.InsertOneAsync(message, cancellationToken: cancellationToken);
    }

    public async Task UpdateAsync(Message message, CancellationToken cancellationToken = default)
    {
        await _context.Messages.ReplaceOneAsync(
            m => m.Id == message.Id,
            message,
            cancellationToken: cancellationToken);
    }

    public async Task DeleteAsync(Guid id, CancellationToken cancellationToken = default)
    {
        await _context.Messages.DeleteOneAsync(m => m.Id == id, cancellationToken);
    }
}
