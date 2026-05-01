using Microsoft.Extensions.Logging;
using MongoDB.Driver;
using SchoolConnect.Enrolment.Application.ReadModels;
using SchoolConnect.Enrolment.Infrastructure.Persistence;

namespace SchoolConnect.Enrolment.Infrastructure.ReadModels;

public class StudentEnrolmentSummaryReadModelRepository : IStudentEnrolmentSummaryReadModelRepository
{
    private readonly IMongoCollection<StudentEnrolmentSummaryReadModel> _collection;
    private readonly ILogger<StudentEnrolmentSummaryReadModelRepository> _logger;

    public StudentEnrolmentSummaryReadModelRepository(
        EnrolmentDbContext context,
        ILogger<StudentEnrolmentSummaryReadModelRepository> logger)
    {
        _collection = context.StudentEnrolmentSummaryReadModels;
        _logger = logger;
    }

    public async Task<StudentEnrolmentSummaryReadModel?> GetByIdAsync(Guid id, CancellationToken ct = default)
    {
        return await _collection
            .Find(e => e.Id == id)
            .FirstOrDefaultAsync(ct);
    }

    public async Task<IEnumerable<StudentEnrolmentSummaryReadModel>> GetByStudentIdAsync(Guid studentId, CancellationToken ct = default)
    {
        return await _collection
            .Find(e => e.StudentId == studentId)
            .ToListAsync(ct);
    }

    public async Task<IEnumerable<StudentEnrolmentSummaryReadModel>> GetByStreamIdAsync(Guid streamId, CancellationToken ct = default)
    {
        return await _collection
            .Find(e => e.StreamId == streamId)
            .ToListAsync(ct);
    }

    public async Task<IEnumerable<StudentEnrolmentSummaryReadModel>> GetByCohortIdAsync(Guid cohortId, CancellationToken ct = default)
    {
        return await _collection
            .Find(e => e.CohortId == cohortId)
            .ToListAsync(ct);
    }

    public async Task UpsertAsync(StudentEnrolmentSummaryReadModel model, CancellationToken ct = default)
    {
        model.LastUpdated = DateTime.UtcNow;
        
        var filter = Builders<StudentEnrolmentSummaryReadModel>.Filter.Eq(e => e.Id, model.Id);
        var options = new ReplaceOptions { IsUpsert = true };
        
        await _collection.ReplaceOneAsync(filter, model, options, ct);
        
        _logger.LogInformation(
            "Upserted student enrolment summary read model for enrolment {EnrolmentId} (Student: {StudentId})",
            model.Id,
            model.StudentId);
    }

    public async Task DeleteAsync(Guid id, CancellationToken ct = default)
    {
        await _collection.DeleteOneAsync(e => e.Id == id, ct);
        
        _logger.LogInformation("Deleted student enrolment summary read model for enrolment {EnrolmentId}", id);
    }
}
