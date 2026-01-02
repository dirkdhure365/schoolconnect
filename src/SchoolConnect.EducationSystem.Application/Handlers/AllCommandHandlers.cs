using MediatR;
using SchoolConnect.EducationSystem.Application.Commands;
using SchoolConnect.EducationSystem.Application.DTOs;
using SchoolConnect.EducationSystem.Application.Interfaces;
using SchoolConnect.EducationSystem.Domain.Entities;

namespace SchoolConnect.EducationSystem.Application.Handlers;

// EducationSystem Handlers
public class CreateEducationSystemHandler : IRequestHandler<CreateEducationSystemCommand, EducationSystemDto>
{
    private readonly IRepository<Domain.Entities.EducationSystem> _repository;
    public CreateEducationSystemHandler(IRepository<Domain.Entities.EducationSystem> repository) => _repository = repository;

    public async Task<EducationSystemDto> Handle(CreateEducationSystemCommand request, CancellationToken cancellationToken)
    {
        var system = Domain.Entities.EducationSystem.Create(request.CountryId, request.Name, request.Description);
        var created = await _repository.AddAsync(system);
        return new EducationSystemDto(created.Id, created.CountryId, created.Name, created.Description, created.CreatedAt, created.UpdatedAt);
    }
}

public class UpdateEducationSystemHandler : IRequestHandler<UpdateEducationSystemCommand, EducationSystemDto>
{
    private readonly IRepository<Domain.Entities.EducationSystem> _repository;
    public UpdateEducationSystemHandler(IRepository<Domain.Entities.EducationSystem> repository) => _repository = repository;

    public async Task<EducationSystemDto> Handle(UpdateEducationSystemCommand request, CancellationToken cancellationToken)
    {
        var system = await _repository.GetByIdAsync(request.Id) ?? throw new KeyNotFoundException($"EducationSystem {request.Id} not found");
        system.Update(request.Name, request.Description);
        var updated = await _repository.UpdateAsync(system);
        return new EducationSystemDto(updated.Id, updated.CountryId, updated.Name, updated.Description, updated.CreatedAt, updated.UpdatedAt);
    }
}

public class DeleteEducationSystemHandler : IRequestHandler<DeleteEducationSystemCommand, bool>
{
    private readonly IRepository<Domain.Entities.EducationSystem> _repository;
    public DeleteEducationSystemHandler(IRepository<Domain.Entities.EducationSystem> repository) => _repository = repository;
    public async Task<bool> Handle(DeleteEducationSystemCommand request, CancellationToken cancellationToken) => await _repository.DeleteAsync(request.Id);
}

// AssessmentBoard Handlers
public class CreateAssessmentBoardHandler : IRequestHandler<CreateAssessmentBoardCommand, AssessmentBoardDto>
{
    private readonly IRepository<AssessmentBoard> _repository;
    public CreateAssessmentBoardHandler(IRepository<AssessmentBoard> repository) => _repository = repository;

    public async Task<AssessmentBoardDto> Handle(CreateAssessmentBoardCommand request, CancellationToken cancellationToken)
    {
        var board = AssessmentBoard.Create(request.EducationSystemId, request.Name, request.Abbreviation, request.Description);
        var created = await _repository.AddAsync(board);
        return new AssessmentBoardDto(created.Id, created.EducationSystemId, created.Name, created.Abbreviation, created.Description, created.CreatedAt, created.UpdatedAt);
    }
}

public class UpdateAssessmentBoardHandler : IRequestHandler<UpdateAssessmentBoardCommand, AssessmentBoardDto>
{
    private readonly IRepository<AssessmentBoard> _repository;
    public UpdateAssessmentBoardHandler(IRepository<AssessmentBoard> repository) => _repository = repository;

    public async Task<AssessmentBoardDto> Handle(UpdateAssessmentBoardCommand request, CancellationToken cancellationToken)
    {
        var board = await _repository.GetByIdAsync(request.Id) ?? throw new KeyNotFoundException($"AssessmentBoard {request.Id} not found");
        board.Update(request.Name, request.Abbreviation, request.Description);
        var updated = await _repository.UpdateAsync(board);
        return new AssessmentBoardDto(updated.Id, updated.EducationSystemId, updated.Name, updated.Abbreviation, updated.Description, updated.CreatedAt, updated.UpdatedAt);
    }
}

