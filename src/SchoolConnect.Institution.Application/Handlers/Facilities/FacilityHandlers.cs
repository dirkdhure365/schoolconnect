using MediatR;
using SchoolConnect.Institution.Application.Commands.Facilities;
using SchoolConnect.Institution.Application.Queries.Facilities;
using SchoolConnect.Institution.Domain.DTOs;
using SchoolConnect.Institution.Domain.Entities;
using SchoolConnect.Institution.Domain.Interfaces;

namespace SchoolConnect.Institution.Application.Handlers.Facilities;

// Command Handlers
public class CreateFacilityHandler : IRequestHandler<CreateFacilityCommand, FacilityDto>
{
    private readonly IFacilityRepository _repository;
    
    public CreateFacilityHandler(IFacilityRepository repository)
    {
        _repository = repository;
    }
    
    public async Task<FacilityDto> Handle(CreateFacilityCommand request, CancellationToken cancellationToken)
    {
        BookingRules? bookingRules = null;
        if (request.IsBookable && request.MinDurationMinutes.HasValue)
        {
            bookingRules = new BookingRules
            {
                MinDurationMinutes = request.MinDurationMinutes ?? 30,
                MaxDurationMinutes = request.MaxDurationMinutes ?? 480,
                AdvanceBookingDays = request.AdvanceBookingDays ?? 30,
                RequiresApproval = request.RequiresApproval ?? false,
                AllowedRoles = request.AllowedRoles ?? new List<string>()
            };
        }
        
        var facility = Facility.Create(
            request.CentreId,
            request.Name,
            request.Type,
            request.Capacity,
            request.IsBookable,
            request.Code,
            request.Description,
            request.Amenities,
            bookingRules
        );
        
        await _repository.AddAsync(facility);
        
        return MapToDto(facility);
    }
    
    private static FacilityDto MapToDto(Facility facility) => new(
        facility.Id,
        facility.CentreId,
        facility.Name,
        facility.Code,
        facility.Type,
        facility.Capacity,
        facility.Description,
        facility.Amenities,
        facility.ImageUrl,
        facility.Status,
        facility.IsBookable,
        facility.BookingRules != null ? new BookingRulesDto(
            facility.BookingRules.MinDurationMinutes,
            facility.BookingRules.MaxDurationMinutes,
            facility.BookingRules.AdvanceBookingDays,
            facility.BookingRules.RequiresApproval,
            facility.BookingRules.AllowedRoles
        ) : null,
        facility.CreatedAt,
        facility.UpdatedAt
    );
}

public class UpdateFacilityHandler : IRequestHandler<UpdateFacilityCommand, FacilityDto>
{
    private readonly IFacilityRepository _repository;
    
    public UpdateFacilityHandler(IFacilityRepository repository)
    {
        _repository = repository;
    }
    
    public async Task<FacilityDto> Handle(UpdateFacilityCommand request, CancellationToken cancellationToken)
    {
        var facility = await _repository.GetByIdAsync(request.Id);
        if (facility == null) throw new Exception($"Facility not found: {request.Id}");
        
        facility.Update(request.Name, request.Capacity, request.Description, request.Amenities);
        
        await _repository.UpdateAsync(facility);
        
        return MapToDto(facility);
    }
    
    private static FacilityDto MapToDto(Facility facility) => new(
        facility.Id,
        facility.CentreId,
        facility.Name,
        facility.Code,
        facility.Type,
        facility.Capacity,
        facility.Description,
        facility.Amenities,
        facility.ImageUrl,
        facility.Status,
        facility.IsBookable,
        facility.BookingRules != null ? new BookingRulesDto(
            facility.BookingRules.MinDurationMinutes,
            facility.BookingRules.MaxDurationMinutes,
            facility.BookingRules.AdvanceBookingDays,
            facility.BookingRules.RequiresApproval,
            facility.BookingRules.AllowedRoles
        ) : null,
        facility.CreatedAt,
        facility.UpdatedAt
    );
}

public class DeleteFacilityHandler : IRequestHandler<DeleteFacilityCommand, bool>
{
    private readonly IFacilityRepository _repository;
    
    public DeleteFacilityHandler(IFacilityRepository repository)
    {
        _repository = repository;
    }
    
    public async Task<bool> Handle(DeleteFacilityCommand request, CancellationToken cancellationToken)
    {
        var facility = await _repository.GetByIdAsync(request.Id);
        if (facility == null) return false;
        
        await _repository.DeleteAsync(request.Id);
        
        return true;
    }
}

public class BookFacilityHandler : IRequestHandler<BookFacilityCommand, FacilityBookingDto>
{
    private readonly IFacilityBookingRepository _bookingRepository;
    private readonly IFacilityRepository _facilityRepository;
    
