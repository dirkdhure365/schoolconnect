using SchoolConnect.Calendar.Domain.Entities;

namespace SchoolConnect.Calendar.Domain.Interfaces;

public interface ITimetableRepository
{
    Task<Timetable?> GetByIdAsync(Guid id, CancellationToken cancellationToken = default);
    Task<IEnumerable<Timetable>> GetByInstituteIdAsync(Guid instituteId, CancellationToken cancellationToken = default);
    Task<IEnumerable<Timetable>> GetByCentreIdAsync(Guid centreId, CancellationToken cancellationToken = default);
    Task<Timetable?> GetActiveByInstituteAsync(Guid instituteId, Guid centreId, CancellationToken cancellationToken = default);
    Task AddAsync(Timetable timetable, CancellationToken cancellationToken = default);
    Task UpdateAsync(Timetable timetable, CancellationToken cancellationToken = default);
    Task DeleteAsync(Guid id, CancellationToken cancellationToken = default);
}
