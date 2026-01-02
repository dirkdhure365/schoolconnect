namespace SchoolConnect.EducationSystem.Application.DTOs;

public record SubjectDto(string Id, string ProgramId, string Name, string Code, string Description, bool IsCore, DateTime CreatedAt, DateTime? UpdatedAt);

public record CreateSubjectDto(string ProgramId, string Name, string Code, string Description, bool IsCore);

public record UpdateSubjectDto(string Name, string Code, string Description, bool IsCore);
