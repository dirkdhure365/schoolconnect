using SchoolConnect.EducationSystem.Application.Interfaces;
using SchoolConnect.EducationSystem.Domain.Entities;
using SchoolConnect.EducationSystem.Infrastructure.Persistence;

namespace SchoolConnect.EducationSystem.Infrastructure.Repositories;

public class CountryRepository : MongoRepository<Country>
{
    public CountryRepository(IMongoDbContext context, Application.Interfaces.IEventStore eventStore) 
        : base(context, eventStore, "Countries")
    {
    }
}

public class EducationSystemRepository : MongoRepository<Domain.Entities.EducationSystem>
{
    public EducationSystemRepository(IMongoDbContext context, Application.Interfaces.IEventStore eventStore) 
        : base(context, eventStore, "EducationSystems")
    {
    }
}

public class AssessmentBoardRepository : MongoRepository<AssessmentBoard>
{
    public AssessmentBoardRepository(IMongoDbContext context, Application.Interfaces.IEventStore eventStore) 
        : base(context, eventStore, "AssessmentBoards")
    {
    }
}

public class EducationPhaseRepository : MongoRepository<EducationPhase>
{
    public EducationPhaseRepository(IMongoDbContext context, Application.Interfaces.IEventStore eventStore) 
        : base(context, eventStore, "EducationPhases")
    {
    }
}

public class ProgramRepository : MongoRepository<Domain.Entities.Program>
{
    public ProgramRepository(IMongoDbContext context, Application.Interfaces.IEventStore eventStore) 
        : base(context, eventStore, "Programs")
    {
    }
}

public class SubjectRepository : MongoRepository<Subject>
{
    public SubjectRepository(IMongoDbContext context, Application.Interfaces.IEventStore eventStore) 
        : base(context, eventStore, "Subjects")
    {
    }
}

public class CurriculumRepository : MongoRepository<Domain.Entities.Curriculum>
{
    public CurriculumRepository(IMongoDbContext context, Application.Interfaces.IEventStore eventStore) 
        : base(context, eventStore, "Curricula")
    {
    }
}
