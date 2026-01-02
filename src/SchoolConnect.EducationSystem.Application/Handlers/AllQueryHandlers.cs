using MediatR;
using SchoolConnect.EducationSystem.Application.Queries;
using SchoolConnect.EducationSystem.Application.DTOs;
using SchoolConnect.EducationSystem.Application.Interfaces;
using SchoolConnect.EducationSystem.Domain.Entities;

namespace SchoolConnect.EducationSystem.Application.Handlers;

// AssessmentBoard Query Handlers
public class GetAllAssessmentBoardsHandler : IRequestHandler<GetAllAssessmentBoardsQuery, List<AssessmentBoardDto>>
{
    private readonly IRepository<AssessmentBoard> _repository;
    public GetAllAssessmentBoardsHandler(IRepository<AssessmentBoard> repository) => _repository = repository;
    public async Task<List<AssessmentBoardDto>> Handle(GetAllAssessmentBoardsQuery request, CancellationToken cancellationToken) =>
        (await _repository.GetAllAsync()).Select(b => new AssessmentBoardDto(b.Id, b.EducationSystemId, b.Name, b.Abbreviation, b.Description, b.CreatedAt, b.UpdatedAt)).ToList();
}

public class GetAssessmentBoardByIdHandler : IRequestHandler<GetAssessmentBoardByIdQuery, AssessmentBoardDto?>
{
    private readonly IRepository<AssessmentBoard> _repository;
    public GetAssessmentBoardByIdHandler(IRepository<AssessmentBoard> repository) => _repository = repository;
    public async Task<AssessmentBoardDto?> Handle(GetAssessmentBoardByIdQuery request, CancellationToken cancellationToken)
    {
        var board = await _repository.GetByIdAsync(request.Id);
        return board != null ? new AssessmentBoardDto(board.Id, board.EducationSystemId, board.Name, board.Abbreviation, board.Description, board.CreatedAt, board.UpdatedAt) : null;
    }
}

public class GetAssessmentBoardsByEducationSystemHandler : IRequestHandler<GetAssessmentBoardsByEducationSystemQuery, List<AssessmentBoardDto>>
{
    private readonly IAssessmentBoardRepository _repository;
    public GetAssessmentBoardsByEducationSystemHandler(IAssessmentBoardRepository repository) => _repository = repository;
    public async Task<List<AssessmentBoardDto>> Handle(GetAssessmentBoardsByEducationSystemQuery request, CancellationToken cancellationToken) =>
        (await _repository.GetByEducationSystemIdAsync(request.EducationSystemId)).Select(b => new AssessmentBoardDto(b.Id, b.EducationSystemId, b.Name, b.Abbreviation, b.Description, b.CreatedAt, b.UpdatedAt)).ToList();
}

// EducationPhase Query Handlers
public class GetAllEducationPhasesHandler : IRequestHandler<GetAllEducationPhasesQuery, List<EducationPhaseDto>>
{
    private readonly IRepository<EducationPhase> _repository;
    public GetAllEducationPhasesHandler(IRepository<EducationPhase> repository) => _repository = repository;
    public async Task<List<EducationPhaseDto>> Handle(GetAllEducationPhasesQuery request, CancellationToken cancellationToken) =>
        (await _repository.GetAllAsync()).Select(p => new EducationPhaseDto(p.Id, p.EducationSystemId, p.Name, p.Description, p.StartAge, p.EndAge, p.CreatedAt, p.UpdatedAt)).ToList();
}

public class GetEducationPhaseByIdHandler : IRequestHandler<GetEducationPhaseByIdQuery, EducationPhaseDto?>
{
    private readonly IRepository<EducationPhase> _repository;
    public GetEducationPhaseByIdHandler(IRepository<EducationPhase> repository) => _repository = repository;
    public async Task<EducationPhaseDto?> Handle(GetEducationPhaseByIdQuery request, CancellationToken cancellationToken)
    {
        var phase = await _repository.GetByIdAsync(request.Id);
        return phase != null ? new EducationPhaseDto(phase.Id, phase.EducationSystemId, phase.Name, phase.Description, phase.StartAge, phase.EndAge, phase.CreatedAt, phase.UpdatedAt) : null;
    }
}