public class DeleteAssessmentBoardHandler : IRequestHandler<DeleteAssessmentBoardCommand, bool>
{
    private readonly IRepository<AssessmentBoard> _repository;
    public DeleteAssessmentBoardHandler(IRepository<AssessmentBoard> repository) => _repository = repository;
    public async Task<bool> Handle(DeleteAssessmentBoardCommand request, CancellationToken cancellationToken) => await _repository.DeleteAsync(request.Id);
}

// EducationPhase Handlers
public class CreateEducationPhaseHandler : IRequestHandler<CreateEducationPhaseCommand, EducationPhaseDto>
{
    private readonly IRepository<EducationPhase> _repository;
    public CreateEducationPhaseHandler(IRepository<EducationPhase> repository) => _repository = repository;

    public async Task<EducationPhaseDto> Handle(CreateEducationPhaseCommand request, CancellationToken cancellationToken)
    {
        var phase = EducationPhase.Create(request.EducationSystemId, request.Name, request.Description, request.StartAge, request.EndAge);
        var created = await _repository.AddAsync(phase);
        return new EducationPhaseDto(created.Id, created.EducationSystemId, created.Name, created.Description, created.StartAge, created.EndAge, created.CreatedAt, created.UpdatedAt);
    }
}

public class UpdateEducationPhaseHandler : IRequestHandler<UpdateEducationPhaseCommand, EducationPhaseDto>
{
    private readonly IRepository<EducationPhase> _repository;
    public UpdateEducationPhaseHandler(IRepository<EducationPhase> repository) => _repository = repository;

    public async Task<EducationPhaseDto> Handle(UpdateEducationPhaseCommand request, CancellationToken cancellationToken)
    {
        var phase = await _repository.GetByIdAsync(request.Id) ?? throw new KeyNotFoundException($"EducationPhase {request.Id} not found");
        phase.Update(request.Name, request.Description, request.StartAge, request.EndAge);
        var updated = await _repository.UpdateAsync(phase);
        return new EducationPhaseDto(updated.Id, updated.EducationSystemId, updated.Name, updated.Description, updated.StartAge, updated.EndAge, updated.CreatedAt, updated.UpdatedAt);
    }
}

public class DeleteEducationPhaseHandler : IRequestHandler<DeleteEducationPhaseCommand, bool>
{
    private readonly IRepository<EducationPhase> _repository;
    public DeleteEducationPhaseHandler(IRepository<EducationPhase> repository) => _repository = repository;
    public async Task<bool> Handle(DeleteEducationPhaseCommand request, CancellationToken cancellationToken) => await _repository.DeleteAsync(request.Id);
}

// Program Handlers
public class CreateProgramHandler : IRequestHandler<CreateProgramCommand, ProgramDto>
{
    private readonly IRepository<Domain.Entities.Program> _repository;
    public CreateProgramHandler(IRepository<Domain.Entities.Program> repository) => _repository = repository;

    public async Task<ProgramDto> Handle(CreateProgramCommand request, CancellationToken cancellationToken)
    {
        var program = Domain.Entities.Program.Create(request.AssessmentBoardId, request.EducationPhaseId, request.Name, request.Description, request.DurationYears);
        var created = await _repository.AddAsync(program);
        return new ProgramDto(created.Id, created.AssessmentBoardId, created.EducationPhaseId, created.Name, created.Description, created.DurationYears, created.CreatedAt, created.UpdatedAt);
    }
}

public class UpdateProgramHandler : IRequestHandler<UpdateProgramCommand, ProgramDto>
{
    private readonly IRepository<Domain.Entities.Program> _repository;
    public UpdateProgramHandler(IRepository<Domain.Entities.Program> repository) => _repository = repository;

    public async Task<ProgramDto> Handle(UpdateProgramCommand request, CancellationToken cancellationToken)
    {
        var program = await _repository.GetByIdAsync(request.Id) ?? throw new KeyNotFoundException($"Program {request.Id} not found");
        program.Update(request.Name, request.Description, request.DurationYears);
        var updated = await _repository.UpdateAsync(program);
        return new ProgramDto(updated.Id, updated.AssessmentBoardId, updated.EducationPhaseId, updated.Name, updated.Description, updated.DurationYears, updated.CreatedAt, updated.UpdatedAt);
    }
}

