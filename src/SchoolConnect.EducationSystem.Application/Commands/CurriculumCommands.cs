using MediatR;
using SchoolConnect.EducationSystem.Application.DTOs;

namespace SchoolConnect.EducationSystem.Application.Commands;

public record CreateCurriculumCommand(string SubjectId, string Title, string Content, string LearningObjectives, string Assessment, int Year) : IRequest<CurriculumDto>;

public record UpdateCurriculumCommand(string Id, string Title, string Content, string LearningObjectives, string Assessment, int Year) : IRequest<CurriculumDto>;

public record DeleteCurriculumCommand(string Id) : IRequest<bool>;