public class GetEducationPhasesByEducationSystemHandler : IRequestHandler<GetEducationPhasesByEducationSystemQuery, List<EducationPhaseDto>>
{
    private readonly IEducationPhaseRepository _repository;
    public GetEducationPhasesByEducationSystemHandler(IEducationPhaseRepository repository) => _repository = repository;
    public async Task<List<EducationPhaseDto>> Handle(GetEducationPhasesByEducationSystemQuery request, CancellationToken cancellationToken) =>
        (await _repository.GetByEducationSystemIdAsync(request.EducationSystemId)).Select(p => new EducationPhaseDto(p.Id, p.EducationSystemId, p.Name, p.Description, p.StartAge, p.EndAge, p.CreatedAt, p.UpdatedAt)).ToList();
}

// Program Query Handlers
public class GetAllProgramsHandler : IRequestHandler<GetAllProgramsQuery, List<ProgramDto>>
{
    private readonly IRepository<Domain.Entities.Program> _repository;
    public GetAllProgramsHandler(IRepository<Domain.Entities.Program> repository) => _repository = repository;
    public async Task<List<ProgramDto>> Handle(GetAllProgramsQuery request, CancellationToken cancellationToken) =>
        (await _repository.GetAllAsync()).Select(p => new ProgramDto(p.Id, p.AssessmentBoardId, p.EducationPhaseId, p.Name, p.Description, p.DurationYears, p.CreatedAt, p.UpdatedAt)).ToList();
}

public class GetProgramByIdHandler : IRequestHandler<GetProgramByIdQuery, ProgramDto?>
{
    private readonly IRepository<Domain.Entities.Program> _repository;
    public GetProgramByIdHandler(IRepository<Domain.Entities.Program> repository) => _repository = repository;
    public async Task<ProgramDto?> Handle(GetProgramByIdQuery request, CancellationToken cancellationToken)
    {
        var program = await _repository.GetByIdAsync(request.Id);
        return program != null ? new ProgramDto(program.Id, program.AssessmentBoardId, program.EducationPhaseId, program.Name, program.Description, program.DurationYears, program.CreatedAt, program.UpdatedAt) : null;
    }
}

public class GetProgramsByAssessmentBoardHandler : IRequestHandler<GetProgramsByAssessmentBoardQuery, List<ProgramDto>>
{
    private readonly IProgramRepository _repository;
    public GetProgramsByAssessmentBoardHandler(IProgramRepository repository) => _repository = repository;
    public async Task<List<ProgramDto>> Handle(GetProgramsByAssessmentBoardQuery request, CancellationToken cancellationToken) =>
        (await _repository.GetByAssessmentBoardIdAsync(request.AssessmentBoardId)).Select(p => new ProgramDto(p.Id, p.AssessmentBoardId, p.EducationPhaseId, p.Name, p.Description, p.DurationYears, p.CreatedAt, p.UpdatedAt)).ToList();
}

// Subject Query Handlers
public class GetAllSubjectsHandler : IRequestHandler<GetAllSubjectsQuery, List<SubjectDto>>
{
    private readonly IRepository<Subject> _repository;
    public GetAllSubjectsHandler(IRepository<Subject> repository) => _repository = repository;
    public async Task<List<SubjectDto>> Handle(GetAllSubjectsQuery request, CancellationToken cancellationToken) =>
        (await _repository.GetAllAsync()).Select(s => new SubjectDto(s.Id, s.ProgramId, s.Name, s.Code, s.Description, s.IsCore, s.CreatedAt, s.UpdatedAt)).ToList();
}

public class GetSubjectByIdHandler : IRequestHandler<GetSubjectByIdQuery, SubjectDto?>
{
    private readonly IRepository<Subject> _repository;
    public GetSubjectByIdHandler(IRepository<Subject> repository) => _repository = repository;
    public async Task<SubjectDto?> Handle(GetSubjectByIdQuery request, CancellationToken cancellationToken)
    {
        var subject = await _repository.GetByIdAsync(request.Id);
        return subject != null ? new SubjectDto(subject.Id, subject.ProgramId, subject.Name, subject.Code, subject.Description, subject.IsCore, subject.CreatedAt, subject.UpdatedAt) : null;
    }
}

