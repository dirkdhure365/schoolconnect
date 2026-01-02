using MediatR;
using SchoolConnect.EducationSystem.Application.Commands;
using SchoolConnect.EducationSystem.Application.DTOs;
using SchoolConnect.EducationSystem.Application.Interfaces;
using SchoolConnect.EducationSystem.Domain.Entities;

namespace SchoolConnect.EducationSystem.Application.Handlers;

public class CreateCountryHandler : IRequestHandler<CreateCountryCommand, CountryDto>
{
    private readonly IRepository<Country> _repository;

    public CreateCountryHandler(IRepository<Country> repository)
    {
        _repository = repository;
    }

    public async Task<CountryDto> Handle(CreateCountryCommand request, CancellationToken cancellationToken)
    {
        var country = Country.Create(request.Name, request.Code, request.Region);
        var created = await _repository.AddAsync(country);
        
        return new CountryDto(created.Id, created.Name, created.Code, created.Region, created.CreatedAt, created.UpdatedAt);
    }
}

public class UpdateCountryHandler : IRequestHandler<UpdateCountryCommand, CountryDto>
{
    private readonly IRepository<Country> _repository;

    public UpdateCountryHandler(IRepository<Country> repository)
    {
        _repository = repository;
    }

    public async Task<CountryDto> Handle(UpdateCountryCommand request, CancellationToken cancellationToken)
    {
        var country = await _repository.GetByIdAsync(request.Id);
        if (country == null)
            throw new KeyNotFoundException($"Country with ID {request.Id} not found");

        country.Update(request.Name, request.Code, request.Region);
        var updated = await _repository.UpdateAsync(country);
        
        return new CountryDto(updated.Id, updated.Name, updated.Code, updated.Region, updated.CreatedAt, updated.UpdatedAt);
    }
}

public class DeleteCountryHandler : IRequestHandler<DeleteCountryCommand, bool>
{
    private readonly IRepository<Country> _repository;

    public DeleteCountryHandler(IRepository<Country> repository)
    {
        _repository = repository;
    }

    public async Task<bool> Handle(DeleteCountryCommand request, CancellationToken cancellationToken)
    {
        return await _repository.DeleteAsync(request.Id);
    }
}
