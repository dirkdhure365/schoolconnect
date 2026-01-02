using MediatR;
using SchoolConnect.Institution.Domain.DTOs;
using SchoolConnect.Institution.Domain.Enums;

namespace SchoolConnect.Institution.Application.Commands.Staff;

public record OnboardStaffCommand(
    Guid InstituteId,
    Guid UserId,
    string EmployeeCode,
    string FirstName,
    string LastName,
    EmploymentType EmploymentType,
    DateTime JoinDate,
    string? JobTitle = null,
    string? Department = null,
    List<string>? Qualifications = null,
    List<string>? Specializations = null,
    int? MaxTeachingHoursPerWeek = null
) : IRequest<StaffMemberDto>;

public record UpdateStaffCommand(
    Guid Id,
    string FirstName,
    string LastName,
    string? JobTitle = null,
    string? Department = null,
    int? MaxTeachingHoursPerWeek = null
) : IRequest<StaffMemberDto>;

public record OffboardStaffCommand(
    Guid Id,
    DateTime TerminationDate
) : IRequest<bool>;

public record AssignStaffToCentreCommand(
    Guid StaffMemberId,
    Guid CentreId,
    Guid AssignedBy,
    DateTime StartDate,
    bool IsPrimary = false
) : IRequest<bool>;

public record RemoveStaffFromCentreCommand(
    Guid StaffMemberId,
    Guid CentreId,
    DateTime EndDate
) : IRequest<bool>;
