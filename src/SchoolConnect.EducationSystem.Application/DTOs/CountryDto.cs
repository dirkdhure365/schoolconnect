namespace SchoolConnect.EducationSystem.Application.DTOs;

public record CountryDto(string Id, string Name, string Code, string Region, DateTime CreatedAt, DateTime? UpdatedAt);

public record CreateCountryDto(string Name, string Code, string Region);

public record UpdateCountryDto(string Name, string Code, string Region);
