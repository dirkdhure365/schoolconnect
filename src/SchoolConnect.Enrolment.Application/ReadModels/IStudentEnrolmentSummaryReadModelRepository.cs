namespace SchoolConnect.Enrolment.Application.ReadModels;

public interface IStudentEnrolmentSummaryReadModelRepository
{
    Task<StudentEnrolmentSummaryReadModel?> GetByIdAsync(Guid id, CancellationToken ct = default);
    Task<IEnumerable<StudentEnrolmentSummaryReadModel>> GetByStudentIdAsync(Guid studentId, CancellationToken ct = default);
    Task<IEnumerable<StudentEnrolmentSummaryReadModel>> GetByStreamIdAsync(Guid streamId, CancellationToken ct = default);
    Task<IEnumerable<StudentEnrolmentSummaryReadModel>> GetByCohortIdAsync(Guid cohortId, CancellationToken ct = default);
    Task UpsertAsync(StudentEnrolmentSummaryReadModel model, CancellationToken ct = default);
    Task DeleteAsync(Guid id, CancellationToken ct = default);
}
