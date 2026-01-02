using MediatR;
using SchoolConnect.Enrolment.Domain.Enums;

namespace SchoolConnect.Enrolment.Application.Commands.Students;

public record CreateStudentCommand : IRequest<Guid>
{
    public Guid InstituteId { get; init; }
    public string StudentCode { get; init; } = string.Empty;
    public string FirstName { get; init; } = string.Empty;
    public string LastName { get; init; } = string.Empty;
    public string? MiddleName { get; init; }
    public DateTime DateOfBirth { get; init; }
    public Gender Gender { get; init; }
    public string? Nationality { get; init; }
    public string? IdNumber { get; init; }
    public string? Email { get; init; }
    public string? PhoneNumber { get; init; }
    public Guid? ApplicationId { get; init; }
    public Guid? UserId { get; init; }
}

public record UpdateStudentCommand : IRequest<Unit>
{
    public Guid Id { get; init; }
    public string FirstName { get; init; } = string.Empty;
    public string LastName { get; init; } = string.Empty;
    public string? MiddleName { get; init; }
    public string? Nationality { get; init; }
    public string? IdNumber { get; init; }
    public string? Email { get; init; }
    public string? PhoneNumber { get; init; }
}

public record WithdrawStudentCommand : IRequest<Unit>
{
    public Guid Id { get; init; }
    public string Reason { get; init; } = string.Empty;
}
