using MediatR;
using SchoolConnect.EducationSystem.Application.DTOs;

namespace SchoolConnect.EducationSystem.Application.Queries;

public record GetAllCountriesQuery : IRequest<List<CountryDto>>;

public record GetCountryByIdQuery(string Id) : IRequest<CountryDto?>;
