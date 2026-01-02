using SchoolConnect.Common.Domain.Primitives;
using SchoolConnect.Enrolment.Domain.Enums;
using SchoolConnect.Enrolment.Domain.Events;

namespace SchoolConnect.Enrolment.Domain.Entities;

public class StudentEnrolment : AggregateRoot
{
    public Guid StudentId { get; private set; }
    public Guid StreamId { get; private set; }
    public Guid CohortId { get; private set; }
    public int CurrentGradeLevel { get; private set; }
    public EnrolmentStatus Status { get; private set; }
    public DateTime EnrolledAt { get; private set; }
    public Guid EnrolledBy { get; private set; }
    public DateTime? WithdrawnAt { get; private set; }
    public string? WithdrawalReason { get; private set; }
    public DateTime? CompletedAt { get; private set; }

    private StudentEnrolment() { }

    public static StudentEnrolment Create(
        Guid studentId,
        Guid streamId,
        Guid cohortId,
        int currentGradeLevel,
        Guid enrolledBy)
    {
        var enrolment = new StudentEnrolment
        {
            StudentId = studentId,
            StreamId = streamId,
            CohortId = cohortId,
            CurrentGradeLevel = currentGradeLevel,
            Status = EnrolmentStatus.Active,
            EnrolledAt = DateTime.UtcNow,
            EnrolledBy = enrolledBy
        };

        enrolment.AddDomainEvent(new StudentEnrolledEvent
        {
            AggregateId = enrolment.Id,
            AggregateType = nameof(StudentEnrolment),
            StudentId = studentId,
            StreamId = streamId,
            CohortId = cohortId,
            EnrolledAt = enrolment.EnrolledAt
        });

        return enrolment;
    }

    public void UpdateCohort(Guid cohortId)
    {
        CohortId = cohortId;
        UpdatedAt = DateTime.UtcNow;
    }

    public void Withdraw(string reason)
    {
        Status = EnrolmentStatus.Withdrawn;
        WithdrawalReason = reason;
        WithdrawnAt = DateTime.UtcNow;
        UpdatedAt = DateTime.UtcNow;
    }

    public void Complete()
    {
        Status = EnrolmentStatus.Completed;
        CompletedAt = DateTime.UtcNow;
        UpdatedAt = DateTime.UtcNow;
    }

    public void Transfer()
    {
        Status = EnrolmentStatus.Transferred;
        UpdatedAt = DateTime.UtcNow;
    }

    public void Suspend()
    {
        Status = EnrolmentStatus.Suspended;
        UpdatedAt = DateTime.UtcNow;
    }

    public void Reactivate()
    {
        Status = EnrolmentStatus.Active;
        UpdatedAt = DateTime.UtcNow;
    }

    protected override void When(DomainEvent @event)
    {
        // Event sourcing support - not implemented in this version
    }
}
