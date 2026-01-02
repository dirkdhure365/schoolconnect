using SchoolConnect.Common.Domain.Primitives;

namespace SchoolConnect.LessonDelivery.Domain.ValueObjects;

public class ActivityMaterial : ValueObject
{
    public string Name { get; private set; } = string.Empty;
    public string? Description { get; private set; }
    public int Quantity { get; private set; }
    public bool IsRequired { get; private set; }

    private ActivityMaterial() { }

    public ActivityMaterial(string name, string? description, int quantity, bool isRequired)
    {
        Name = name;
        Description = description;
        Quantity = quantity;
        IsRequired = isRequired;
    }

    protected override IEnumerable<object> GetEqualityComponents()
    {
        yield return Name;
        yield return Description ?? string.Empty;
        yield return Quantity;
        yield return IsRequired;
    }
}
