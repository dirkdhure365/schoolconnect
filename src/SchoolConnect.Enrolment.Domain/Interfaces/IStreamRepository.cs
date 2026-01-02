using StreamEntity = SchoolConnect.Enrolment.Domain.Entities.Stream;

namespace SchoolConnect.Enrolment.Domain.Interfaces;

public interface IStreamRepository
{
    Task<StreamEntity?> GetByIdAsync(Guid id, CancellationToken cancellationToken = default);
    Task<IEnumerable<StreamEntity>> GetByInstituteAsync(Guid instituteId, CancellationToken cancellationToken = default);
    Task AddAsync(StreamEntity stream, CancellationToken cancellationToken = default);
    Task UpdateAsync(StreamEntity stream, CancellationToken cancellationToken = default);
    Task DeleteAsync(Guid id, CancellationToken cancellationToken = default);
}
