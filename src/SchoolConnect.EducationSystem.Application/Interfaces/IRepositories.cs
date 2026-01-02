using SchoolConnect.EducationSystem.Domain.Aggregates;

namespace SchoolConnect.EducationSystem.Application.Interfaces;

public interface IRepository<T> where T : AggregateRoot
{
    Task<T?> GetByIdAsync(string id);
    Task<List<T>> GetAllAsync();
    Task<T> AddAsync(T entity);
    Task<T> UpdateAsync(T entity);
    Task<bool> DeleteAsync(string id);
}

public interface IEducationSystemRepository : IRepository<Domain.Entities.EducationSystem>
{
    Task<List<Domain.Entities.EducationSystem>> GetByCountryIdAsync(string countryId);
}

public interface IAssessmentBoardRepository : IRepository<Domain.Entities.AssessmentBoard>
{
    Task<List<Domain.Entities.AssessmentBoard>> GetByEducationSystemIdAsync(string educationSystemId);
}

public interface IEducationPhaseRepository : IRepository<Domain.Entities.EducationPhase>
{
    Task<List<Domain.Entities.EducationPhase>> GetByEducationSystemIdAsync(string educationSystemId);
}

public interface IProgramRepository : IRepository<Domain.Entities.Program>
{
    Task<List<Domain.Entities.Program>> GetByAssessmentBoardIdAsync(string assessmentBoardId);
}

public interface ISubjectRepository : IRepository<Domain.Entities.Subject>
{
    Task<List<Domain.Entities.Subject>> GetByProgramIdAsync(string programId);
}

public interface ICurriculumRepository : IRepository<Domain.Entities.Curriculum>
{
    Task<List<Domain.Entities.Curriculum>> GetBySubjectIdAsync(string subjectId);
}

public interface IEventStore
{
    Task SaveEventAsync(Domain.Events.DomainEvent domainEvent);
    Task<List<Domain.Events.DomainEvent>> GetEventsAsync();
    Task<List<Domain.Events.DomainEvent>> GetEventsByAggregateAsync(string aggregateId);
}
