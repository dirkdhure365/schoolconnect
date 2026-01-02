using SchoolConnect.Common.Domain.Primitives;

namespace SchoolConnect.Collaboration.Domain.ValueObjects;

public class BoardBackground : ValueObject
{
    public string Type { get; private set; } = string.Empty; // Color, Image, Gradient
    public string Value { get; private set; } = string.Empty; // Hex color, URL, or gradient definition
    public string? ThumbnailUrl { get; private set; }

    private BoardBackground() { }

    public static BoardBackground Create(string type, string value, string? thumbnailUrl = null)
    {
        return new BoardBackground
        {
            Type = type,
            Value = value,
            ThumbnailUrl = thumbnailUrl
        };
    }

    protected override IEnumerable<object> GetEqualityComponents()
    {
        yield return Type;
        yield return Value;
        if (ThumbnailUrl != null) yield return ThumbnailUrl;
    }
}
