using MediatR;
using SchoolConnect.Institution.Domain.DTOs;

namespace SchoolConnect.Institution.Application.Queries.Resources;

public record GetResourceByIdQuery(Guid Id) : IRequest<ResourceDto?>;

public record GetResourcesByCentreQuery(Guid CentreId) : IRequest<IEnumerable<ResourceDto>>;

public record GetResourceAllocationsQuery(Guid ResourceId) : IRequest<IEnumerable<ResourceAllocationDto>>;

public record GetResourceInventoryReportQuery(Guid CentreId) : IRequest<ResourceInventoryReportDto?>;
