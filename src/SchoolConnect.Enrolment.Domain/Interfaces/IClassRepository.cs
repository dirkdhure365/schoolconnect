using SchoolConnect.Enrolment.Domain.Entities;

namespace SchoolConnect.Enrolment.Domain.Interfaces;

public interface IClassRepository
{
    Task<Class?> GetByIdAsync(Guid id, CancellationToken cancellationToken = default);
    Task<IEnumerable<Class>> GetByCohortAsync(Guid cohortId, CancellationToken cancellationToken = default);
    Task<IEnumerable<Class>> GetByTeacherAsync(Guid teacherId, CancellationToken cancellationToken = default);
    Task AddAsync(Class @class, CancellationToken cancellationToken = default);
    Task UpdateAsync(Class @class, CancellationToken cancellationToken = default);
    Task DeleteAsync(Guid id, CancellationToken cancellationToken = default);
}
