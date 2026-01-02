namespace SchoolConnect.EducationSystem.Application.DTOs;

public record AssessmentBoardDto(string Id, string EducationSystemId, string Name, string Abbreviation, string Description, DateTime CreatedAt, DateTime? UpdatedAt);

public record CreateAssessmentBoardDto(string EducationSystemId, string Name, string Abbreviation, string Description);

public record UpdateAssessmentBoardDto(string Name, string Abbreviation, string Description);
