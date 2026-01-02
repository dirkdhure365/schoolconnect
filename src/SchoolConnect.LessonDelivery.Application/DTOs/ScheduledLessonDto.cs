using SchoolConnect.LessonDelivery.Domain.Enums;

namespace SchoolConnect.LessonDelivery.Application.DTOs;

public class ScheduledLessonDto
{
    public Guid Id { get; set; }
    public Guid? LessonPlanId { get; set; }
    public Guid ClassId { get; set; }
    public string ClassName { get; set; } = string.Empty;
    public Guid SubjectId { get; set; }
    public string SubjectName { get; set; } = string.Empty;
    public Guid TeacherId { get; set; }
    public string TeacherName { get; set; } = string.Empty;
    public string? Title { get; set; }
    public DateTime ScheduledStart { get; set; }
    public DateTime ScheduledEnd { get; set; }
    public LessonStatus Status { get; set; }
    public string? FacilityName { get; set; }
}
