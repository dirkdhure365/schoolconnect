namespace SchoolConnect.EducationSystem.Application.DTOs;

public record CurriculumDto(string Id, string SubjectId, string Title, string Content, string LearningObjectives, string Assessment, int Year, DateTime CreatedAt, DateTime? UpdatedAt);

public record CreateCurriculumDto(string SubjectId, string Title, string Content, string LearningObjectives, string Assessment, int Year);

public record UpdateCurriculumDto(string Title, string Content, string LearningObjectives, string Assessment, int Year);
