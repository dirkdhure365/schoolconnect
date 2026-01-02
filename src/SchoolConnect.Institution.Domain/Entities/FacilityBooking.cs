using SchoolConnect.Common.Domain.Primitives;
using SchoolConnect.Institution.Domain.Enums;
using SchoolConnect.Institution.Domain.Events;

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
        var booking = new FacilityBooking
        {
            FacilityId = facilityId,
            BookedBy = bookedBy,
            Purpose = purpose,
            Description = description,
            StartTime = startTime,
            EndTime = endTime,
            Status = BookingStatus.Pending,
            Notes = notes
        };

        booking.AddDomainEvent(new FacilityBookedEvent
        {
            AggregateId = booking.Id,
            AggregateType = nameof(FacilityBooking),
            FacilityId = facilityId,
            BookedBy = bookedBy,
            StartTime = startTime,
            EndTime = endTime
        });

        return booking;
    }

    public void Update(
        string purpose,
        DateTime startTime,
        DateTime endTime,
        string? description = null,
        string? notes = null)
    {
        Purpose = purpose;
        Description = description;
        StartTime = startTime;
        EndTime = endTime;
        Notes = notes;
        UpdatedAt = DateTime.UtcNow;
    }

    public void Approve(Guid approvedBy)
    {
        Status = BookingStatus.Confirmed;
        ApprovedBy = approvedBy;
        ApprovedAt = DateTime.UtcNow;
        UpdatedAt = DateTime.UtcNow;
    }

    public void Cancel(string? reason = null)
    {
        Status = BookingStatus.Cancelled;
        CancellationReason = reason;
        UpdatedAt = DateTime.UtcNow;

        AddDomainEvent(new FacilityBookingCancelledEvent
        {
            AggregateId = Id,
            AggregateType = nameof(FacilityBooking),
            FacilityId = FacilityId,
            Reason = reason
        });
    }

    public void Complete()
    {
        Status = BookingStatus.Completed;
        UpdatedAt = DateTime.UtcNow;
    }

    protected override void When(DomainEvent @event)
    {
        // Event sourcing support - not implemented in this version
    }
}
