using MongoDB.Driver;
using SchoolConnect.Communication.Domain.Entities;
using SchoolConnect.Communication.Domain.Interfaces;
using SchoolConnect.Communication.Infrastructure.Persistence;

namespace SchoolConnect.Communication.Infrastructure.Repositories;

public class AnnouncementRepository : IAnnouncementRepository
{
    private readonly CommunicationDbContext _context;

    public AnnouncementRepository(CommunicationDbContext context)
    {
        _context = context;
    }

    public async Task<Announcement?> GetByIdAsync(Guid id, CancellationToken cancellationToken = default)
    {
        return await _context.Announcements
            .Find(a => a.Id == id)
            .FirstOrDefaultAsync(cancellationToken);
    }

    public async Task<List<Announcement>> GetByInstituteIdAsync(
        Guid instituteId, 
        int page = 1, 
        int pageSize = 50, 
        CancellationToken cancellationToken = default)
    {
        return await _context.Announcements
            .Find(a => a.InstituteId == instituteId)
            .SortByDescending(a => a.PublishedAt)
            .Skip((page - 1) * pageSize)
            .Limit(pageSize)
            .ToListAsync(cancellationToken);
    }

    public async Task<List<Announcement>> GetPinnedAsync(Guid instituteId, CancellationToken cancellationToken = default)
    {
        return await _context.Announcements
            .Find(a => a.InstituteId == instituteId && a.IsPinned)
            .SortByDescending(a => a.PublishedAt)
            .ToListAsync(cancellationToken);
    }

    public async Task<List<AnnouncementAcknowledgment>> GetAcknowledgmentsAsync(
        Guid announcementId, 
        CancellationToken cancellationToken = default)
    {
        return await _context.AnnouncementAcknowledgments
            .Find(a => a.AnnouncementId == announcementId)
            .SortByDescending(a => a.AcknowledgedAt)
            .ToListAsync(cancellationToken);
    }

    public async Task AddAsync(Announcement announcement, CancellationToken cancellationToken = default)
    {
        await _context.Announcements.InsertOneAsync(announcement, cancellationToken: cancellationToken);
    }

    public async Task UpdateAsync(Announcement announcement, CancellationToken cancellationToken = default)
    {
        await _context.Announcements.ReplaceOneAsync(
            a => a.Id == announcement.Id,
            announcement,
            cancellationToken: cancellationToken);
    }

    public async Task DeleteAsync(Guid id, CancellationToken cancellationToken = default)
    {
        await _context.Announcements.DeleteOneAsync(a => a.Id == id, cancellationToken);
    }

    public async Task AddAcknowledgmentAsync(
        AnnouncementAcknowledgment acknowledgment, 
        CancellationToken cancellationToken = default)
    {
        await _context.AnnouncementAcknowledgments.InsertOneAsync(
            acknowledgment, 
            cancellationToken: cancellationToken);
    }
}
