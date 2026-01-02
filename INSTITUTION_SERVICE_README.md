# SchoolConnect Institution Management Microservice

## Overview
The Institution Management microservice handles schools, centres, facilities, resources, staff, and teams within the SchoolConnect platform. It provides comprehensive APIs for managing educational institutions and their related entities.

## Architecture

This microservice follows Clean Architecture principles with the following layers:

### Domain Layer (`SchoolConnect.Institution.Domain`)
- **Entities**: 10 aggregate roots and entities
  - Institute, Centre, Facility, FacilityBooking
  - Resource, ResourceAllocation
  - StaffMember, StaffCentreAssignment
  - Team, TeamMember
- **Value Objects**: Address, ContactInfo, WorkingHours, GeoLocation
- **Enums**: 13 enumerations for various statuses and types
- **Domain Events**: 25 events tracking entity lifecycle changes
- **Exceptions**: 6 custom domain exceptions
- **Interfaces**: 6 repository interfaces

### Application Layer (`SchoolConnect.Institution.Application`)
- **DTOs**: 16 data transfer objects
- **Commands**: CQRS command handlers for write operations
- **Queries**: CQRS query handlers for read operations
- **Mappers**: AutoMapper profiles for entity-DTO mapping
- Uses MediatR for mediator pattern implementation

### Infrastructure Layer (`SchoolConnect.Institution.Infrastructure`)
- **Persistence**: MongoDB database context
- **Repositories**: 6 repository implementations
- **Extensions**: Dependency injection configuration

### API Layer (`SchoolConnect.Institution.Api`)
- **Endpoints**: RESTful API endpoints using Minimal API pattern
- **Configuration**: Swagger/OpenAPI documentation
- **Middleware**: CORS, authentication, error handling

## Database
- **Database Type**: MongoDB
- **Database Name**: SchoolConnectInstitution
- **Collections**: 10 collections (institutes, centres, facilities, facility_bookings, resources, resource_allocations, staff_members, staff_centre_assignments, teams, team_members)

## API Endpoints

### Institutes
- `GET /api/institutes` - List all institutes
- `POST /api/institutes` - Create a new institute
- `GET /api/institutes/{id}` - Get institute by ID
- `PUT /api/institutes/{id}` - Update institute
- `DELETE /api/institutes/{id}` - Deactivate institute
- `GET /api/institutes/{id}/settings` - Get institute settings
- `PUT /api/institutes/{id}/settings` - Update institute settings
- `POST /api/institutes/{id}/logo` - Upload institute logo
- `GET /api/institutes/{id}/dashboard` - Get institute dashboard

### Centres
- `GET /api/institutes/{instituteId}/centres` - List centres by institute
- `POST /api/institutes/{instituteId}/centres` - Create centre
- `GET /api/centres/{id}` - Get centre by ID
- `PUT /api/centres/{id}` - Update centre
- `DELETE /api/centres/{id}` - Deactivate centre
- `GET /api/centres/{id}/dashboard` - Get centre dashboard

### Facilities, Resources, Staff, and Teams
Additional endpoints for facilities, resources, staff, and teams are planned for future implementation.

## Configuration

### appsettings.json
```json
{
  "ConnectionStrings": {
    "MongoDB": "mongodb://localhost:27017"
  },
  "MongoDB": {
    "DatabaseName": "SchoolConnectInstitution"
  }
}
```

## Running the Service

### Prerequisites
- .NET 10.0 SDK
- MongoDB (local or cloud instance)

### Build
```bash
dotnet build src/SchoolConnect.Institution.Api/SchoolConnect.Institution.Api.csproj
```

### Run
```bash
dotnet run --project src/SchoolConnect.Institution.Api/SchoolConnect.Institution.Api.csproj
```

The API will start on `http://localhost:5000` by default.

### Swagger UI
Once running, access the Swagger UI at: `http://localhost:5000/swagger`

## Domain Models

### Institute
Represents an educational institution with:
- Basic information (name, code, type, description)
- Contact details and address
- Settings (language, timezone, academic year)
- Status management

### Centre
Represents a physical location/campus with:
- Institute association
- Address and geolocation
- Capacity and working hours
- Status management

### Facility
Represents bookable spaces like classrooms, labs, etc. with:
- Type and capacity
- Amenities and features
- Booking rules
- Status tracking

### Resource
Represents physical assets with:
- Type and condition tracking
- Acquisition details and value
- Allocation management
- Location tracking

### Staff Member
Represents institutional employees with:
- Employment details
- Qualifications and specializations
- Centre assignments
- Team memberships

### Team
Represents groups of staff members with:
- Purpose and type
- Leadership structure
- Member management

## Technology Stack
- **Framework**: ASP.NET Core 10.0
- **Database**: MongoDB 3.5.2
- **ORM/Driver**: MongoDB.Driver
- **Mapping**: AutoMapper 16.0.0
- **Mediator**: MediatR 12.4.0
- **Validation**: FluentValidation 12.1.1
- **API Documentation**: Swashbuckle.AspNetCore 10.1.0

## Development Patterns
- **Architecture**: Clean Architecture / Onion Architecture
- **Design Pattern**: CQRS (Command Query Responsibility Segregation)
- **Domain-Driven Design**: Aggregates, Entities, Value Objects, Domain Events
- **Repository Pattern**: Abstraction over data access
- **Dependency Injection**: Built-in .NET DI container

## Future Enhancements
1. Complete implementation of Facility, Resource, Staff, and Team command/query handlers
2. Add FluentValidation validators for all commands
3. Implement seed data service
4. Add comprehensive unit and integration tests
5. Implement authentication and authorization
6. Add caching layer for frequently accessed data
7. Implement event publishing to message bus
8. Add logging and monitoring

## License
Copyright © 2026 SchoolConnect