public class DeleteProgramHandler : IRequestHandler<DeleteProgramCommand, bool>
{
    private readonly IRepository<Domain.Entities.Program> _repository;
    public DeleteProgramHandler(IRepository<Domain.Entities.Program> repository) => _repository = repository;
    public async Task<bool> Handle(DeleteProgramCommand request, CancellationToken cancellationToken) => await _repository.DeleteAsync(request.Id);
}

// Subject Handlers
public class CreateSubjectHandler : IRequestHandler<CreateSubjectCommand, SubjectDto>
{
    private readonly IRepository<Subject> _repository;
    public CreateSubjectHandler(IRepository<Subject> repository) => _repository = repository;

    public async Task<SubjectDto> Handle(CreateSubjectCommand request, CancellationToken cancellationToken)
    {
        var subject = Subject.Create(request.ProgramId, request.Name, request.Code, request.Description, request.IsCore);
        var created = await _repository.AddAsync(subject);
        return new SubjectDto(created.Id, created.ProgramId, created.Name, created.Code, created.Description, created.IsCore, created.CreatedAt, created.UpdatedAt);
    }
}

public class UpdateSubjectHandler : IRequestHandler<UpdateSubjectCommand, SubjectDto>
{
    private readonly IRepository<Subject> _repository;
    public UpdateSubjectHandler(IRepository<Subject> repository) => _repository = repository;

    public async Task<SubjectDto> Handle(UpdateSubjectCommand request, CancellationToken cancellationToken)
    {
        var subject = await _repository.GetByIdAsync(request.Id) ?? throw new KeyNotFoundException($"Subject {request.Id} not found");
        subject.Update(request.Name, request.Code, request.Description, request.IsCore);
        var updated = await _repository.UpdateAsync(subject);
        return new SubjectDto(updated.Id, updated.ProgramId, updated.Name, updated.Code, updated.Description, updated.IsCore, updated.CreatedAt, updated.UpdatedAt);
    }
}

public class DeleteSubjectHandler : IRequestHandler<DeleteSubjectCommand, bool>
{
    private readonly IRepository<Subject> _repository;
    public DeleteSubjectHandler(IRepository<Subject> repository) => _repository = repository;
    public async Task<bool> Handle(DeleteSubjectCommand request, CancellationToken cancellationToken) => await _repository.DeleteAsync(request.Id);
}

// Curriculum Handlers
public class CreateCurriculumHandler : IRequestHandler<CreateCurriculumCommand, CurriculumDto>
{
    private readonly IRepository<Curriculum> _repository;
    public CreateCurriculumHandler(IRepository<Curriculum> repository) => _repository = repository;

    public async Task<CurriculumDto> Handle(CreateCurriculumCommand request, CancellationToken cancellationToken)
    {
        var curriculum = Curriculum.Create(request.SubjectId, request.Title, request.Content, request.LearningObjectives, request.Assessment, request.Year);
        var created = await _repository.AddAsync(curriculum);
        return new CurriculumDto(created.Id, created.SubjectId, created.Title, created.Content, created.LearningObjectives, created.Assessment, created.Year, created.CreatedAt, created.UpdatedAt);
    }
}

public class UpdateCurriculumHandler : IRequestHandler<UpdateCurriculumCommand, CurriculumDto>
{
    private readonly IRepository<Curriculum> _repository;
    public UpdateCurriculumHandler(IRepository<Curriculum> repository) => _repository = repository;

    public async Task<CurriculumDto> Handle(UpdateCurriculumCommand request, CancellationToken cancellationToken)
    {
        var curriculum = await _repository.GetByIdAsync(request.Id) ?? throw new KeyNotFoundException($"Curriculum {request.Id} not found");
        curriculum.Update(request.Title, request.Content, request.LearningObjectives, request.Assessment, request.Year);
        var updated = await _repository.UpdateAsync(curriculum);
        return new CurriculumDto(updated.Id, updated.SubjectId, updated.Title, updated.Content, updated.LearningObjectives, updated.Assessment, updated.Year, updated.CreatedAt, updated.UpdatedAt);
    }
}

public class DeleteCurriculumHandler : IRequestHandler<DeleteCurriculumCommand, bool>
{
    private readonly IRepository<Curriculum> _repository;
    public DeleteCurriculumHandler(IRepository<Curriculum> repository) => _repository = repository;
    public async Task<bool> Handle(DeleteCurriculumCommand request, CancellationToken cancellationToken) => await _repository.DeleteAsync(request.Id);
}
