using MongoDB.Driver;
using SchoolConnect.LessonDelivery.Domain.Entities;
using SchoolConnect.LessonDelivery.Domain.Interfaces;
using SchoolConnect.LessonDelivery.Infrastructure.Persistence;

namespace SchoolConnect.LessonDelivery.Infrastructure.Repositories;

public class AttendanceRepository : IAttendanceRepository
{
    private readonly LessonDeliveryDbContext _context;

    public AttendanceRepository(LessonDeliveryDbContext context)
    {
        _context = context;
    }

    public async Task<Attendance?> GetByIdAsync(Guid id, CancellationToken cancellationToken = default)
    {
        return await _context.Attendances
            .Find(a => a.Id == id)
            .FirstOrDefaultAsync(cancellationToken);
    }

    public async Task<List<Attendance>> GetBySessionIdAsync(Guid sessionId, CancellationToken cancellationToken = default)
    {
        return await _context.Attendances
            .Find(a => a.LessonSessionId == sessionId)
            .ToListAsync(cancellationToken);
    }

    public async Task<List<Attendance>> GetByStudentIdAsync(Guid studentId, CancellationToken cancellationToken = default)
    {
        return await _context.Attendances
            .Find(a => a.StudentId == studentId)
            .ToListAsync(cancellationToken);
    }

    public async Task AddAsync(Attendance attendance, CancellationToken cancellationToken = default)
    {
        await _context.Attendances.InsertOneAsync(attendance, cancellationToken: cancellationToken);
    }

    public async Task UpdateAsync(Attendance attendance, CancellationToken cancellationToken = default)
    {
        await _context.Attendances.ReplaceOneAsync(
            a => a.Id == attendance.Id,
            attendance,
            cancellationToken: cancellationToken);
    }

    public async Task DeleteAsync(Guid id, CancellationToken cancellationToken = default)
    {
        await _context.Attendances.DeleteOneAsync(a => a.Id == id, cancellationToken);
    }
}
