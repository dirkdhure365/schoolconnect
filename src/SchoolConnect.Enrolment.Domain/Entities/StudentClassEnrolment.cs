using SchoolConnect.Common.Domain.Primitives;
using SchoolConnect.Enrolment.Domain.Enums;

namespace SchoolConnect.Enrolment.Domain.Entities;

public class StudentClassEnrolment : Entity
{
    public Guid StudentEnrolmentId { get; private set; }
    public Guid StudentId { get; private set; }
    public Guid ClassId { get; private set; }
    public EnrolmentStatus Status { get; private set; }
    public DateTime EnrolledAt { get; private set; }
    public DateTime? WithdrawnAt { get; private set; }
    public string? WithdrawalReason { get; private set; }

    private StudentClassEnrolment() { }

    public static StudentClassEnrolment Create(
        Guid studentEnrolmentId,
        Guid studentId,
        Guid classId)
    {
        return new StudentClassEnrolment
        {
            StudentEnrolmentId = studentEnrolmentId,
            StudentId = studentId,
            ClassId = classId,
            Status = EnrolmentStatus.Active,
            EnrolledAt = DateTime.UtcNow
        };
    }

    public void Withdraw(string reason)
    {
        Status = EnrolmentStatus.Withdrawn;
        WithdrawalReason = reason;
        WithdrawnAt = DateTime.UtcNow;
        UpdatedAt = DateTime.UtcNow;
    }

    public void Reactivate()
    {
        Status = EnrolmentStatus.Active;
        WithdrawnAt = null;
        WithdrawalReason = null;
        UpdatedAt = DateTime.UtcNow;
    }
}
