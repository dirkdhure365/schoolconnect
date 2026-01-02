using MediatR;
using SchoolConnect.Institution.Domain.Enums;

namespace SchoolConnect.Institution.Application.Commands.Institutes;

public record CreateInstituteCommand : IRequest<Guid>
{
    public string Name { get; init; } = string.Empty;
    public string Code { get; init; } = string.Empty;
    public InstituteType Type { get; init; }
    public string Email { get; init; } = string.Empty;
    public string Phone { get; init; } = string.Empty;
    public string? Website { get; init; }
    public string Street { get; init; } = string.Empty;
    public string City { get; init; } = string.Empty;
    public string State { get; init; } = string.Empty;
    public string PostalCode { get; init; } = string.Empty;
    public string Country { get; init; } = string.Empty;
    public string Timezone { get; init; } = "UTC";
    public int AcademicYearStartMonth { get; init; } = 1;
    public string? Description { get; init; }
}

public record UpdateInstituteCommand : IRequest<Unit>
{
    public Guid Id { get; init; }
    public string Name { get; init; } = string.Empty;
    public string? Description { get; init; }
    public string Email { get; init; } = string.Empty;
    public string Phone { get; init; } = string.Empty;
    public string? Website { get; init; }
    public string Street { get; init; } = string.Empty;
    public string City { get; init; } = string.Empty;
    public string State { get; init; } = string.Empty;
    public string PostalCode { get; init; } = string.Empty;
    public string Country { get; init; } = string.Empty;
    public string Timezone { get; init; } = "UTC";
    public int AcademicYearStartMonth { get; init; } = 1;
}

public record DeactivateInstituteCommand : IRequest<Unit>
{
    public Guid Id { get; init; }
}

public record UpdateInstituteSettingsCommand : IRequest<Unit>
{
    public Guid Id { get; init; }
    public string DefaultLanguage { get; init; } = "en";
    public string DateFormat { get; init; } = "yyyy-MM-dd";
    public string TimeFormat { get; init; } = "HH:mm";
    public string Currency { get; init; } = "USD";
    public List<string> EnabledFeatures { get; init; } = [];
}

public record UploadInstituteLogoCommand : IRequest<Unit>
{
    public Guid Id { get; init; }
    public string LogoUrl { get; init; } = string.Empty;
}
