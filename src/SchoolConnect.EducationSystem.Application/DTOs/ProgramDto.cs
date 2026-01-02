namespace SchoolConnect.EducationSystem.Application.DTOs;

public record ProgramDto(string Id, string AssessmentBoardId, string EducationPhaseId, string Name, string Description, int DurationYears, DateTime CreatedAt, DateTime? UpdatedAt);

public record CreateProgramDto(string AssessmentBoardId, string EducationPhaseId, string Name, string Description, int DurationYears);

public record UpdateProgramDto(string Name, string Description, int DurationYears);
