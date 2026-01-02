using SchoolConnect.LessonDelivery.Domain.Enums;

namespace SchoolConnect.LessonDelivery.Application.DTOs;

public class LessonSessionDto
{
    public Guid Id { get; set; }
    public Guid ScheduledLessonId { get; set; }
    public Guid ClassId { get; set; }
    public Guid TeacherId { get; set; }
    
    public DateTime ActualStart { get; set; }
    public DateTime? ActualEnd { get; set; }
    public int? ActualDurationMinutes { get; set; }
    
    public string? Notes { get; set; }
    public string? Reflection { get; set; }
    public string? AudioRecordingUrl { get; set; }
    
    public SessionStatus Status { get; set; }
    public int AttendeeCount { get; set; }
    public int AbsentCount { get; set; }
}
