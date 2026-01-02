using MediatR;

namespace SchoolConnect.LessonDelivery.Application.Commands.Coverage;

public record MarkTopicCoveredCommand : IRequest<Unit>
{
    public Guid CoverageId { get; init; }
    public decimal HoursCovered { get; init; }
    public Guid LessonSessionId { get; init; }
}
