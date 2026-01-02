using MediatR;
using SchoolConnect.EducationSystem.Application.DTOs;

namespace SchoolConnect.EducationSystem.Application.Queries;

public record GetAllEducationSystemsQuery : IRequest<List<EducationSystemDto>>;

public record GetEducationSystemByIdQuery(string Id) : IRequest<EducationSystemDto?>;

public record GetEducationSystemsByCountryQuery(string CountryId) : IRequest<List<EducationSystemDto>>;
