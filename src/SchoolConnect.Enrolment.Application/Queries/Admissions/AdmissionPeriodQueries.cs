using MediatR;
using SchoolConnect.Enrolment.Application.DTOs;

namespace SchoolConnect.Enrolment.Application.Queries.Admissions;

public record GetAdmissionPeriodByIdQuery : IRequest<AdmissionPeriodDto?>
{
    public Guid Id { get; init; }
}

public record GetAdmissionPeriodsByInstituteQuery : IRequest<IEnumerable<AdmissionPeriodDto>>
{
    public Guid InstituteId { get; init; }
}
