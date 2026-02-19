using MongoDB.Driver;
using SchoolConnect.Enrolment.Domain.Entities;
using SchoolConnect.Enrolment.Domain.Interfaces;
using SchoolConnect.Enrolment.Infrastructure.Persistence;
using SchoolConnect.Common.Infrastructure.EventDispatcher;

namespace SchoolConnect.Enrolment.Infrastructure.Repositories;

public class StudentRepository : IStudentRepository
{
    private readonly EnrolmentDbContext _context;
    private readonly IDomainEventDispatcher? _eventDispatcher;

    public StudentRepository(
        EnrolmentDbContext context,
        IDomainEventDispatcher? eventDispatcher = null)
    {
        _context = context;
        _eventDispatcher = eventDispatcher;
    }

    public async Task<Student?> GetByIdAsync(Guid id, CancellationToken cancellationToken = default)
    {
        return await _context.Students
            .Find(s => s.Id == id)
            .FirstOrDefaultAsync(cancellationToken);
    }

    public async Task<Student?> GetByStudentCodeAsync(string studentCode, CancellationToken cancellationToken = default)
    {
        return await _context.Students
            .Find(s => s.StudentCode == studentCode)
            .FirstOrDefaultAsync(cancellationToken);
    }

    public async Task<IEnumerable<Student>> GetByInstituteAsync(Guid instituteId, CancellationToken cancellationToken = default)
    {
        return await _context.Students
            .Find(s => s.InstituteId == instituteId)
            .ToListAsync(cancellationToken);
    }

    public async Task<IEnumerable<Student>> GetByCentreAsync(Guid centreId, CancellationToken cancellationToken = default)
    {
        // Note: This would require joining with enrolments. For now, return empty
        return new List<Student>();
    }

    public async Task AddAsync(Student student, CancellationToken cancellationToken = default)
    {
        await _context.Students.InsertOneAsync(student, cancellationToken: cancellationToken);
        
        // Dispatch domain events if dispatcher is available
        if (_eventDispatcher != null && student.DomainEvents.Any())
        {
            await _eventDispatcher.DispatchAsync(student.DomainEvents, cancellationToken);
            student.ClearDomainEvents();
        }
    }

    public async Task UpdateAsync(Student student, CancellationToken cancellationToken = default)
    {
        await _context.Students.ReplaceOneAsync(
            s => s.Id == student.Id,
            student,
            cancellationToken: cancellationToken);
        
        // Dispatch domain events if dispatcher is available
        if (_eventDispatcher != null && student.DomainEvents.Any())
        {
            await _eventDispatcher.DispatchAsync(student.DomainEvents, cancellationToken);
            student.ClearDomainEvents();
        }
    }

    public async Task DeleteAsync(Guid id, CancellationToken cancellationToken = default)
    {
        await _context.Students.DeleteOneAsync(
            s => s.Id == id,
            cancellationToken);
    }
}
