namespace SchoolConnect.EducationSystem.Application.DTOs;

public record EducationPhaseDto(string Id, string EducationSystemId, string Name, string Description, int StartAge, int EndAge, DateTime CreatedAt, DateTime? UpdatedAt);

public record CreateEducationPhaseDto(string EducationSystemId, string Name, string Description, int StartAge, int EndAge);

public record UpdateEducationPhaseDto(string Name, string Description, int StartAge, int EndAge);
