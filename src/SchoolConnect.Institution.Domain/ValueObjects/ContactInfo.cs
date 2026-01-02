using SchoolConnect.Common.Domain.Primitives;

namespace SchoolConnect.Institution.Domain.ValueObjects;

public class ContactInfo : ValueObject
{
    public string Email { get; private set; }
    public string Phone { get; private set; }
    public string? Website { get; private set; }

    private ContactInfo() 
    {
        Email = string.Empty;
        Phone = string.Empty;
    }

    public ContactInfo(string email, string phone, string? website = null)
    {
        Email = email;
        Phone = phone;
        Website = website;
    }

    protected override IEnumerable<object> GetEqualityComponents()
    {
        yield return Email;
        yield return Phone;
        if (Website != null) yield return Website;
    }
}
