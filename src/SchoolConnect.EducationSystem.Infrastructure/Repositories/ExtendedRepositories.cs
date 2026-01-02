using MongoDB.Driver;
using SchoolConnect.EducationSystem.Application.Interfaces;
using SchoolConnect.EducationSystem.Domain.Entities;
using SchoolConnect.EducationSystem.Infrastructure.Persistence;

namespace SchoolConnect.EducationSystem.Infrastructure.Repositories;

public class EducationSystemRepositoryExtended : EducationSystemRepository, IEducationSystemRepository
{
    private readonly IMongoDbContext _context;

    public EducationSystemRepositoryExtended(IMongoDbContext context, Application.Interfaces.IEventStore eventStore) 
        : base(context, eventStore)
    {
        _context = context;
    }

    public async Task<List<Domain.Entities.EducationSystem>> GetByCountryIdAsync(string countryId)
    {
        var collection = _context.GetCollection<Domain.Entities.EducationSystem>("EducationSystems");
        var filter = Builders<Domain.Entities.EducationSystem>.Filter.Eq(e => e.CountryId, countryId);
        return await collection.Find(filter).ToListAsync();
    }
}

public class AssessmentBoardRepositoryExtended : AssessmentBoardRepository, IAssessmentBoardRepository
{
    private readonly IMongoDbContext _context;

    public AssessmentBoardRepositoryExtended(IMongoDbContext context, Application.Interfaces.IEventStore eventStore) 
        : base(context, eventStore)
    {
        _context = context;
    }

    public async Task<List<AssessmentBoard>> GetByEducationSystemIdAsync(string educationSystemId)
    {
        var collection = _context.GetCollection<AssessmentBoard>("AssessmentBoards");
        var filter = Builders<AssessmentBoard>.Filter.Eq(e => e.EducationSystemId, educationSystemId);
        return await collection.Find(filter).ToListAsync();
    }
}

public class EducationPhaseRepositoryExtended : EducationPhaseRepository, IEducationPhaseRepository
{
    private readonly IMongoDbContext _context;

    public EducationPhaseRepositoryExtended(IMongoDbContext context, Application.Interfaces.IEventStore eventStore) 
        : base(context, eventStore)
    {
        _context = context;
    }

    public async Task<List<EducationPhase>> GetByEducationSystemIdAsync(string educationSystemId)
    {
        var collection = _context.GetCollection<EducationPhase>("EducationPhases");
        var filter = Builders<EducationPhase>.Filter.Eq(e => e.EducationSystemId, educationSystemId);
        return await collection.Find(filter).ToListAsync();
    }
}

public class ProgramRepositoryExtended : ProgramRepository, IProgramRepository
{
    private readonly IMongoDbContext _context;

    public ProgramRepositoryExtended(IMongoDbContext context, Application.Interfaces.IEventStore eventStore) 
        : base(context, eventStore)
    {
        _context = context;
    }

    public async Task<List<Domain.Entities.Program>> GetByAssessmentBoardIdAsync(string assessmentBoardId)
    {
        var collection = _context.GetCollection<Domain.Entities.Program>("Programs");
        var filter = Builders<Domain.Entities.Program>.Filter.Eq(e => e.AssessmentBoardId, assessmentBoardId);
        return await collection.Find(filter).ToListAsync();
    }
}

public class SubjectRepositoryExtended : SubjectRepository, ISubjectRepository
{
    private readonly IMongoDbContext _context;

    public SubjectRepositoryExtended(IMongoDbContext context, Application.Interfaces.IEventStore eventStore) 
        : base(context, eventStore)
    {
        _context = context;
    }

    public async Task<List<Subject>> GetByProgramIdAsync(string programId)
    {
        var collection = _context.GetCollection<Subject>("Subjects");
        var filter = Builders<Subject>.Filter.Eq(e => e.ProgramId, programId);
        return await collection.Find(filter).ToListAsync();
    }
}

public class CurriculumRepositoryExtended : CurriculumRepository, ICurriculumRepository
{
    private readonly IMongoDbContext _context;

    public CurriculumRepositoryExtended(IMongoDbContext context, Application.Interfaces.IEventStore eventStore) 
        : base(context, eventStore)
    {
        _context = context;
    }

    public async Task<List<Curriculum>> GetBySubjectIdAsync(string subjectId)
    {
        var collection = _context.GetCollection<Curriculum>("Curricula");
        var filter = Builders<Curriculum>.Filter.Eq(e => e.SubjectId, subjectId);
        return await collection.Find(filter).ToListAsync();
    }
}
