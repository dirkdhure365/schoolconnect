using AutoMapper;
using MediatR;
using SchoolConnect.Institution.Application.DTOs;
using SchoolConnect.Institution.Application.Queries.Institutes;
using SchoolConnect.Institution.Domain.Interfaces;

namespace SchoolConnect.Institution.Application.Handlers.Institutes;

public class GetInstituteByIdQueryHandler : IRequestHandler<GetInstituteByIdQuery, InstituteDto?>
{
    private readonly IInstituteRepository _repository;
    private readonly IMapper _mapper;

    public GetInstituteByIdQueryHandler(IInstituteRepository repository, IMapper mapper)
    {
        _repository = repository;
        _mapper = mapper;
    }

    public async Task<InstituteDto?> Handle(GetInstituteByIdQuery request, CancellationToken cancellationToken)
    {
        var institute = await _repository.GetByIdAsync(request.Id, cancellationToken);
        return institute == null ? null : _mapper.Map<InstituteDto>(institute);
    }
}

public class GetInstitutesQueryHandler : IRequestHandler<GetInstitutesQuery, List<InstituteSummaryDto>>
{
    private readonly IInstituteRepository _repository;
    private readonly IMapper _mapper;

    public GetInstitutesQueryHandler(IInstituteRepository repository, IMapper mapper)
    {
        _repository = repository;
        _mapper = mapper;
    }

    public async Task<List<InstituteSummaryDto>> Handle(GetInstitutesQuery request, CancellationToken cancellationToken)
    {
        var institutes = await _repository.GetAllAsync(cancellationToken);
        return _mapper.Map<List<InstituteSummaryDto>>(institutes);
    }
}

public class GetInstituteSettingsQueryHandler : IRequestHandler<GetInstituteSettingsQuery, InstituteSettingsDto?>
{
    private readonly IInstituteRepository _repository;
    private readonly IMapper _mapper;

    public GetInstituteSettingsQueryHandler(IInstituteRepository repository, IMapper mapper)
    {
        _repository = repository;
        _mapper = mapper;
    }

    public async Task<InstituteSettingsDto?> Handle(GetInstituteSettingsQuery request, CancellationToken cancellationToken)
    {
        var institute = await _repository.GetByIdAsync(request.Id, cancellationToken);
        return institute == null ? null : _mapper.Map<InstituteSettingsDto>(institute.Settings);
    }
}

public class GetInstituteDashboardQueryHandler : IRequestHandler<GetInstituteDashboardQuery, InstituteDashboardDto?>
{
    private readonly IInstituteRepository _repository;
    private readonly ICentreRepository _centreRepository;
    private readonly IStaffRepository _staffRepository;
    private readonly ITeamRepository _teamRepository;

    public GetInstituteDashboardQueryHandler(
        IInstituteRepository repository,
        ICentreRepository centreRepository,
        IStaffRepository staffRepository,
        ITeamRepository teamRepository)
    {
        _repository = repository;
        _centreRepository = centreRepository;
        _staffRepository = staffRepository;
        _teamRepository = teamRepository;
    }

    public async Task<InstituteDashboardDto?> Handle(GetInstituteDashboardQuery request, CancellationToken cancellationToken)
    {
        var institute = await _repository.GetByIdAsync(request.Id, cancellationToken);
        if (institute == null) return null;

        var centres = await _centreRepository.GetByInstituteIdAsync(request.Id, cancellationToken);
        var staff = await _staffRepository.GetByInstituteIdAsync(request.Id, cancellationToken);
        var teams = await _teamRepository.GetByInstituteIdAsync(request.Id, cancellationToken);

        return new InstituteDashboardDto
        {
            InstituteId = institute.Id,
            Name = institute.Name,
            TotalCentres = centres.Count,
            TotalStaff = staff.Count,
            TotalTeams = teams.Count,
            Status = institute.Status
        };
    }
}
