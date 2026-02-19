namespace SchoolConnect.Enrolment.Application.ReadModels;

public interface IStudentReadModelRepository
{
    Task<StudentReadModel?> GetByIdAsync(Guid id, CancellationToken ct = default);
    Task<StudentReadModel?> GetByStudentCodeAsync(string studentCode, CancellationToken ct = default);
    Task<IEnumerable<StudentReadModel>> GetByInstituteAsync(Guid instituteId, CancellationToken ct = default);
    Task UpsertAsync(StudentReadModel model, CancellationToken ct = default);
    Task DeleteAsync(Guid id, CancellationToken ct = default);
}
