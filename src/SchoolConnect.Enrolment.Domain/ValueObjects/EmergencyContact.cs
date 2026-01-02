using SchoolConnect.Common.Domain.Primitives;

namespace SchoolConnect.Enrolment.Domain.ValueObjects;

public class EmergencyContact : ValueObject
{
    public string Name { get; private set; }
    public string Relationship { get; private set; }
    public string PhoneNumber { get; private set; }
    public string? AlternatePhone { get; private set; }
    public string? Email { get; private set; }
    public int Priority { get; private set; }

    private EmergencyContact()
    {
        Name = string.Empty;
        Relationship = string.Empty;
        PhoneNumber = string.Empty;
    }

    public EmergencyContact(
        string name,
        string relationship,
        string phoneNumber,
        int priority,
        string? alternatePhone = null,
        string? email = null)
    {
        Name = name;
        Relationship = relationship;
        PhoneNumber = phoneNumber;
        AlternatePhone = alternatePhone;
        Email = email;
        Priority = priority;
    }

    protected override IEnumerable<object> GetEqualityComponents()
    {
        yield return Name;
        yield return Relationship;
        yield return PhoneNumber;
        yield return Priority;
        if (AlternatePhone != null) yield return AlternatePhone;
        if (Email != null) yield return Email;
    }
}
