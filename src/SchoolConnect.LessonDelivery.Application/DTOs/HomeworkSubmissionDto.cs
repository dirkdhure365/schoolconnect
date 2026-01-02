using SchoolConnect.LessonDelivery.Domain.Enums;

namespace SchoolConnect.LessonDelivery.Application.DTOs;

public class HomeworkSubmissionDto
{
    public Guid Id { get; set; }
    public Guid HomeworkId { get; set; }
    public Guid StudentId { get; set; }
    public string StudentName { get; set; } = string.Empty;
    public string StudentCode { get; set; } = string.Empty;
    
    public DateTime SubmittedAt { get; set; }
    public bool IsLate { get; set; }
    public int AttemptNumber { get; set; }
    
    public SubmissionStatus Status { get; set; }
    public decimal? MarksObtained { get; set; }
    public decimal? Percentage { get; set; }
    public string? Feedback { get; set; }
    public DateTime? GradedAt { get; set; }
}
