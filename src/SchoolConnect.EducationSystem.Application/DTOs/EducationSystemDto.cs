namespace SchoolConnect.EducationSystem.Application.DTOs;

public record EducationSystemDto(string Id, string CountryId, string Name, string Description, DateTime CreatedAt, DateTime? UpdatedAt);

public record CreateEducationSystemDto(string CountryId, string Name, string Description);

public record UpdateEducationSystemDto(string Name, string Description);
