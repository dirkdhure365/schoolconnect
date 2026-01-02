using MediatR;
using SchoolConnect.EducationSystem.Application.DTOs;

namespace SchoolConnect.EducationSystem.Application.Commands;

public record CreateCountryCommand(string Name, string Code, string Region) : IRequest<CountryDto>;

public record UpdateCountryCommand(string Id, string Name, string Code, string Region) : IRequest<CountryDto>;

public record DeleteCountryCommand(string Id) : IRequest<bool>;
