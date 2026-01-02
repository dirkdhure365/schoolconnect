using SchoolConnect.LessonDelivery.Domain.Enums;

namespace SchoolConnect.LessonDelivery.Application.DTOs;

public class HomeworkDto
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
    public string? Instructions { get; set; }
    
    public DateTime AssignedDate { get; set; }
    public DateTime DueDate { get; set; }
    public int? MaxMarks { get; set; }
    
    public HomeworkStatus Status { get; set; }
    public int SubmissionCount { get; set; }
    public int GradedCount { get; set; }
}
