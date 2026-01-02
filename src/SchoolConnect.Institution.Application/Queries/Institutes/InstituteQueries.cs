using MediatR;
using SchoolConnect.Institution.Application.DTOs;

namespace SchoolConnect.Institution.Application.Queries.Institutes;

public record GetInstituteByIdQuery : IRequest<InstituteDto?>
{
    public Guid Id { get; init; }
}

public record GetInstitutesQuery : IRequest<List<InstituteSummaryDto>>
{
}

public record GetInstituteSettingsQuery : IRequest<InstituteSettingsDto?>
{
    public Guid Id { get; init; }
}

public record GetInstituteDashboardQuery : IRequest<InstituteDashboardDto?>
{
    public Guid Id { get; init; }
}