    public BookFacilityHandler(IFacilityBookingRepository bookingRepository, IFacilityRepository facilityRepository)
    {
        _bookingRepository = bookingRepository;
        _facilityRepository = facilityRepository;
    }
    
    public async Task<FacilityBookingDto> Handle(BookFacilityCommand request, CancellationToken cancellationToken)
    {
        var facility = await _facilityRepository.GetByIdAsync(request.FacilityId);
        if (facility == null) throw new Exception($"Facility not found: {request.FacilityId}");
        
        // Check for conflicts
        var conflicts = await _bookingRepository.GetConflictingBookingsAsync(request.FacilityId, request.StartTime, request.EndTime);
        if (conflicts.Any())
        {
            throw new Exception("Facility is already booked for the requested time period");
        }
        
        var booking = FacilityBooking.Create(
            request.FacilityId,
            request.BookedBy,
            request.Purpose,
            request.StartTime,
            request.EndTime,
            request.Description,
            request.Notes
        );
        
        await _bookingRepository.AddAsync(booking);
        
        return MapToDto(booking);
    }
    
    private static FacilityBookingDto MapToDto(FacilityBooking booking) => new(
        booking.Id,
        booking.FacilityId,
        booking.BookedBy,
        booking.Purpose,
        booking.Description,
        booking.StartTime,
        booking.EndTime,
        booking.Status,
        booking.Notes,
        booking.ApprovedBy,
        booking.ApprovedAt,
        booking.CancellationReason,
        booking.CreatedAt
    );
}

public class UpdateBookingHandler : IRequestHandler<UpdateBookingCommand, FacilityBookingDto>
{
    private readonly IFacilityBookingRepository _repository;
    
    public UpdateBookingHandler(IFacilityBookingRepository repository)
    {
        _repository = repository;
    }
    
    public async Task<FacilityBookingDto> Handle(UpdateBookingCommand request, CancellationToken cancellationToken)
    {
        var booking = await _repository.GetByIdAsync(request.Id);
        if (booking == null) throw new Exception($"Booking not found: {request.Id}");
        
        booking.Update(request.StartTime, request.EndTime, request.Notes);
        
        await _repository.UpdateAsync(booking);
        
        return MapToDto(booking);
    }
    
    private static FacilityBookingDto MapToDto(FacilityBooking booking) => new(
        booking.Id,
        booking.FacilityId,
        booking.BookedBy,
        booking.Purpose,
        booking.Description,
        booking.StartTime,
        booking.EndTime,
        booking.Status,
        booking.Notes,
        booking.ApprovedBy,
        booking.ApprovedAt,
        booking.CancellationReason,
        booking.CreatedAt
    );
}

public class CancelBookingHandler : IRequestHandler<CancelBookingCommand, bool>
{
    private readonly IFacilityBookingRepository _repository;
    
    public CancelBookingHandler(IFacilityBookingRepository repository)
    {
        _repository = repository;
    }
    
    public async Task<bool> Handle(CancelBookingCommand request, CancellationToken cancellationToken)
    {
        var booking = await _repository.GetByIdAsync(request.Id);
        if (booking == null) return false;
        
        booking.Cancel(request.Reason);
        
        await _repository.UpdateAsync(booking);
        
        return true;
    }
}

public class ApproveBookingHandler : IRequestHandler<ApproveBookingCommand, FacilityBookingDto>
{
    private readonly IFacilityBookingRepository _repository;
    
    public ApproveBookingHandler(IFacilityBookingRepository repository)
    {
        _repository = repository;
    }
    
    public async Task<FacilityBookingDto> Handle(ApproveBookingCommand request, CancellationToken cancellationToken)
    {
        var booking = await _repository.GetByIdAsync(request.Id);
        if (booking == null) throw new Exception($"Booking not found: {request.Id}");
        
        booking.Approve(request.ApprovedBy);
        
        await _repository.UpdateAsync(booking);
        
        return MapToDto(booking);
    }
    
    private static FacilityBookingDto MapToDto(FacilityBooking booking) => new(
        booking.Id,
        booking.FacilityId,
        booking.BookedBy,
        booking.Purpose,
        booking.Description,
        booking.StartTime,
        booking.EndTime,
        booking.Status,
        booking.Notes,
        booking.ApprovedBy,
        booking.ApprovedAt,
        booking.CancellationReason,
        booking.CreatedAt
    );
}

// Query Handlers
public class GetFacilityByIdHandler : IRequestHandler<GetFacilityByIdQuery, FacilityDto?>
{
    private readonly IFacilityRepository _repository;
    
    public GetFacilityByIdHandler(IFacilityRepository repository)
    {
        _repository = repository;
    }
    
    public async Task<FacilityDto?> Handle(GetFacilityByIdQuery request, CancellationToken cancellationToken)
    {
        var facility = await _repository.GetByIdAsync(request.Id);
        if (facility == null) return null;
        
        return MapToDto(facility);
    }
    
