using SchoolConnect.Common.Domain.Primitives;
using SchoolConnect.Institution.Domain.Enums;
using SchoolConnect.Institution.Domain.Events;
using SchoolConnect.Institution.Domain.Primitives;

namespace SchoolConnect.Institution.Domain.Entities;

public class StaffMember : AggregateRoot
{
    public Guid InstituteId { get; private set; }
    public Guid UserId { get; private set; }
    public string EmployeeCode { get; private set; } = string.Empty;
    public string FirstName { get; private set; } = string.Empty;
    public string LastName { get; private set; } = string.Empty;
    public string? JobTitle { get; private set; }
    public string? Department { get; private set; }
    public EmploymentType EmploymentType { get; private set; }
    public DateTime JoinDate { get; private set; }
    public DateTime? TerminationDate { get; private set; }
    public StaffStatus Status { get; private set; }
    public List<string> Qualifications { get; private set; } = new();
    public List<string> Specializations { get; private set; } = new();
    public int? MaxTeachingHoursPerWeek { get; private set; }

    private StaffMember() { }

    public static StaffMember Create(
        Guid instituteId,
        Guid userId,
        string employeeCode,
        string firstName,
        string lastName,
        EmploymentType employmentType,
        DateTime joinDate,
        string? jobTitle = null,
        string? department = null,
        List<string>? qualifications = null,
        List<string>? specializations = null,
        int? maxTeachingHoursPerWeek = null
    )
    {
        var staff = new StaffMember
        {
            InstituteId = instituteId,
            UserId = userId,
            EmployeeCode = employeeCode,
            FirstName = firstName,
            LastName = lastName,
            JobTitle = jobTitle,
            Department = department,
            EmploymentType = employmentType,
            JoinDate = joinDate,
            Status = StaffStatus.Active,
            Qualifications = qualifications ?? new List<string>(),
            Specializations = specializations ?? new List<string>(),
            MaxTeachingHoursPerWeek = maxTeachingHoursPerWeek
        };

        staff.AddDomainEvent(
            new StaffOnboardedEvent
            {
                AggregateId = staff.Id,
                EventType = nameof(StaffOnboardedEvent),
                InstituteId = instituteId,
                EmployeeCode = employeeCode,
                FirstName = firstName,
                LastName = lastName
            }
        );

        return staff;
    }

    public void Update(
        string firstName,
        string lastName,
        string? jobTitle = null,
        string? department = null,
        List<string>? qualifications = null,
        List<string>? specializations = null,
        int? maxTeachingHoursPerWeek = null
    )
    {
        FirstName = firstName;
        LastName = lastName;
        JobTitle = jobTitle;
        Department = department;
        if (qualifications != null)
            Qualifications = qualifications;
        if (specializations != null)
            Specializations = specializations;
        MaxTeachingHoursPerWeek = maxTeachingHoursPerWeek;
        MarkAsUpdated();

        AddDomainEvent(
            new StaffUpdatedEvent
            {
                AggregateId = Id,
                EventType = nameof(StaffUpdatedEvent),
                FirstName = firstName,
                LastName = lastName
            }
        );
    }

    public void SetOnLeave()
    {
        Status = StaffStatus.OnLeave;
        MarkAsUpdated();
    }

    public void Suspend()
    {
        Status = StaffStatus.Suspended;
        MarkAsUpdated();
    }

    public void Activate()
    {
        Status = StaffStatus.Active;
        MarkAsUpdated();
    }

    public void Terminate(DateTime terminationDate)
    {
        Status = StaffStatus.Terminated;
        TerminationDate = terminationDate;
        MarkAsUpdated();

        AddDomainEvent(
            new StaffOffboardedEvent
            {
                AggregateId = Id,
                EventType = nameof(StaffOffboardedEvent),
                TerminationDate = terminationDate
            }
        );
    }
}
