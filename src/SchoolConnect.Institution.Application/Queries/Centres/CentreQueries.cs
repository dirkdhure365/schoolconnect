using MediatR;
using SchoolConnect.Institution.Application.DTOs;

namespace SchoolConnect.Institution.Application.Queries.Centres;

public record GetCentreByIdQuery : IRequest<CentreDto?>
{
    public Guid Id { get; init; }
}

public record GetCentresByInstituteQuery : IRequest<List<CentreSummaryDto>>
{
    public Guid InstituteId { get; init; }
}

public record GetCentreDashboardQuery : IRequest<CentreDashboardDto?>
{
    public Guid Id { get; init; }
}
