using MediatR;
using SchoolConnect.Institution.Domain.DTOs;
using SchoolConnect.Institution.Domain.Enums;

namespace SchoolConnect.Institution.Application.Commands.Institutes;

// Create Institute
public record CreateInstituteCommand(
    string Name,
    string Code,
    InstituteType Type,
    string Email,
    string Phone,
    string? Website,
    string Street,
    string City,
    string State,
    string PostalCode,
    string Country,
    string Timezone,
    int AcademicYearStartMonth,
    string? Description = null,
    Guid? SubscriptionId = null
) : IRequest<InstituteDto>;

// Update Institute
public record UpdateInstituteCommand(
    Guid Id,
    string Name,
    string? Description,
    string Email,
    string Phone,
    string? Website,
    string Street,
    string City,
    string State,
    string PostalCode,
    string Country,
    string Timezone,
    int AcademicYearStartMonth
) : IRequest<InstituteDto>;

// Update Institute Settings
public record UpdateInstituteSettingsCommand(
    Guid InstituteId,
    string DefaultLanguage,
    string DateFormat,
    string TimeFormat,
    string Currency,
    List<string> EnabledFeatures
) : IRequest<InstituteDto>;

// Upload Institute Logo
public record UploadInstituteLogoCommand(
    Guid InstituteId,
    string LogoUrl
) : IRequest<InstituteDto>;

// Deactivate Institute
public record DeactivateInstituteCommand(
    Guid InstituteId
) : IRequest<bool>;
