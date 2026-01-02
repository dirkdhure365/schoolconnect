using SchoolConnect.Common.Domain.Primitives;
using SchoolConnect.Enrolment.Domain.Enums;
using SchoolConnect.Enrolment.Domain.Events;

namespace SchoolConnect.Enrolment.Domain.Entities;

public class ExtraCurricularEnrolment : AggregateRoot
{
    public Guid StudentId { get; private set; }
    public ActivityType ActivityType { get; private set; }
    public Guid? ActivityId { get; private set; }
    public string ActivityName { get; private set; } = string.Empty;
    public Guid CentreId { get; private set; }
    public string? Role { get; private set; }
    public EnrolmentStatus Status { get; private set; }
    public DateTime EnrolledAt { get; private set; }
    public DateTime? WithdrawnAt { get; private set; }

    private ExtraCurricularEnrolment() { }

    public static ExtraCurricularEnrolment Create(
        Guid studentId,
        ActivityType activityType,
        string activityName,
        Guid centreId,
        Guid? activityId = null,
        string? role = null)
    {
        var enrolment = new ExtraCurricularEnrolment
        {
            StudentId = studentId,
            ActivityType = activityType,
            ActivityId = activityId,
            ActivityName = activityName,
            CentreId = centreId,
            Role = role,
            Status = EnrolmentStatus.Active,
            EnrolledAt = DateTime.UtcNow
        };

        enrolment.AddDomainEvent(new ExtraCurricularEnrolmentEvent
        {
            AggregateId = enrolment.Id,
            AggregateType = nameof(ExtraCurricularEnrolment),
            StudentId = studentId,
            ActivityName = activityName,
            EnrolledAt = enrolment.EnrolledAt
        });

        return enrolment;
    }

    public void UpdateRole(string? role)
    {
        Role = role;
        UpdatedAt = DateTime.UtcNow;
    }

    public void Withdraw()
    {
        Status = EnrolmentStatus.Withdrawn;
        WithdrawnAt = DateTime.UtcNow;
        UpdatedAt = DateTime.UtcNow;
    }

    public void Reactivate()
    {
        Status = EnrolmentStatus.Active;
        WithdrawnAt = null;
        UpdatedAt = DateTime.UtcNow;
    }

    protected override void When(DomainEvent @event)
    {
        // Event sourcing support - not implemented in this version
    }
}
