using MediatR;
using SchoolConnect.EducationSystem.Application.DTOs;

namespace SchoolConnect.EducationSystem.Application.Commands;

public record CreateSubjectCommand(string ProgramId, string Name, string Code, string Description, bool IsCore) : IRequest<SubjectDto>;

public record UpdateSubjectCommand(string Id, string Name, string Code, string Description, bool IsCore) : IRequest<SubjectDto>;

public record DeleteSubjectCommand(string Id) : IRequest<bool>;
