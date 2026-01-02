using MongoDB.Driver;
using SchoolConnect.Communication.Domain.Entities;
using SchoolConnect.Communication.Domain.Interfaces;
using SchoolConnect.Communication.Infrastructure.Persistence;

namespace SchoolConnect.Communication.Infrastructure.Repositories;

public class ConversationRepository : IConversationRepository
{
    private readonly CommunicationDbContext _context;

    public ConversationRepository(CommunicationDbContext context)
    {
        _context = context;
    }

    public async Task<Conversation?> GetByIdAsync(Guid id, CancellationToken cancellationToken = default)
    {
        return await _context.Conversations
            .Find(c => c.Id == id && !c.IsArchived)
            .FirstOrDefaultAsync(cancellationToken);
    }

    public async Task<List<Conversation>> GetByUserIdAsync(Guid userId, CancellationToken cancellationToken = default)
    {
        var filter = Builders<Conversation>.Filter.And(
            Builders<Conversation>.Filter.ElemMatch(c => c.Participants, p => p.UserId == userId && p.IsActive),
            Builders<Conversation>.Filter.Eq(c => c.IsArchived, false)
        );

        return await _context.Conversations
            .Find(filter)
            .SortByDescending(c => c.LastMessageAt)
            .ToListAsync(cancellationToken);
    }

    public async Task<int> GetUnreadCountAsync(Guid userId, CancellationToken cancellationToken = default)
    {
        var filter = Builders<Conversation>.Filter.ElemMatch(
            c => c.Participants, 
            p => p.UserId == userId && p.IsActive && p.UnreadCount > 0
        );

        var conversations = await _context.Conversations
            .Find(filter)
            .ToListAsync(cancellationToken);

        return conversations
            .SelectMany(c => c.Participants.Where(p => p.UserId == userId))
            .Sum(p => p.UnreadCount);
    }

    public async Task AddAsync(Conversation conversation, CancellationToken cancellationToken = default)
    {
        await _context.Conversations.InsertOneAsync(conversation, cancellationToken: cancellationToken);
    }

    public async Task UpdateAsync(Conversation conversation, CancellationToken cancellationToken = default)
    {
        await _context.Conversations.ReplaceOneAsync(
            c => c.Id == conversation.Id,
            conversation,
            cancellationToken: cancellationToken);
    }

    public async Task DeleteAsync(Guid id, CancellationToken cancellationToken = default)
    {
        await _context.Conversations.DeleteOneAsync(c => c.Id == id, cancellationToken);
    }
}
