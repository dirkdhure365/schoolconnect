using SchoolConnect.Enrolment.Domain.Enums;

namespace SchoolConnect.Enrolment.Application.DTOs;

public record StudentDto
{
    public Guid Id { get; init; }
    public Guid InstituteId { get; init; }
    public Guid? UserId { get; init; }
    public string StudentCode { get; init; } = string.Empty;
    public string FirstName { get; init; } = string.Empty;
    public string LastName { get; init; } = string.Empty;
    public string? MiddleName { get; init; }
    public DateTime DateOfBirth { get; init; }
    public Gender Gender { get; init; }
    public string? Nationality { get; init; }
    public string? IdNumber { get; init; }
    public string? PassportNumber { get; init; }
    public string? PhotoUrl { get; init; }
    public string? Email { get; init; }
    public string? PhoneNumber { get; init; }
    public StudentStatus Status { get; init; }
    public DateTime EnrolledAt { get; init; }
    public DateTime? GraduatedAt { get; init; }
    public DateTime? WithdrawnAt { get; init; }
    public string? WithdrawalReason { get; init; }
}
