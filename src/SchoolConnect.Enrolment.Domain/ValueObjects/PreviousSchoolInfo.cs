using SchoolConnect.Common.Domain.Primitives;

namespace SchoolConnect.Enrolment.Domain.ValueObjects;

public class PreviousSchoolInfo : ValueObject
{
    public string SchoolName { get; private set; }
    public string? Address { get; private set; }
    public string? LastGradeCompleted { get; private set; }
    public int? YearCompleted { get; private set; }
    public string? ReasonForLeaving { get; private set; }
    public string? ContactPerson { get; private set; }
    public string? ContactPhone { get; private set; }

    private PreviousSchoolInfo()
    {
        SchoolName = string.Empty;
    }

    public PreviousSchoolInfo(
        string schoolName,
        string? address = null,
        string? lastGradeCompleted = null,
        int? yearCompleted = null,
        string? reasonForLeaving = null,
        string? contactPerson = null,
        string? contactPhone = null)
    {
        SchoolName = schoolName;
        Address = address;
        LastGradeCompleted = lastGradeCompleted;
        YearCompleted = yearCompleted;
        ReasonForLeaving = reasonForLeaving;
        ContactPerson = contactPerson;
        ContactPhone = contactPhone;
    }

    protected override IEnumerable<object> GetEqualityComponents()
    {
        yield return SchoolName;
        if (Address != null) yield return Address;
        if (LastGradeCompleted != null) yield return LastGradeCompleted;
        if (YearCompleted.HasValue) yield return YearCompleted.Value;
        if (ReasonForLeaving != null) yield return ReasonForLeaving;
        if (ContactPerson != null) yield return ContactPerson;
        if (ContactPhone != null) yield return ContactPhone;
    }
}
