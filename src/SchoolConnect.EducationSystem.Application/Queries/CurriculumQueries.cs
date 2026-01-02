using MediatR;
using SchoolConnect.EducationSystem.Application.DTOs;

namespace SchoolConnect.EducationSystem.Application.Queries;

public record GetAllCurriculaQuery : IRequest<List<CurriculumDto>>;

public record GetCurriculumByIdQuery(string Id) : IRequest<CurriculumDto?>;

public record GetCurriculaBySubjectQuery(string SubjectId) : IRequest<List<CurriculumDto>>;
