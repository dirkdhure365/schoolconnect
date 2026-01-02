# SchoolConnect Institution Management Microservice

## Overview
The Institution Management microservice handles schools, centres, facilities, resources, staff, and teams for the SchoolConnect platform.

## Architecture
This microservice follows Clean Architecture principles with CQRS pattern:
- **Domain Layer**: Contains entities, value objects, enums, events, exceptions, and repository interfaces
- **Application Layer**: Contains commands, queries, handlers, and DTOs
- **Infrastructure Layer**: Contains database context, repository implementations, and seed services
- **API Layer**: Contains HTTP endpoints using minimal APIs

## Technology Stack
- .NET 10.0
- MongoDB for data persistence
- MediatR for CQRS implementation
- Swashbuckle for API documentation

## Domain Entities

### Institute
Represents an educational institution with settings, contact information, and organizational details.

### Centre
Represents a campus or branch of an institution with location, capacity, and working hours.

### Facility
Represents physical spaces like classrooms, labs, libraries, etc. with booking capabilities.

### FacilityBooking
Manages bookings for facilities with approval workflows.

### Resource
Tracks institutional resources like equipment, vehicles, instruments, etc.

### ResourceAllocation
Manages allocation of resources to staff or students.

### StaffMember
Represents employees with qualifications, specializations, and employment details.

### StaffCentreAssignment
Tracks staff assignments to different centres.

### Team
Groups staff members into departments, subjects, projects, committees, or clubs.

### TeamMember
Manages team membership and roles.

## API Endpoints

### Institutes
- `GET /api/institutes` - List all institutes
- `POST /api/institutes` - Create a new institute
- `GET /api/institutes/{id}` - Get institute by ID
- `PUT /api/institutes/{id}` - Update institute
- `GET /api/institutes/{id}/settings` - Get institute settings
- `PUT /api/institutes/{id}/settings` - Update institute settings
- `GET /api/institutes/{id}/dashboard` - Get institute dashboard

### Centres
- `GET /api/institutes/{instituteId}/centres` - List centres by institute
- `POST /api/institutes/{instituteId}/centres` - Create centre
- `GET /api/centres/{id}` - Get centre by ID
- `PUT /api/centres/{id}` - Update centre
- `GET /api/centres/{id}/dashboard` - Get centre dashboard

### Facilities
- `GET /api/centres/{centreId}/facilities` - List facilities by centre
- `POST /api/centres/{centreId}/facilities` - Create facility
- `GET /api/facilities/{id}` - Get facility by ID
- `PUT /api/facilities/{id}` - Update facility
- `GET /api/facilities/{id}/bookings` - Get facility bookings
- `POST /api/facilities/{id}/bookings` - Book facility
- `GET /api/facilities/{id}/schedule` - Get facility schedule

### Resources
- `GET /api/centres/{centreId}/resources` - List resources by centre
- `POST /api/centres/{centreId}/resources` - Create resource
- `GET /api/resources/{id}` - Get resource by ID
- `PUT /api/resources/{id}` - Update resource
- `POST /api/resources/{id}/allocate` - Allocate resource
- `GET /api/resources/{id}/allocations` - Get resource allocations

### Staff
- `GET /api/institutes/{instituteId}/staff` - List staff by institute
- `POST /api/institutes/{instituteId}/staff` - Onboard staff
- `GET /api/staff/{id}` - Get staff by ID
- `PUT /api/staff/{id}` - Update staff
- `GET /api/staff/{id}/centres` - Get staff centre assignments
- `POST /api/staff/{id}/centres` - Assign staff to centre
- `GET /api/staff/{id}/teams` - Get staff team memberships
- `GET /api/centres/{centreId}/staff` - List staff by centre

### Teams
- `GET /api/institutes/{instituteId}/teams` - List teams by institute
- `POST /api/institutes/{instituteId}/teams` - Create team
- `GET /api/teams/{id}` - Get team by ID
- `PUT /api/teams/{id}` - Update team
- `GET /api/teams/{id}/members` - Get team members
- `POST /api/teams/{id}/members` - Add team member

## Configuration

The service requires MongoDB configuration in `appsettings.json`:

```json
{
  "MongoDB": {
    "ConnectionString": "mongodb://localhost:27017",
    "DatabaseName": "InstitutionDb"
  }
}
```

## Running the Service

```bash
cd src/SchoolConnect.Institution.Api
dotnet run
```

The API will be available at `https://localhost:5001` with Swagger UI at the root.

## Seed Data

Use the seed endpoint to populate the database with sample data:

```bash
POST /api/seed
```

## Project Structure

```
src/SchoolConnect.Institution/
├── SchoolConnect.Institution.Domain/
│   ├── Entities/
│   ├── ValueObjects/
│   ├── Events/
│   ├── Enums/
│   ├── Exceptions/
│   ├── Interfaces/
│   └── Primitives/
├── SchoolConnect.Institution.Application/
│   ├── Commands/
│   ├── Queries/
│   ├── Handlers/
│   ├── DTOs/
│   └── Validators/
├── SchoolConnect.Institution.Infrastructure/
│   ├── Persistence/
│   ├── Repositories/
│   ├── Seed/
│   └── Extensions/
└── SchoolConnect.Institution.Api/
    └── Program.cs
```

## Implementation Status

### Completed
- ✅ Domain Layer (all entities, value objects, enums, events, exceptions, interfaces)
- ✅ Application Layer (all commands, queries, DTOs, sample handlers for Institutes)
- ✅ Infrastructure Layer (database context, all repositories, seed service)
- ✅ API Layer (all endpoints, service registration, Swagger configuration)
- ✅ Solution builds successfully

### Remaining Work
- Implement remaining command and query handlers (following the Institute handler pattern)
- Add validation using FluentValidation
- Add comprehensive unit and integration tests
- Add error handling middleware
- Add authentication and authorization
- Add logging and monitoring

## Notes

The current implementation provides:
1. Complete domain model with all entities and value objects
2. Full repository pattern implementation
3. CQRS structure with commands and queries
4. Sample handlers demonstrating the implementation pattern
5. Complete API endpoints
6. MongoDB integration

All remaining handlers follow the same pattern as the Institute handlers and can be implemented similarly. The solution compiles and runs successfully, providing a solid foundation for the Institution Management microservice.
