using MediatR;
using SchoolConnect.Institution.Domain.DTOs;

namespace SchoolConnect.Institution.Application.Queries.Staff;

public record GetStaffByIdQuery(Guid Id) : IRequest<StaffMemberDto?>;

public record GetStaffByInstituteQuery(Guid InstituteId) : IRequest<IEnumerable<StaffMemberDto>>;

public record GetStaffByCentreQuery(Guid CentreId) : IRequest<IEnumerable<StaffMemberDto>>;

public record GetStaffCentresQuery(Guid StaffId) : IRequest<IEnumerable<CentreSummaryDto>>;

public record GetStaffTeamsQuery(Guid StaffId) : IRequest<IEnumerable<TeamDto>>;
