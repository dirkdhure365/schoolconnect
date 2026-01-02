using MediatR;
using SchoolConnect.EducationSystem.Application.DTOs;

namespace SchoolConnect.EducationSystem.Application.Queries;

public record GetAllSubjectsQuery : IRequest<List<SubjectDto>>;

public record GetSubjectByIdQuery(string Id) : IRequest<SubjectDto?>;

public record GetSubjectsByProgramQuery(string ProgramId) : IRequest<List<SubjectDto>>;