public class GetSubjectsByProgramHandler : IRequestHandler<GetSubjectsByProgramQuery, List<SubjectDto>>
{
    private readonly ISubjectRepository _repository;
    public GetSubjectsByProgramHandler(ISubjectRepository repository) => _repository = repository;
    public async Task<List<SubjectDto>> Handle(GetSubjectsByProgramQuery request, CancellationToken cancellationToken) =>
        (await _repository.GetByProgramIdAsync(request.ProgramId)).Select(s => new SubjectDto(s.Id, s.ProgramId, s.Name, s.Code, s.Description, s.IsCore, s.CreatedAt, s.UpdatedAt)).ToList();
}

// Curriculum Query Handlers
public class GetAllCurriculaHandler : IRequestHandler<GetAllCurriculaQuery, List<CurriculumDto>>
{
    private readonly IRepository<Curriculum> _repository;
    public GetAllCurriculaHandler(IRepository<Curriculum> repository) => _repository = repository;
    public async Task<List<CurriculumDto>> Handle(GetAllCurriculaQuery request, CancellationToken cancellationToken) =>
        (await _repository.GetAllAsync()).Select(c => new CurriculumDto(c.Id, c.SubjectId, c.Title, c.Content, c.LearningObjectives, c.Assessment, c.Year, c.CreatedAt, c.UpdatedAt)).ToList();
}

public class GetCurriculumByIdHandler : IRequestHandler<GetCurriculumByIdQuery, CurriculumDto?>
{
    private readonly IRepository<Curriculum> _repository;
    public GetCurriculumByIdHandler(IRepository<Curriculum> repository) => _repository = repository;
    public async Task<CurriculumDto?> Handle(GetCurriculumByIdQuery request, CancellationToken cancellationToken)
    {
        var curriculum = await _repository.GetByIdAsync(request.Id);
        return curriculum != null ? new CurriculumDto(curriculum.Id, curriculum.SubjectId, curriculum.Title, curriculum.Content, curriculum.LearningObjectives, curriculum.Assessment, curriculum.Year, curriculum.CreatedAt, curriculum.UpdatedAt) : null;
    }
}

public class GetCurriculaBySubjectHandler : IRequestHandler<GetCurriculaBySubjectQuery, List<CurriculumDto>>
{
    private readonly ICurriculumRepository _repository;
    public GetCurriculaBySubjectHandler(ICurriculumRepository repository) => _repository = repository;
    public async Task<List<CurriculumDto>> Handle(GetCurriculaBySubjectQuery request, CancellationToken cancellationToken) =>
        (await _repository.GetBySubjectIdAsync(request.SubjectId)).Select(c => new CurriculumDto(c.Id, c.SubjectId, c.Title, c.Content, c.LearningObjectives, c.Assessment, c.Year, c.CreatedAt, c.UpdatedAt)).ToList();
}

// Event Query Handlers
public class GetAllEventsHandler : IRequestHandler<GetAllEventsQuery, List<Domain.Events.DomainEvent>>
{
    private readonly IEventStore _eventStore;
    public GetAllEventsHandler(IEventStore eventStore) => _eventStore = eventStore;
    public async Task<List<Domain.Events.DomainEvent>> Handle(GetAllEventsQuery request, CancellationToken cancellationToken) =>
        await _eventStore.GetEventsAsync();
}

public class GetEventsByAggregateHandler : IRequestHandler<GetEventsByAggregateQuery, List<Domain.Events.DomainEvent>>
{
    private readonly IEventStore _eventStore;
    public GetEventsByAggregateHandler(IEventStore eventStore) => _eventStore = eventStore;
    public async Task<List<Domain.Events.DomainEvent>> Handle(GetEventsByAggregateQuery request, CancellationToken cancellationToken) =>
        await _eventStore.GetEventsByAggregateAsync(request.AggregateId);
}
