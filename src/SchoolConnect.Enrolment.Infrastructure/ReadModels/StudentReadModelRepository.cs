using Microsoft.Extensions.Logging;
using MongoDB.Driver;
using SchoolConnect.Enrolment.Application.ReadModels;
using SchoolConnect.Enrolment.Infrastructure.Persistence;

namespace SchoolConnect.Enrolment.Infrastructure.ReadModels;

public class StudentReadModelRepository : IStudentReadModelRepository
{
    private readonly IMongoCollection<StudentReadModel> _collection;
    private readonly ILogger<StudentReadModelRepository> _logger;

    public StudentReadModelRepository(
        EnrolmentDbContext context,
        ILogger<StudentReadModelRepository> logger)
    {
        _collection = context.StudentReadModels;
        _logger = logger;
    }

    public async Task<StudentReadModel?> GetByIdAsync(Guid id, CancellationToken ct = default)
    {
        return await _collection
            .Find(s => s.Id == id)
            .FirstOrDefaultAsync(ct);
    }

    public async Task<StudentReadModel?> GetByStudentCodeAsync(string studentCode, CancellationToken ct = default)
    {
        return await _collection
            .Find(s => s.StudentCode == studentCode)
            .FirstOrDefaultAsync(ct);
    }

    public async Task<IEnumerable<StudentReadModel>> GetByInstituteAsync(Guid instituteId, CancellationToken ct = default)
    {
        return await _collection
            .Find(s => s.InstituteId == instituteId)
            .ToListAsync(ct);
    }

    public async Task UpsertAsync(StudentReadModel model, CancellationToken ct = default)
    {
        model.LastUpdated = DateTime.UtcNow;
        
        var filter = Builders<StudentReadModel>.Filter.Eq(s => s.Id, model.Id);
        var options = new ReplaceOptions { IsUpsert = true };
        
        await _collection.ReplaceOneAsync(filter, model, options, ct);
        
        _logger.LogInformation(
            "Upserted student read model for student {StudentId} (Code: {StudentCode})",
            model.Id,
            model.StudentCode);
    }

    public async Task DeleteAsync(Guid id, CancellationToken ct = default)
    {
        await _collection.DeleteOneAsync(s => s.Id == id, ct);
        
        _logger.LogInformation("Deleted student read model for student {StudentId}", id);
    }
}
