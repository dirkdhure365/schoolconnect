using SchoolConnect.Common.Domain.Primitives;
using SchoolConnect.Institution.Domain.Enums;
using SchoolConnect.Institution.Domain.Events;
using SchoolConnect.Institution.Domain.Primitives;
using SchoolConnect.Institution.Domain.ValueObjects;
using SchoolConnect.Institution.Domain.ValueObjects;

namespace SchoolConnect.Institution.Domain.Entities;

public class InstituteSettings
{
    public string DefaultLanguage { get; set; } = "en";
    public string DateFormat { get; set; } = "yyyy-MM-dd";
    public string TimeFormat { get; set; } = "HH:mm";
    public string Currency { get; set; } = "USD";
    public GradingPolicy? GradingPolicy { get; set; }
    public AttendancePolicy? AttendancePolicy { get; set; }
    public List<string> EnabledFeatures { get; set; } = [];
}

public class GradingPolicy
{
    public string Name { get; set; } = string.Empty;
    public decimal PassingGrade { get; set; }
    public List<GradeRange> GradeRanges { get; set; } = [];
}

public class GradeRange
{
    public string Grade { get; set; } = string.Empty;
    public decimal MinPercentage { get; set; }
    public decimal MaxPercentage { get; set; }
}

public class AttendancePolicy
{
    public decimal MinimumAttendancePercentage { get; set; }
    public bool TrackLateArrivals { get; set; }
    public int GracePeriodMinutes { get; set; }
}

public class Institute : AggregateRoot
{
    public string Name { get; private set; } = string.Empty;
    public string Code { get; private set; } = string.Empty;
    public string? LogoUrl { get; private set; }
    public string? Description { get; private set; }
    public InstituteType Type { get; private set; }
    public InstituteStatus Status { get; private set; }
    public ContactInfo ContactInfo { get; private set; } = null!;
    public Address Address { get; private set; } = null!;
    public string Country { get; private set; } = string.Empty;
    public string Timezone { get; private set; } = "UTC";
    public int AcademicYearStartMonth { get; private set; } = 1;
    public InstituteSettings Settings { get; private set; } = new();
    public Guid? SubscriptionId { get; private set; }

    private Institute() { }

    public static Institute Create(
        string name,
        string code,
        InstituteType type,
        ContactInfo contactInfo,
        Address address,
        string country,
        string timezone,
        int academicYearStartMonth,
        string? description = null,
        Guid? subscriptionId = null
    )
    {
        var institute = new Institute
        {
            Name = name,
            Code = code,
            Type = type,
            Status = InstituteStatus.Pending,
            ContactInfo = contactInfo,
            Address = address,
            Country = country,
            Timezone = timezone,
            AcademicYearStartMonth = academicYearStartMonth,
            Description = description,
            SubscriptionId = subscriptionId,
            Settings = new InstituteSettings()
        };

        institute.AddDomainEvent(
            new InstituteCreatedEvent
            {
                AggregateId = institute.Id,
                EventType = nameof(InstituteCreatedEvent),
                Name = name,
                Code = code,
                Type = type
            }
        );

        return institute;
    }

    public void Update(
        string name,
        string? description,
        ContactInfo contactInfo,
        Address address,
        string country,
        string timezone,
        int academicYearStartMonth
    )
    {
        Name = name;
        Description = description;
        ContactInfo = contactInfo;
        Address = address;
        Country = country;
        Timezone = timezone;
        AcademicYearStartMonth = academicYearStartMonth;
        MarkAsUpdated();

        AddDomainEvent(
            new InstituteUpdatedEvent
            {
                AggregateId = Id,
                EventType = nameof(InstituteUpdatedEvent),
                Name = name
            }
        );
    }

    public void UpdateSettings(InstituteSettings settings)
    {
        Settings = settings;
        MarkAsUpdated();

        AddDomainEvent(
            new InstituteUpdatedEvent
            {
                AggregateId = Id,
                EventType = nameof(InstituteUpdatedEvent),
                Name = Name
            }
        );
    }

    public void UploadLogo(string logoUrl)
    {
        LogoUrl = logoUrl;
        MarkAsUpdated();
    }

    public void Activate()
    {
        Status = InstituteStatus.Active;
        MarkAsUpdated();
    }

    public void Deactivate()
    {
        Status = InstituteStatus.Inactive;
        MarkAsUpdated();

        AddDomainEvent(
            new InstituteDeactivatedEvent
            {
                AggregateId = Id,
                EventType = nameof(InstituteDeactivatedEvent)
            }
        );
    }

    public void Suspend()
    {
        Status = InstituteStatus.Suspended;
        MarkAsUpdated();
    }
}
