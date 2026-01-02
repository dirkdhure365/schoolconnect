using MediatR;
using SchoolConnect.EducationSystem.Application.DTOs;

namespace SchoolConnect.EducationSystem.Application.Queries;

public record GetAllAssessmentBoardsQuery : IRequest<List<AssessmentBoardDto>>;

public record GetAssessmentBoardByIdQuery(string Id) : IRequest<AssessmentBoardDto?>;

public record GetAssessmentBoardsByEducationSystemQuery(string EducationSystemId) : IRequest<List<AssessmentBoardDto>>;
