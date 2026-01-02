# SchoolConnect Calendar & Scheduling Microservice

A comprehensive calendar and scheduling microservice for the SchoolConnect platform, built using Clean Architecture principles and CQRS pattern.

## Overview

The Calendar & Scheduling microservice handles calendar events, timetables, bookings, reminders, and schedule management for educational institutions. It provides a robust API for managing events, timetable periods, slots, substitutions, and conflict detection.

## Architecture

This microservice follows Clean Architecture with four distinct layers:

### 1. Domain Layer (`SchoolConnect.Calendar.Domain`)
Contains the core business logic and rules:

- **Entities**: CalendarEvent, EventAttendee, EventReminder, Timetable, TimetablePeriod, TimetableSlot, TimetableChange
- **Value Objects**: RecurrenceRule, TimeSlot, EventLocation, ReminderSettings
- **Domain Events**: 17 event types for event sourcing
- **Enums**: EventType, EventStatus, EventVisibility, RsvpStatus, ReminderChannel, ReminderStatus, TimetableStatus, PeriodType, ChangeType, ConflictType
- **Exceptions**: Custom domain exceptions
- **Interfaces**: Repository contracts

### 2. Application Layer (`SchoolConnect.Calendar.Application`)
Contains application logic and use cases:

- **Commands**: CQRS commands for write operations
- **Queries**: CQRS queries for read operations
- **Handlers**: MediatR handlers for commands and queries
- **DTOs**: Data Transfer Objects for API communication
- **Mappers**: AutoMapper profiles
- **Services**: Application service interfaces

### 3. Infrastructure Layer (`SchoolConnect.Calendar.Infrastructure`)
Contains implementation details:

- **Repositories**: MongoDB repository implementations
- **Services**: ConflictDetectionService, ReminderSchedulerService
- **Extensions**: Dependency Injection setup

### 4. API Layer (`SchoolConnect.Calendar.Api`)
RESTful API endpoints:

- **EventEndpoints**: Calendar event management
- **TimetableEndpoints**: Timetable management
- **TimetableSlotEndpoints**: Slot and substitution management

## Features

### Calendar Events
- Create, update, cancel, and delete events
- Support for recurring events with flexible recurrence rules
- Event attendees and RSVP management
- Event reminders via multiple channels (InApp, Push, Email, SMS)
- Event visibility levels (Public, Institute, Centre, Class, Private)
- Attachments and custom fields support
- Date range queries and upcoming events

### Timetables
- Create and manage academic timetables
- Define periods with specific time slots
- Support for different period types (Lesson, Break, Lunch, Assembly, etc.)
- Publish and archive timetables
- Clone timetables for new terms

### Timetable Slots
- Assign classes, subjects, teachers, and facilities to time slots
- Bulk slot creation
- Slot updates and deletions
- Teacher, class, and facility-specific timetable views

### Substitutions & Changes
- Create teacher substitutions
- Track room changes
- Schedule rescheduling
- Notification management for changes

### Conflict Detection
- Detect teacher double-booking
- Detect facility double-booking
- Detect class conflicts
- Overlapping period detection

## Technology Stack

- **.NET 10.0**: Latest .NET framework
- **MongoDB**: NoSQL database for data persistence
- **MediatR**: CQRS implementation
- **AutoMapper**: Object-to-object mapping
- **Minimal APIs**: Lightweight HTTP API framework
- **Swagger/OpenAPI**: API documentation

## API Endpoints

### Calendar Events
```
GET    /api/calendar/events/{id}
GET    /api/calendar/events
GET    /api/calendar/events/range
GET    /api/calendar/events/upcoming
POST   /api/calendar/events
PUT    /api/calendar/events/{id}
POST   /api/calendar/events/{id}/cancel
DELETE /api/calendar/events/{id}
POST   /api/calendar/events/{id}/attendees
DELETE /api/calendar/events/{eventId}/attendees/{userId}
```

### Timetables
```
GET    /api/timetables/{id}
PUT    /api/timetables/{id}
POST   /api/timetables/{id}/publish
DELETE /api/timetables/{id}
GET    /api/timetables/{id}/slots
POST   /api/timetables/{id}/slots
GET    /api/institutes/{instituteId}/timetables
POST   /api/institutes/{instituteId}/timetables
```

### Timetable Views
```
GET    /api/timetables/views/teacher/{teacherId}
GET    /api/timetables/views/class/{classId}
```

### Timetable Slots
```
PUT    /api/timetable-slots/{id}
DELETE /api/timetable-slots/{id}
POST   /api/timetable-slots/{id}/substitutions
```

## Configuration

The service requires MongoDB configuration in `appsettings.json`:

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

## Building and Running

### Prerequisites
- .NET 10.0 SDK
- MongoDB instance

### Build
```bash
dotnet build SchoolConnect.Calendar.sln
```

### Run
```bash
cd src/SchoolConnect.Calendar.Api
dotnet run
```

The API will be available at `https://localhost:5001` (HTTPS) or `http://localhost:5000` (HTTP).

### Swagger UI
Access the interactive API documentation at: `https://localhost:5001/swagger`

## Domain Model Details

### CalendarEvent
Represents a calendar event with support for:
- Basic event information (title, description, location)
- Time management (start/end times, all-day events, timezones)
- Recurrence patterns
- Visibility and access control
- RSVP management
- Event status tracking

### Timetable
Represents an academic timetable with:
- Institute and centre association
- Academic year and term tracking
- Effective date ranges
- Configurable settings (working days, period durations)
- Publication workflow

### TimetableSlot
Links together:
- Period (when)
- Day of week
- Class and cohort
- Subject
- Teacher
- Facility (optional)

### TimetableChange
Tracks modifications to scheduled slots:
- Substitutions (teacher changes)
- Room changes
- Cancellations
- Rescheduling

## Integration Points

The Calendar microservice can integrate with:
- **Identity Service**: For user authentication and authorization
- **Institution Service**: For institute, centre, and facility information
- **Communication Service**: For sending event and timetable change notifications
- **Curriculum Service**: For subject information

## Future Enhancements

Potential improvements for future iterations:
- iCal/ICS export functionality
- Calendar synchronization with external systems (Google Calendar, Outlook)
- Advanced recurring event patterns
- Resource booking with approval workflows
- Time zone conversion support
- Mobile-specific optimizations
- Real-time updates via WebSockets/SignalR
- Analytics and reporting

## License

This microservice is part of the SchoolConnect platform.
