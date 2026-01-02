using MediatR;
using SchoolConnect.Institution.Domain.DTOs;

namespace SchoolConnect.Institution.Application.Commands.Centres;

public record CreateCentreCommand(
    Guid InstituteId,
    string Name,
    string Code,
    string Street,
    string City,
    string State,
    string PostalCode,
    string Country,
    string Email,
    string Phone,
    string? Website,
    int Capacity,
    TimeOnly StartTime,
    TimeOnly EndTime,
    List<DayOfWeek> WorkingDays,
    double? Latitude = null,
    double? Longitude = null,
    Guid? CentreAdminId = null
) : IRequest<CentreDto>;

public record UpdateCentreCommand(
    Guid Id,
    string Name,
    string Street,
    string City,
    string State,
    string PostalCode,
    string Country,
    string Email,
    string Phone,
    string? Website,
    int Capacity,
    TimeOnly StartTime,
    TimeOnly EndTime,
    List<DayOfWeek> WorkingDays,
    double? Latitude = null,
    double? Longitude = null,
    Guid? CentreAdminId = null
) : IRequest<CentreDto>;

public record DeactivateCentreCommand(
    Guid Id
) : IRequest<bool>;
