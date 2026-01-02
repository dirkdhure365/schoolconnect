using MediatR;
using SchoolConnect.EducationSystem.Application.DTOs;

namespace SchoolConnect.EducationSystem.Application.Commands;

public record CreateAssessmentBoardCommand(string EducationSystemId, string Name, string Abbreviation, string Description) : IRequest<AssessmentBoardDto>;

public record UpdateAssessmentBoardCommand(string Id, string Name, string Abbreviation, string Description) : IRequest<AssessmentBoardDto>;

public record DeleteAssessmentBoardCommand(string Id) : IRequest<bool>;
