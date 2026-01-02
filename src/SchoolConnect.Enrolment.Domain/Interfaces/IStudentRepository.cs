using SchoolConnect.Enrolment.Domain.Entities;

namespace SchoolConnect.Enrolment.Domain.Interfaces;

public interface IStudentRepository
{
    Task<Student?> GetByIdAsync(Guid id, CancellationToken cancellationToken = default);
    Task<Student?> GetByStudentCodeAsync(string studentCode, CancellationToken cancellationToken = default);
    Task<IEnumerable<Student>> GetByInstituteAsync(Guid instituteId, CancellationToken cancellationToken = default);
    Task<IEnumerable<Student>> GetByCentreAsync(Guid centreId, CancellationToken cancellationToken = default);
    Task AddAsync(Student student, CancellationToken cancellationToken = default);
    Task UpdateAsync(Student student, CancellationToken cancellationToken = default);
    Task DeleteAsync(Guid id, CancellationToken cancellationToken = default);
}
