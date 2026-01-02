# SchoolConnect Calendar & Scheduling Microservice

The Calendar & Scheduling microservice handles events, timetables, bookings, reminders, and schedule management for all stakeholders in the SchoolConnect platform.

## Architecture

This microservice follows Clean Architecture principles with the following layers:

### Domain Layer (`SchoolConnect.Calendar.Domain`)
Contains the core business logic and domain models:
- **Entities**: CalendarEvent, EventAttendee, EventReminder, Timetable, TimetablePeriod, TimetableSlot, TimetableChange
- **Value Objects**: EventLocation, RecurrenceRule, TimeSlot, ReminderSettings, TimetableSettings
- **Domain Events**: Event and timetable-related events for event sourcing
- **Enums**: EventType, EventStatus, EventVisibility, RsvpStatus, ReminderChannel, etc.
- **Exceptions**: Custom domain exceptions
- **Interfaces**: Repository interfaces

### Application Layer (`SchoolConnect.Calendar.Application`)
Contains application business logic:
- **Commands**: CreateEvent, UpdateEvent, CancelEvent, CreateTimetable, CreateSlot, etc.
- **Queries**: GetEventById, GetEventsByDateRange, GetTimetableSlots, etc.
- **DTOs**: Data transfer objects for API responses
- **Mappers**: AutoMapper profiles
- **Services**: Conflict detection, reminder scheduling

### Infrastructure Layer (`SchoolConnect.Calendar.Infrastructure`)
Contains infrastructure implementations:
- **Persistence**: MongoDB-based data access with CalendarDbContext
- **Repositories**: Implementations of repository interfaces
- **Services**: ConflictDetectionService, ReminderSchedulerService
- **Extensions**: Dependency injection configuration

### API Layer (`SchoolConnect.Calendar.Api`)
RESTful API endpoints:
- **Event Endpoints**: CRUD operations for calendar events
- **Timetable Endpoints**: Timetable management
- **Slot Endpoints**: Timetable slot management

## Features

### Calendar Events
- Create and manage events (meetings, classes, exams, holidays, etc.)
- Support for recurring events with flexible recurrence rules
- Event visibility control (Public, Institute, Centre, Class, Private)
- RSVP functionality with attendee management
- Event reminders via multiple channels (Email, Push, SMS, In-App)
- Virtual meeting integration (Zoom, Teams, Meet)
- Attachments and custom fields

### Timetables
- Academic timetable creation and management
- Period and slot scheduling
- Teacher, class, and facility assignment
- Conflict detection (double-booking prevention)
- Timetable publishing workflow
- Substitution and change management
- Multiple timetable versions (Draft, Published, Archived)

### Scheduling
- Automated conflict detection
- Availability checking for teachers and facilities
- Bulk slot creation
- Schedule optimization

## API Endpoints

### Calendar Events
```
GET    /api/calendar/events/{id}           - Get event by ID
GET    /api/calendar/events/range          - Get events by date range
POST   /api/calendar/events                - Create new event
PUT    /api/calendar/events/{id}           - Update event
POST   /api/calendar/events/{id}/cancel    - Cancel event
DELETE /api/calendar/events/{id}           - Delete event
POST   /api/calendar/events/{id}/rsvp      - RSVP to event
```

### Timetables
```
GET    /api/timetables/{id}                          - Get timetable by ID
POST   /api/institutes/{instituteId}/timetables      - Create timetable
POST   /api/timetables/{id}/publish                  - Publish timetable
GET    /api/timetables/{timetableId}/slots           - Get timetable slots
POST   /api/timetables/{timetableId}/slots           - Create slot
```

## Technology Stack

- **.NET 10.0**: Latest .NET framework
- **MongoDB**: Document database for flexible schema
- **MediatR**: CQRS pattern implementation
- **AutoMapper**: Object-to-object mapping
- **FluentValidation**: Input validation
- **Swagger/OpenAPI**: API documentation

## Database Schema

The service uses MongoDB with the following collections:
- `events` - Calendar events
- `event_attendees` - Event attendees and RSVPs
- `event_reminders` - Event reminders
- `timetables` - Academic timetables
- `timetable_periods` - Timetable periods
- `timetable_slots` - Scheduled slots
- `timetable_changes` - Schedule changes and substitutions

## Configuration

### appsettings.json
```json
{
  "ConnectionStrings": {
    "MongoDB": "mongodb://localhost:27017"
  },
  "MongoDB": {
    "DatabaseName": "schoolconnect_calendar"
  }
}
```

## Getting Started

### Prerequisites
- .NET 10.0 SDK
- MongoDB 4.4 or higher

### Build and Run

```bash
# Build the solution
dotnet build SchoolConnect.Calendar.sln

# Run the API
dotnet run --project src/SchoolConnect.Calendar.Api

# The API will be available at https://localhost:5001
```

### Docker Support
```bash
docker-compose up calendar-api
```

## Integration

### Event Publishing
The service publishes integration events for:
- Timetable changes (for notifications)
- Event updates (for calendar synchronization)
- Reminder scheduling

### Event Consumption
The service may consume events from:
- Institution service (institute/centre updates)
- Enrolment service (class/student updates)
- Identity service (user updates)

## Domain Model

### CalendarEvent
Core entity representing any scheduled event with support for:
- Recurrence patterns
- Attendee management
- RSVP tracking
- Location (physical or virtual)
- Custom fields

### Timetable
Represents an academic schedule with:
- Configurable periods
- Working days
- Academic year/term tracking
- Publishing workflow

### TimetableSlot
Scheduled class sessions with:
- Teacher assignment
- Facility booking
- Class/cohort association
- Subject information

## Business Rules

1. **Conflict Prevention**: System prevents double-booking of teachers, facilities, and classes
2. **Past Event Protection**: Completed events cannot be modified
3. **RSVP Deadline**: RSVPs must be submitted before the deadline
4. **Timetable Publishing**: Only published timetables are visible to students/parents
5. **Substitution Tracking**: All schedule changes are logged with reasons

## Future Enhancements

- Calendar synchronization (iCal, Google Calendar, Outlook)
- Advanced recurrence patterns (nth weekday, custom intervals)
- Resource booking (equipment, venues)
- Automated timetable generation
- AI-powered conflict resolution
- Mobile push notification integration
- Parent-teacher meeting scheduling
- Exam timetable management

## License

Copyright © 2024 SchoolConnect. All rights reserved.
