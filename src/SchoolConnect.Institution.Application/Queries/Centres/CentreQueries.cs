using MediatR;
using SchoolConnect.Institution.Domain.DTOs;

namespace SchoolConnect.Institution.Application.Queries.Centres;

public record GetCentreByIdQuery(Guid Id) : IRequest<CentreDto?>;

public record GetCentresByInstituteQuery(Guid InstituteId) : IRequest<IEnumerable<CentreDto>>;

public record GetCentreDashboardQuery(Guid CentreId) : IRequest<CentreDashboardDto?>;
