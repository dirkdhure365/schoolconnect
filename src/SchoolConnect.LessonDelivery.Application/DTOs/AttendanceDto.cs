using SchoolConnect.LessonDelivery.Domain.Enums;

namespace SchoolConnect.LessonDelivery.Application.DTOs;

public class AttendanceDto
{
    public Guid Id { get; set; }
    public Guid LessonSessionId { get; set; }
    public Guid StudentId { get; set; }
    public string StudentName { get; set; } = string.Empty;
    public string StudentCode { get; set; } = string.Empty;
    
    public AttendanceStatus Status { get; set; }
    public DateTime? ArrivalTime { get; set; }
    public int? LateByMinutes { get; set; }
    public string? AbsenceReason { get; set; }
    public bool IsExcused { get; set; }
    public string? Notes { get; set; }
    
    public DateTime MarkedAt { get; set; }
}
