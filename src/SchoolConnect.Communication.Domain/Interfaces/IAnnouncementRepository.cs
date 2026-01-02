using SchoolConnect.Communication.Domain.Entities;

namespace SchoolConnect.Communication.Domain.Interfaces;

public interface IAnnouncementRepository
{
    Task<Announcement?> GetByIdAsync(Guid id, CancellationToken cancellationToken = default);
    Task<List<Announcement>> GetByInstituteIdAsync(Guid instituteId, int page = 1, int pageSize = 50, CancellationToken cancellationToken = default);
    Task<List<Announcement>> GetPinnedAsync(Guid instituteId, CancellationToken cancellationToken = default);
    Task<List<AnnouncementAcknowledgment>> GetAcknowledgmentsAsync(Guid announcementId, CancellationToken cancellationToken = default);
    Task AddAsync(Announcement announcement, CancellationToken cancellationToken = default);
    Task UpdateAsync(Announcement announcement, CancellationToken cancellationToken = default);
    Task DeleteAsync(Guid id, CancellationToken cancellationToken = default);
    Task AddAcknowledgmentAsync(AnnouncementAcknowledgment acknowledgment, CancellationToken cancellationToken = default);
}
