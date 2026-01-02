using MediatR;
using SchoolConnect.EducationSystem.Application.Queries;
using SchoolConnect.EducationSystem.Application.DTOs;
using SchoolConnect.EducationSystem.Application.Interfaces;

namespace SchoolConnect.EducationSystem.Application.Handlers;

// EducationSystem Query Handlers
public class GetAllEducationSystemsHandler : IRequestHandler<GetAllEducationSystemsQuery, List<EducationSystemDto>>
{
    private readonly IRepository<Domain.Entities.EducationSystem> _repository;
    public GetAllEducationSystemsHandler(IRepository<Domain.Entities.EducationSystem> repository) => _repository = repository;

    public async Task<List<EducationSystemDto>> Handle(GetAllEducationSystemsQuery request, CancellationToken cancellationToken)
    {
        var systems = await _repository.GetAllAsync();
        return systems.Select(s => new EducationSystemDto(s.Id, s.CountryId, s.Name, s.Description, s.CreatedAt, s.UpdatedAt)).ToList();
    }
}

public class GetEducationSystemByIdHandler : IRequestHandler<GetEducationSystemByIdQuery, EducationSystemDto?>
{
    private readonly IRepository<Domain.Entities.EducationSystem> _repository;
    public GetEducationSystemByIdHandler(IRepository<Domain.Entities.EducationSystem> repository) => _repository = repository;

    public async Task<EducationSystemDto?> Handle(GetEducationSystemByIdQuery request, CancellationToken cancellationToken)
    {
        var system = await _repository.GetByIdAsync(request.Id);
        return system != null ? new EducationSystemDto(system.Id, system.CountryId, system.Name, system.Description, system.CreatedAt, system.UpdatedAt) : null;
    }
}

public class GetEducationSystemsByCountryHandler : IRequestHandler<GetEducationSystemsByCountryQuery, List<EducationSystemDto>>
{
    private readonly IEducationSystemRepository _repository;
    public GetEducationSystemsByCountryHandler(IEducationSystemRepository repository) => _repository = repository;

    public async Task<List<EducationSystemDto>> Handle(GetEducationSystemsByCountryQuery request, CancellationToken cancellationToken)
    {
        var systems = await _repository.GetByCountryIdAsync(request.CountryId);
        return systems.Select(s => new EducationSystemDto(s.Id, s.CountryId, s.Name, s.Description, s.CreatedAt, s.UpdatedAt)).ToList();
    }
}
