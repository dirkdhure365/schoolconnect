using MediatR;

namespace SchoolConnect.Institution.Application.Commands.Centres;

public record CreateCentreCommand : IRequest<Guid>
{
    public Guid InstituteId { get; init; }
    public string Name { get; init; } = string.Empty;
    public string Code { get; init; } = string.Empty;
    public string Email { get; init; } = string.Empty;
    public string Phone { get; init; } = string.Empty;
    public string? Website { get; init; }
    public string Street { get; init; } = string.Empty;
    public string City { get; init; } = string.Empty;
    public string State { get; init; } = string.Empty;
    public string PostalCode { get; init; } = string.Empty;
    public string Country { get; init; } = string.Empty;
    public int Capacity { get; init; }
    public double? Latitude { get; init; }
    public double? Longitude { get; init; }
    public Guid? CentreAdminId { get; init; }
}

public record UpdateCentreCommand : IRequest<Unit>
{
    public Guid Id { get; init; }
    public string Name { get; init; } = string.Empty;
    public string Email { get; init; } = string.Empty;
    public string Phone { get; init; } = string.Empty;
    public string? Website { get; init; }
    public string Street { get; init; } = string.Empty;
    public string City { get; init; } = string.Empty;
    public string State { get; init; } = string.Empty;
    public string PostalCode { get; init; } = string.Empty;
    public string Country { get; init; } = string.Empty;
    public int Capacity { get; init; }
    public double? Latitude { get; init; }
    public double? Longitude { get; init; }
    public Guid? CentreAdminId { get; init; }
}

public record DeactivateCentreCommand : IRequest<Unit>
{
    public Guid Id { get; init; }
}
