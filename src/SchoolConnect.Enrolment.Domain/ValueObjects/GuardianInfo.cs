using SchoolConnect.Common.Domain.Primitives;

namespace SchoolConnect.Enrolment.Domain.ValueObjects;

public class GuardianInfo : ValueObject
{
    public string FirstName { get; private set; }
    public string LastName { get; private set; }
    public string Relationship { get; private set; }
    public string Email { get; private set; }
    public string PhoneNumber { get; private set; }
    public string? AlternatePhone { get; private set; }
    public string? Occupation { get; private set; }
    public string? Employer { get; private set; }
    public Address? Address { get; private set; }
    public string? IdNumber { get; private set; }

    private GuardianInfo()
    {
        FirstName = string.Empty;
        LastName = string.Empty;
        Relationship = string.Empty;
        Email = string.Empty;
        PhoneNumber = string.Empty;
    }

    public GuardianInfo(
        string firstName,
        string lastName,
        string relationship,
        string email,
        string phoneNumber,
        string? alternatePhone = null,
        string? occupation = null,
        string? employer = null,
        Address? address = null,
        string? idNumber = null)
    {
        FirstName = firstName;
        LastName = lastName;
        Relationship = relationship;
        Email = email;
        PhoneNumber = phoneNumber;
        AlternatePhone = alternatePhone;
        Occupation = occupation;
        Employer = employer;
        Address = address;
        IdNumber = idNumber;
    }

    protected override IEnumerable<object> GetEqualityComponents()
    {
        yield return FirstName;
        yield return LastName;
        yield return Relationship;
        yield return Email;
        yield return PhoneNumber;
        if (AlternatePhone != null) yield return AlternatePhone;
        if (Occupation != null) yield return Occupation;
        if (Employer != null) yield return Employer;
        if (Address != null) yield return Address;
        if (IdNumber != null) yield return IdNumber;
    }
}
