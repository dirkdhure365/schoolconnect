using MediatR;
using SchoolConnect.Institution.Domain.DTOs;
using SchoolConnect.Institution.Domain.Enums;

namespace SchoolConnect.Institution.Application.Commands.Resources;

public record CreateResourceCommand(
    Guid CentreId,
    string Name,
    ResourceType Type,
    string? SerialNumber = null,
    string? Description = null,
    DateTime? AcquisitionDate = null,
    decimal? Value = null,
    string? Currency = null,
    string? Location = null
) : IRequest<ResourceDto>;

public record UpdateResourceCommand(
    Guid Id,
    string Name,
    string? Description = null,
    decimal? Value = null,
    string? Location = null
) : IRequest<ResourceDto>;

public record DeleteResourceCommand(
    Guid Id
) : IRequest<bool>;

public record AllocateResourceCommand(
    Guid ResourceId,
    Guid AllocatedToId,
    string AllocatedToType,
    string AllocatedToName,
    Guid AllocatedBy,
    DateTime StartDate,
    DateTime? EndDate = null,
    string? Notes = null
) : IRequest<ResourceAllocationDto>;

public record ReturnResourceCommand(
    Guid AllocationId,
    ResourceCondition ConditionOnReturn,
    string? Notes = null
) : IRequest<ResourceAllocationDto>;

public record ReportResourceDamageCommand(
    Guid AllocationId,
    ResourceCondition Condition,
    string Notes
) : IRequest<ResourceAllocationDto>;
