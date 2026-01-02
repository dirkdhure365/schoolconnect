using SchoolConnect.Calendar.Domain.Entities;

namespace SchoolConnect.Calendar.Domain.Interfaces;

public interface IEventRepository
{
    Task<CalendarEvent?> GetByIdAsync(Guid id, CancellationToken cancellationToken = default);
    Task<IEnumerable<CalendarEvent>> GetByInstituteIdAsync(Guid instituteId, CancellationToken cancellationToken = default);
    Task<IEnumerable<CalendarEvent>> GetByCentreIdAsync(Guid centreId, CancellationToken cancellationToken = default);
    Task<IEnumerable<CalendarEvent>> GetByDateRangeAsync(DateTime startDate, DateTime endDate, Guid? instituteId = null, CancellationToken cancellationToken = default);
    Task<IEnumerable<CalendarEvent>> GetUpcomingEventsAsync(Guid userId, int count = 10, CancellationToken cancellationToken = default);
    Task AddAsync(CalendarEvent calendarEvent, CancellationToken cancellationToken = default);
    Task UpdateAsync(CalendarEvent calendarEvent, CancellationToken cancellationToken = default);
    Task DeleteAsync(Guid id, CancellationToken cancellationToken = default);
}
