using MediatR;
using SchoolConnect.Institution.Domain.DTOs;
using SchoolConnect.Institution.Domain.Enums;

namespace SchoolConnect.Institution.Application.Commands.Facilities;

public record CreateFacilityCommand(
    Guid CentreId,
    string Name,
    FacilityType Type,
    int Capacity,
    bool IsBookable,
    string? Code = null,
    string? Description = null,
    List<string>? Amenities = null,
    int? MinDurationMinutes = null,
    int? MaxDurationMinutes = null,
    int? AdvanceBookingDays = null,
    bool? RequiresApproval = null,
    List<string>? AllowedRoles = null
) : IRequest<FacilityDto>;

public record UpdateFacilityCommand(
    Guid Id,
    string Name,
    int Capacity,
    string? Description = null,
    List<string>? Amenities = null
) : IRequest<FacilityDto>;

public record DeleteFacilityCommand(
    Guid Id
) : IRequest<bool>;

public record BookFacilityCommand(
    Guid FacilityId,
    Guid BookedBy,
    string Purpose,
    DateTime StartTime,
    DateTime EndTime,
    string? Description = null,
    string? Notes = null
) : IRequest<FacilityBookingDto>;

public record UpdateBookingCommand(
    Guid Id,
    DateTime StartTime,
    DateTime EndTime,
    string? Notes = null
) : IRequest<FacilityBookingDto>;

public record CancelBookingCommand(
    Guid Id,
    string? Reason = null
) : IRequest<bool>;

public record ApproveBookingCommand(
    Guid Id,
    Guid ApprovedBy
) : IRequest<FacilityBookingDto>;
