using MongoDB.Driver;
using SchoolConnect.Institution.Domain.Entities;
using SchoolConnect.Institution.Domain.Interfaces;
using SchoolConnect.Institution.Infrastructure.Persistence;

namespace SchoolConnect.Institution.Infrastructure.Repositories;

public class FacilityRepository : IFacilityRepository
{
    private readonly InstitutionDbContext _context;

    public FacilityRepository(InstitutionDbContext context)
    {
        _context = context;
    }

    public async Task<Facility?> GetByIdAsync(Guid id, CancellationToken cancellationToken = default)
    {
        return await _context.Facilities
            .Find(f => f.Id == id)
            .FirstOrDefaultAsync(cancellationToken);
    }

    public async Task<List<Facility>> GetByCentreIdAsync(Guid centreId, CancellationToken cancellationToken = default)
    {
        return await _context.Facilities
            .Find(f => f.CentreId == centreId)
            .ToListAsync(cancellationToken);
    }

    public async Task<Facility> AddAsync(Facility facility, CancellationToken cancellationToken = default)
    {
        await _context.Facilities.InsertOneAsync(facility, cancellationToken: cancellationToken);
        return facility;
    }

    public async Task UpdateAsync(Facility facility, CancellationToken cancellationToken = default)
    {
        await _context.Facilities.ReplaceOneAsync(
            f => f.Id == facility.Id,
            facility,
            cancellationToken: cancellationToken);
    }

    public async Task DeleteAsync(Guid id, CancellationToken cancellationToken = default)
    {
        await _context.Facilities.DeleteOneAsync(
            f => f.Id == id,
            cancellationToken);
    }

    public async Task<FacilityBooking?> GetBookingByIdAsync(Guid id, CancellationToken cancellationToken = default)
    {
        return await _context.FacilityBookings
            .Find(b => b.Id == id)
            .FirstOrDefaultAsync(cancellationToken);
    }

    public async Task<List<FacilityBooking>> GetBookingsByFacilityIdAsync(Guid facilityId, CancellationToken cancellationToken = default)
    {
        return await _context.FacilityBookings
            .Find(b => b.FacilityId == facilityId)
            .ToListAsync(cancellationToken);
    }

    public async Task<FacilityBooking> AddBookingAsync(FacilityBooking booking, CancellationToken cancellationToken = default)
    {
        await _context.FacilityBookings.InsertOneAsync(booking, cancellationToken: cancellationToken);
        return booking;
    }

    public async Task UpdateBookingAsync(FacilityBooking booking, CancellationToken cancellationToken = default)
    {
        await _context.FacilityBookings.ReplaceOneAsync(
            b => b.Id == booking.Id,
            booking,
            cancellationToken: cancellationToken);
    }

    public async Task DeleteBookingAsync(Guid id, CancellationToken cancellationToken = default)
    {
        await _context.FacilityBookings.DeleteOneAsync(
            b => b.Id == id,
            cancellationToken);
    }
}
