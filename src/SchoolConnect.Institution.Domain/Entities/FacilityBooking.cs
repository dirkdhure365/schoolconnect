using SchoolConnect.Institution.Domain.Enums;
using SchoolConnect.Institution.Domain.Events;
using SchoolConnect.Institution.Domain.Primitives;

namespace SchoolConnect.Institution.Domain.Entities;

public class FacilityBooking : AggregateRoot
{
    public Guid FacilityId { get; private set; }
    public Guid BookedBy { get; private set; }
    public string Purpose { get; private set; } = string.Empty;
    public string? Description { get; private set; }
    public DateTime StartTime { get; private set; }
    public DateTime EndTime { get; private set; }
    public BookingStatus Status { get; private set; }
    public string? Notes { get; private set; }
    public Guid? ApprovedBy { get; private set; }
    public DateTime? ApprovedAt { get; private set; }
    public string? CancellationReason { get; private set; }
    
    private FacilityBooking() { }
    
    public static FacilityBooking Create(
        Guid facilityId,
        Guid bookedBy,
        string purpose,
        DateTime startTime,
        DateTime endTime,
        string? description = null,
        string? notes = null)
    {
        if (endTime <= startTime)
            throw new ArgumentException("End time must be after start time");
            
        var booking = new FacilityBooking
        {
            FacilityId = facilityId,
            BookedBy = bookedBy,
            Purpose = purpose,
            Description = description,
            StartTime = startTime,
            EndTime = endTime,
            Notes = notes,
            Status = BookingStatus.Pending
        };
        
        booking.AddDomainEvent(new FacilityBookedEvent
        {
            AggregateId = booking.Id,
            EventType = nameof(FacilityBookedEvent),
            FacilityId = facilityId,
            BookedBy = bookedBy,
            StartTime = startTime,
            EndTime = endTime
        });
        
        return booking;
    }
    
    public void Update(DateTime startTime, DateTime endTime, string? notes = null)
    {
        if (endTime <= startTime)
            throw new ArgumentException("End time must be after start time");
            
        StartTime = startTime;
        EndTime = endTime;
        if (notes != null) Notes = notes;
        MarkAsUpdated();
    }
    
    public void Approve(Guid approvedBy)
    {
        Status = BookingStatus.Confirmed;
        ApprovedBy = approvedBy;
        ApprovedAt = DateTime.UtcNow;
        MarkAsUpdated();
    }
    
    public void Cancel(string? reason = null)
    {
        Status = BookingStatus.Cancelled;
        CancellationReason = reason;
        MarkAsUpdated();
        
        AddDomainEvent(new FacilityBookingCancelledEvent
        {
            AggregateId = Id,
            EventType = nameof(FacilityBookingCancelledEvent),
            FacilityId = FacilityId,
            Reason = reason
        });
    }
    
    public void Complete()
    {
        Status = BookingStatus.Completed;
        MarkAsUpdated();
    }
}
