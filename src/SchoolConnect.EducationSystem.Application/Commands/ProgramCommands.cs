using MediatR;
using SchoolConnect.EducationSystem.Application.DTOs;

namespace SchoolConnect.EducationSystem.Application.Commands;

public record CreateProgramCommand(string AssessmentBoardId, string EducationPhaseId, string Name, string Description, int DurationYears) : IRequest<ProgramDto>;

public record UpdateProgramCommand(string Id, string Name, string Description, int DurationYears) : IRequest<ProgramDto>;

public record DeleteProgramCommand(string Id) : IRequest<bool>;
