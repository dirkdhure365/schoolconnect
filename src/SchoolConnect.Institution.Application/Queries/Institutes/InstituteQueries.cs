using MediatR;
using SchoolConnect.Institution.Domain.DTOs;

namespace SchoolConnect.Institution.Application.Queries.Institutes;

public record GetInstituteByIdQuery(Guid Id) : IRequest<InstituteDto?>;

public record GetInstitutesQuery() : IRequest<IEnumerable<InstituteDto>>;

public record GetInstituteSettingsQuery(Guid InstituteId) : IRequest<InstituteSettingsDto?>;

public record GetInstituteDashboardQuery(Guid InstituteId) : IRequest<InstituteDashboardDto?>;
