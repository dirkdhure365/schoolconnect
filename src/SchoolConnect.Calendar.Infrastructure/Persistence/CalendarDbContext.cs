using MongoDB.Driver;
using SchoolConnect.Calendar.Domain.Entities;

namespace SchoolConnect.Calendar.Infrastructure.Persistence;

public class CalendarDbContext
{
    private readonly IMongoDatabase _database;

    public CalendarDbContext(IMongoDatabase database)
    {
        _database = database;
    }

    public IMongoCollection<CalendarEvent> Events => _database.GetCollection<CalendarEvent>("events");
    public IMongoCollection<EventAttendee> EventAttendees => _database.GetCollection<EventAttendee>("event_attendees");
    public IMongoCollection<EventReminder> EventReminders => _database.GetCollection<EventReminder>("event_reminders");
    public IMongoCollection<Timetable> Timetables => _database.GetCollection<Timetable>("timetables");
    public IMongoCollection<TimetablePeriod> TimetablePeriods => _database.GetCollection<TimetablePeriod>("timetable_periods");
    public IMongoCollection<TimetableSlot> TimetableSlots => _database.GetCollection<TimetableSlot>("timetable_slots");
    public IMongoCollection<TimetableChange> TimetableChanges => _database.GetCollection<TimetableChange>("timetable_changes");
}
