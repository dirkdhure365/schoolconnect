using MediatR;
using SchoolConnect.EducationSystem.Application.Queries;
using SchoolConnect.EducationSystem.Application.DTOs;
using SchoolConnect.EducationSystem.Application.Interfaces;
using SchoolConnect.EducationSystem.Domain.Entities;

namespace SchoolConnect.EducationSystem.Application.Handlers;

public class GetAllCountriesHandler : IRequestHandler<GetAllCountriesQuery, List<CountryDto>>
{
    private readonly IRepository<Country> _repository;

    public GetAllCountriesHandler(IRepository<Country> repository)
    {
        _repository = repository;
    }

    public async Task<List<CountryDto>> Handle(GetAllCountriesQuery request, CancellationToken cancellationToken)
    {
        var countries = await _repository.GetAllAsync();
        return countries.Select(c => new CountryDto(c.Id, c.Name, c.Code, c.Region, c.CreatedAt, c.UpdatedAt)).ToList();
    }
}

public class GetCountryByIdHandler : IRequestHandler<GetCountryByIdQuery, CountryDto?>
{
    private readonly IRepository<Country> _repository;

    public GetCountryByIdHandler(IRepository<Country> repository)
    {
        _repository = repository;
    }

    public async Task<CountryDto?> Handle(GetCountryByIdQuery request, CancellationToken cancellationToken)
    {
        var country = await _repository.GetByIdAsync(request.Id);
        return country != null ? new CountryDto(country.Id, country.Name, country.Code, country.Region, country.CreatedAt, country.UpdatedAt) : null;
    }
}
