using SchoolConnect.LessonDelivery.Domain.Enums;

namespace SchoolConnect.LessonDelivery.Application.DTOs;

public class LessonPlanDto
{
    public Guid Id { get; set; }
    public Guid ClassId { get; set; }
    public string ClassName { get; set; } = string.Empty;
    public Guid SubjectId { get; set; }
    public string SubjectName { get; set; } = string.Empty;
    public Guid TeacherId { get; set; }
    public string TeacherName { get; set; } = string.Empty;
    
    public string Title { get; set; } = string.Empty;
    public string? Description { get; set; }
    public List<Guid> CurriculumTopicIds { get; set; } = [];
    public int DurationMinutes { get; set; }
    public int? GradeLevel { get; set; }
    public int? TermNumber { get; set; }
    public int? WeekNumber { get; set; }
    
    public LessonPlanStatus Status { get; set; }
    public bool IsShared { get; set; }
    
    public DateTime CreatedAt { get; set; }
    public DateTime UpdatedAt { get; set; }
}
