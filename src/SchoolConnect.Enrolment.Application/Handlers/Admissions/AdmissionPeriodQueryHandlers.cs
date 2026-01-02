using AutoMapper;
using MediatR;
using SchoolConnect.Enrolment.Application.DTOs;
using SchoolConnect.Enrolment.Application.Queries.Admissions;
using SchoolConnect.Enrolment.Domain.Interfaces;

namespace SchoolConnect.Enrolment.Application.Handlers.Admissions;

public class GetAdmissionPeriodByIdQueryHandler : IRequestHandler<GetAdmissionPeriodByIdQuery, AdmissionPeriodDto?>
{
    private readonly IAdmissionPeriodRepository _repository;
    private readonly IMapper _mapper;

    public GetAdmissionPeriodByIdQueryHandler(IAdmissionPeriodRepository repository, IMapper mapper)
    {
        _repository = repository;
        _mapper = mapper;
    }

    public async Task<AdmissionPeriodDto?> Handle(GetAdmissionPeriodByIdQuery request, CancellationToken cancellationToken)
    {
        var period = await _repository.GetByIdAsync(request.Id, cancellationToken);
        return period != null ? _mapper.Map<AdmissionPeriodDto>(period) : null;
    }
}

public class GetAdmissionPeriodsByInstituteQueryHandler : IRequestHandler<GetAdmissionPeriodsByInstituteQuery, IEnumerable<AdmissionPeriodDto>>
{
    private readonly IAdmissionPeriodRepository _repository;
    private readonly IMapper _mapper;

    public GetAdmissionPeriodsByInstituteQueryHandler(IAdmissionPeriodRepository repository, IMapper mapper)
    {
        _repository = repository;
        _mapper = mapper;
    }

    public async Task<IEnumerable<AdmissionPeriodDto>> Handle(GetAdmissionPeriodsByInstituteQuery request, CancellationToken cancellationToken)
    {
        var periods = await _repository.GetByInstituteAsync(request.InstituteId, cancellationToken);
        return _mapper.Map<IEnumerable<AdmissionPeriodDto>>(periods);
    }
}
