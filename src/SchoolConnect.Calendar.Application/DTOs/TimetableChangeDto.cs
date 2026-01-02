using SchoolConnect.Calendar.Domain.Enums;

namespace SchoolConnect.Calendar.Application.DTOs;

public class TimetableChangeDto
{
    public Guid Id { get; set; }
    public Guid TimetableSlotId { get; set; }
    public Guid TimetableId { get; set; }
    
    public DateTime OriginalDate { get; set; }
    public ChangeType ChangeType { get; set; }
    
    public Guid OriginalTeacherId { get; set; }
    public string OriginalTeacherName { get; set; } = string.Empty;
    public Guid? OriginalFacilityId { get; set; }
    public string? OriginalFacilityName { get; set; }
    
    public Guid? NewTeacherId { get; set; }
    public string? NewTeacherName { get; set; }
    public Guid? NewFacilityId { get; set; }
    public string? NewFacilityName { get; set; }
    public DateTime? NewDate { get; set; }
    
    public string Reason { get; set; } = string.Empty;
    public string? Notes { get; set; }
    
    public bool NotificationSent { get; set; }
    
    public Guid CreatedBy { get; set; }
    public string CreatedByName { get; set; } = string.Empty;
    public DateTime CreatedAt { get; set; }
}
