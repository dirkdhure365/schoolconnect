using AutoMapper;
using MediatR;
using SchoolConnect.Institution.Application.DTOs;
using SchoolConnect.Institution.Application.Queries.Centres;
using SchoolConnect.Institution.Domain.Interfaces;

namespace SchoolConnect.Institution.Application.Handlers.Centres;

public class GetCentreByIdQueryHandler : IRequestHandler<GetCentreByIdQuery, CentreDto?>
{
    private readonly ICentreRepository _repository;
    private readonly IMapper _mapper;

    public GetCentreByIdQueryHandler(ICentreRepository repository, IMapper mapper)
    {
        _repository = repository;
        _mapper = mapper;
    }

    public async Task<CentreDto?> Handle(GetCentreByIdQuery request, CancellationToken cancellationToken)
    {
        var centre = await _repository.GetByIdAsync(request.Id, cancellationToken);
        return centre == null ? null : _mapper.Map<CentreDto>(centre);
    }
}

public class GetCentresByInstituteQueryHandler : IRequestHandler<GetCentresByInstituteQuery, List<CentreSummaryDto>>
{
    private readonly ICentreRepository _repository;
    private readonly IMapper _mapper;

    public GetCentresByInstituteQueryHandler(ICentreRepository repository, IMapper mapper)
    {
        _repository = repository;
        _mapper = mapper;
    }

    public async Task<List<CentreSummaryDto>> Handle(GetCentresByInstituteQuery request, CancellationToken cancellationToken)
    {
        var centres = await _repository.GetByInstituteIdAsync(request.InstituteId, cancellationToken);
        return _mapper.Map<List<CentreSummaryDto>>(centres);
    }
}

public class GetCentreDashboardQueryHandler : IRequestHandler<GetCentreDashboardQuery, CentreDashboardDto?>
{
    private readonly ICentreRepository _centreRepository;
    private readonly IFacilityRepository _facilityRepository;
    private readonly IResourceRepository _resourceRepository;
    private readonly IStaffRepository _staffRepository;

    public GetCentreDashboardQueryHandler(
        ICentreRepository centreRepository,
        IFacilityRepository facilityRepository,
        IResourceRepository resourceRepository,
        IStaffRepository staffRepository)
    {
        _centreRepository = centreRepository;
        _facilityRepository = facilityRepository;
        _resourceRepository = resourceRepository;
        _staffRepository = staffRepository;
    }

    public async Task<CentreDashboardDto?> Handle(GetCentreDashboardQuery request, CancellationToken cancellationToken)
    {
        var centre = await _centreRepository.GetByIdAsync(request.Id, cancellationToken);
        if (centre == null) return null;

        var facilities = await _facilityRepository.GetByCentreIdAsync(request.Id, cancellationToken);
        var resources = await _resourceRepository.GetByCentreIdAsync(request.Id, cancellationToken);
        var staff = await _staffRepository.GetByCentreIdAsync(request.Id, cancellationToken);

        return new CentreDashboardDto
        {
            CentreId = centre.Id,
            Name = centre.Name,
            TotalFacilities = facilities.Count,
            TotalResources = resources.Count,
            TotalStaff = staff.Count,
            Status = centre.Status
        };
    }
}
