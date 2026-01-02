using SchoolConnect.LessonDelivery.Domain.Enums;

namespace SchoolConnect.LessonDelivery.Application.DTOs;

public class LessonPlanSummaryDto
{
    public Guid Id { get; set; }
    public string Title { get; set; } = string.Empty;
    public string SubjectName { get; set; } = string.Empty;
    public string TeacherName { get; set; } = string.Empty;
    public int DurationMinutes { get; set; }
    public LessonPlanStatus Status { get; set; }
    public DateTime CreatedAt { get; set; }
}
