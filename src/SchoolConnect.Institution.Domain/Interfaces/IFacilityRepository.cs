using SchoolConnect.Institution.Domain.Entities;

namespace SchoolConnect.Institution.Domain.Interfaces;

public interface IFacilityRepository
{
    Task<Facility?> GetByIdAsync(Guid id, CancellationToken cancellationToken = default);
    Task<List<Facility>> GetByCentreIdAsync(Guid centreId, CancellationToken cancellationToken = default);
    Task<Facility> AddAsync(Facility facility, CancellationToken cancellationToken = default);
    Task UpdateAsync(Facility facility, CancellationToken cancellationToken = default);
    Task DeleteAsync(Guid id, CancellationToken cancellationToken = default);
    Task<FacilityBooking?> GetBookingByIdAsync(Guid id, CancellationToken cancellationToken = default);
    Task<List<FacilityBooking>> GetBookingsByFacilityIdAsync(Guid facilityId, CancellationToken cancellationToken = default);
    Task<FacilityBooking> AddBookingAsync(FacilityBooking booking, CancellationToken cancellationToken = default);
    Task UpdateBookingAsync(FacilityBooking booking, CancellationToken cancellationToken = default);
    Task DeleteBookingAsync(Guid id, CancellationToken cancellationToken = default);
}
