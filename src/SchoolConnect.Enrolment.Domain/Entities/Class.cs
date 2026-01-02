using SchoolConnect.Common.Domain.Primitives;
using SchoolConnect.Enrolment.Domain.Enums;
using SchoolConnect.Enrolment.Domain.Events;
using SchoolConnect.Enrolment.Domain.ValueObjects;

namespace SchoolConnect.Enrolment.Domain.Entities;

public class Class : AggregateRoot
{
    public Guid CohortId { get; private set; }
    public Guid SubjectId { get; private set; }
    public string SubjectName { get; private set; } = string.Empty;
    public string SubjectCode { get; private set; } = string.Empty;
    public string Name { get; private set; } = string.Empty;
    public Guid? TeacherId { get; private set; }
    public string? TeacherName { get; private set; }
    public Guid? FacilityId { get; private set; }
    public string? FacilityName { get; private set; }
    public int Capacity { get; private set; }
    public int CurrentCount { get; private set; }
    public ScheduleInfo? Schedule { get; private set; }
    public ClassStatus Status { get; private set; }

    private Class() { }

    public static Class Create(
        Guid cohortId,
        Guid subjectId,
        string subjectName,
        string subjectCode,
        string name,
        int capacity,
        Guid? teacherId = null,
        string? teacherName = null,
        Guid? facilityId = null,
        string? facilityName = null,
        ScheduleInfo? schedule = null)
    {
        var @class = new Class
        {
            CohortId = cohortId,
            SubjectId = subjectId,
            SubjectName = subjectName,
            SubjectCode = subjectCode,
            Name = name,
            TeacherId = teacherId,
            TeacherName = teacherName,
            FacilityId = facilityId,
            FacilityName = facilityName,
            Capacity = capacity,
            CurrentCount = 0,
            Schedule = schedule,
            Status = ClassStatus.Active
        };

        @class.AddDomainEvent(new ClassCreatedEvent
        {
            AggregateId = @class.Id,
            AggregateType = nameof(Class),
            CohortId = cohortId,
            SubjectId = subjectId,
            Name = name,
            Capacity = capacity
        });

        return @class;
    }

    public void Update(
        string name,
        int capacity,
        Guid? facilityId = null,
        string? facilityName = null,
        ScheduleInfo? schedule = null)
    {
        Name = name;
        Capacity = capacity;
        FacilityId = facilityId;
        FacilityName = facilityName;
        Schedule = schedule;
        UpdatedAt = DateTime.UtcNow;
    }

    public void AssignTeacher(Guid teacherId, string teacherName)
    {
        TeacherId = teacherId;
        TeacherName = teacherName;
        UpdatedAt = DateTime.UtcNow;

        AddDomainEvent(new TeacherAssignedToClassEvent
        {
            AggregateId = Id,
            AggregateType = nameof(Class),
            ClassId = Id,
            TeacherId = teacherId,
            TeacherName = teacherName
        });
    }

    public void IncrementCount()
    {
        CurrentCount++;
        UpdatedAt = DateTime.UtcNow;
    }

    public void DecrementCount()
    {
        if (CurrentCount > 0)
        {
            CurrentCount--;
            UpdatedAt = DateTime.UtcNow;
        }
    }

    public void Complete()
    {
        Status = ClassStatus.Completed;
        UpdatedAt = DateTime.UtcNow;
    }

    public void Deactivate()
    {
        Status = ClassStatus.Inactive;
        UpdatedAt = DateTime.UtcNow;
    }

    protected override void When(DomainEvent @event)
    {
        // Event sourcing support - not implemented in this version
    }
}
