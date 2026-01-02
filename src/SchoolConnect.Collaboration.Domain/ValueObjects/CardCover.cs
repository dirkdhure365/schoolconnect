using SchoolConnect.Common.Domain.Primitives;

namespace SchoolConnect.Collaboration.Domain.ValueObjects;

public class CardCover : ValueObject
{
    public string Type { get; private set; } = string.Empty; // Color, Image
    public string Value { get; private set; } = string.Empty;
    public string? Size { get; private set; } // Normal, Full

    private CardCover() { }

    public static CardCover Create(string type, string value, string? size = null)
    {
        return new CardCover
        {
            Type = type,
            Value = value,
            Size = size
        };
    }

    protected override IEnumerable<object> GetEqualityComponents()
    {
        yield return Type;
        yield return Value;
        if (Size != null) yield return Size;
    }
}
