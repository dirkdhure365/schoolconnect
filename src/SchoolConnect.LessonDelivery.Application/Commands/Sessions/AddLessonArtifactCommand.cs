using MediatR;
using SchoolConnect.LessonDelivery.Domain.Enums;

namespace SchoolConnect.LessonDelivery.Application.Commands.Sessions;

public record AddLessonArtifactCommand : IRequest<Guid>
{
    public Guid LessonSessionId { get; init; }
    public ArtifactType Type { get; init; }
    public string Name { get; init; } = string.Empty;
    public string Url { get; init; } = string.Empty;
    public Guid CapturedBy { get; init; }
    public Guid? FileId { get; init; }
    public string? Description { get; init; }
}
