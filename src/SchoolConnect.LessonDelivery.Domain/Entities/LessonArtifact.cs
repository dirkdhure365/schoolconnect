using SchoolConnect.Common.Domain.Primitives;
using SchoolConnect.LessonDelivery.Domain.Enums;

namespace SchoolConnect.LessonDelivery.Domain.Entities;

public class LessonArtifact : Entity
{
    public Guid LessonSessionId { get; private set; }
    public ArtifactType Type { get; private set; }
    public string Name { get; private set; } = string.Empty;
    public string Url { get; private set; } = string.Empty;
    public Guid? FileId { get; private set; }
    public string? Description { get; private set; }
    public long? FileSizeBytes { get; private set; }
    public string? MimeType { get; private set; }
    public DateTime CapturedAt { get; private set; }
    public Guid CapturedBy { get; private set; }

    private LessonArtifact() { }

    public static LessonArtifact Create(
        Guid lessonSessionId,
        ArtifactType type,
        string name,
        string url,
        Guid capturedBy,
        Guid? fileId = null,
        string? description = null,
        long? fileSizeBytes = null,
        string? mimeType = null)
    {
        return new LessonArtifact
        {
            LessonSessionId = lessonSessionId,
            Type = type,
            Name = name,
            Url = url,
            FileId = fileId,
            Description = description,
            FileSizeBytes = fileSizeBytes,
            MimeType = mimeType,
            CapturedAt = DateTime.UtcNow,
            CapturedBy = capturedBy
        };
    }
}