    private static FacilityDto MapToDto(Facility facility) => new(
        facility.Id,
        facility.CentreId,
        facility.Name,
        facility.Code,
        facility.Type,
        facility.Capacity,
        facility.Description,
        facility.Amenities,
        facility.ImageUrl,
        facility.Status,
        facility.IsBookable,
        facility.BookingRules != null ? new BookingRulesDto(
            facility.BookingRules.MinDurationMinutes,
            facility.BookingRules.MaxDurationMinutes,
            facility.BookingRules.AdvanceBookingDays,
            facility.BookingRules.RequiresApproval,
            facility.BookingRules.AllowedRoles
        ) : null,
        facility.CreatedAt,
        facility.UpdatedAt
    );
}

public class GetFacilitiesByCentreHandler : IRequestHandler<GetFacilitiesByCentreQuery, IEnumerable<FacilityDto>>
{
    private readonly IFacilityRepository _repository;
    
    public GetFacilitiesByCentreHandler(IFacilityRepository repository)
    {
        _repository = repository;
    }
    
    public async Task<IEnumerable<FacilityDto>> Handle(GetFacilitiesByCentreQuery request, CancellationToken cancellationToken)
    {
        var facilities = await _repository.GetByCentreIdAsync(request.CentreId);
        return facilities.Select(MapToDto);
    }
    
    private static FacilityDto MapToDto(Facility facility) => new(
        facility.Id,
        facility.CentreId,
        facility.Name,
        facility.Code,
        facility.Type,
        facility.Capacity,
        facility.Description,
        facility.Amenities,
        facility.ImageUrl,
        facility.Status,
        facility.IsBookable,
        facility.BookingRules != null ? new BookingRulesDto(
            facility.BookingRules.MinDurationMinutes,
            facility.BookingRules.MaxDurationMinutes,
            facility.BookingRules.AdvanceBookingDays,
            facility.BookingRules.RequiresApproval,
            facility.BookingRules.AllowedRoles
        ) : null,
        facility.CreatedAt,
        facility.UpdatedAt
    );
}

public class GetFacilityBookingsHandler : IRequestHandler<GetFacilityBookingsQuery, IEnumerable<FacilityBookingDto>>
{
    private readonly IFacilityBookingRepository _repository;
    
    public GetFacilityBookingsHandler(IFacilityBookingRepository repository)
    {
        _repository = repository;
    }
    
    public async Task<IEnumerable<FacilityBookingDto>> Handle(GetFacilityBookingsQuery request, CancellationToken cancellationToken)
    {
        var bookings = await _repository.GetByFacilityIdAsync(request.FacilityId);
        return bookings.Select(MapToDto);
    }
    
    private static FacilityBookingDto MapToDto(FacilityBooking booking) => new(
        booking.Id,
        booking.FacilityId,
        booking.BookedBy,
        booking.Purpose,
        booking.Description,
        booking.StartTime,
        booking.EndTime,
        booking.Status,
        booking.Notes,
        booking.ApprovedBy,
        booking.ApprovedAt,
        booking.CancellationReason,
        booking.CreatedAt
    );
}

public class GetFacilityScheduleHandler : IRequestHandler<GetFacilityScheduleQuery, FacilityScheduleDto?>
{
    private readonly IFacilityRepository _facilityRepository;
    private readonly IFacilityBookingRepository _bookingRepository;
    
    public GetFacilityScheduleHandler(IFacilityRepository facilityRepository, IFacilityBookingRepository bookingRepository)
    {
        _facilityRepository = facilityRepository;
        _bookingRepository = bookingRepository;
    }
    
    public async Task<FacilityScheduleDto?> Handle(GetFacilityScheduleQuery request, CancellationToken cancellationToken)
    {
        var facility = await _facilityRepository.GetByIdAsync(request.FacilityId);
        if (facility == null) return null;
        
        var allBookings = await _bookingRepository.GetByFacilityIdAsync(request.FacilityId);
        var filteredBookings = allBookings
            .Where(b => b.StartTime >= request.StartDate && b.EndTime <= request.EndDate)
            .Select(MapToDto)
            .ToList();
        
        return new FacilityScheduleDto(
            facility.Id,
            facility.Name,
            filteredBookings
        );
    }
    
    private static FacilityBookingDto MapToDto(FacilityBooking booking) => new(
        booking.Id,
        booking.FacilityId,
        booking.BookedBy,
        booking.Purpose,
        booking.Description,
        booking.StartTime,
        booking.EndTime,
        booking.Status,
        booking.Notes,
        booking.ApprovedBy,
        booking.ApprovedAt,
        booking.CancellationReason,
        booking.CreatedAt
    );
}
