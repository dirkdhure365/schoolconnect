using SchoolConnect.Common.Domain.Primitives;

namespace SchoolConnect.Communication.Domain.ValueObjects;

public class MessageAttachment : ValueObject
{
    public Guid Id { get; private set; }
    public string Name { get; private set; } = string.Empty;
    public string Url { get; private set; } = string.Empty;
    public string MimeType { get; private set; } = string.Empty;
    public long SizeBytes { get; private set; }
    public string? ThumbnailUrl { get; private set; }

    private MessageAttachment() { }

    public static MessageAttachment Create(
        string name,
        string url,
        string mimeType,
        long sizeBytes,
        string? thumbnailUrl = null)
    {
        return new MessageAttachment
        {
            Id = Guid.NewGuid(),
            Name = name,
            Url = url,
            MimeType = mimeType,
            SizeBytes = sizeBytes,
            ThumbnailUrl = thumbnailUrl
        };
    }

    protected override IEnumerable<object> GetEqualityComponents()
    {
        yield return Id;
        yield return Name;
        yield return Url;
        yield return MimeType;
        yield return SizeBytes;
        yield return ThumbnailUrl ?? string.Empty;
    }
}
