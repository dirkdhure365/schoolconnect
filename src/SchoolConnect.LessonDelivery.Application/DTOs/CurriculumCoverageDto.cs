using SchoolConnect.LessonDelivery.Domain.Enums;

namespace SchoolConnect.LessonDelivery.Application.DTOs;

public class CurriculumCoverageDto
{
    public Guid Id { get; set; }
    public Guid ClassId { get; set; }
    public Guid SubjectId { get; set; }
    public Guid CurriculumTopicId { get; set; }
    public string TopicName { get; set; } = string.Empty;
    public string? TopicCode { get; set; }
    
    public decimal PlannedHours { get; set; }
    public decimal ActualHours { get; set; }
    public decimal RemainingHours { get; set; }
    public decimal ProgressPercentage { get; set; }
    
    public CoverageStatus Status { get; set; }
    public DateTime? StartedAt { get; set; }
    public DateTime? CompletedAt { get; set; }
}
