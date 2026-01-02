using MediatR;
using SchoolConnect.EducationSystem.Application.DTOs;

namespace SchoolConnect.EducationSystem.Application.Queries;

public record GetAllProgramsQuery : IRequest<List<ProgramDto>>;

public record GetProgramByIdQuery(string Id) : IRequest<ProgramDto?>;

public record GetProgramsByAssessmentBoardQuery(string AssessmentBoardId) : IRequest<List<ProgramDto>>;
