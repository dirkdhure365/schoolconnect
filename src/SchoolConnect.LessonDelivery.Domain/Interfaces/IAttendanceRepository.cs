using SchoolConnect.LessonDelivery.Domain.Entities;

namespace SchoolConnect.LessonDelivery.Domain.Interfaces;

public interface IAttendanceRepository
{
    Task<Attendance?> GetByIdAsync(Guid id, CancellationToken cancellationToken = default);
    Task<List<Attendance>> GetBySessionIdAsync(Guid sessionId, CancellationToken cancellationToken = default);
    Task<List<Attendance>> GetByStudentIdAsync(Guid studentId, CancellationToken cancellationToken = default);
    Task AddAsync(Attendance attendance, CancellationToken cancellationToken = default);
    Task UpdateAsync(Attendance attendance, CancellationToken cancellationToken = default);
    Task DeleteAsync(Guid id, CancellationToken cancellationToken = default);
}
