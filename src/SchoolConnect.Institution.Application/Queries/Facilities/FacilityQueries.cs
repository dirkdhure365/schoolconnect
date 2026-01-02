using MediatR;
using SchoolConnect.Institution.Domain.DTOs;

namespace SchoolConnect.Institution.Application.Queries.Facilities;

public record GetFacilityByIdQuery(Guid Id) : IRequest<FacilityDto?>;

public record GetFacilitiesByCentreQuery(Guid CentreId) : IRequest<IEnumerable<FacilityDto>>;

public record GetFacilityBookingsQuery(Guid FacilityId) : IRequest<IEnumerable<FacilityBookingDto>>;

public record GetFacilityScheduleQuery(Guid FacilityId, DateTime StartDate, DateTime EndDate) : IRequest<FacilityScheduleDto?>;
