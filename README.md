# SchoolConnect Education System API

A comprehensive .NET 10 Minimal API for managing Southern African education systems using CQRS (Command Query Responsibility Segregation) and Event Sourcing patterns with MongoDB as the data store.

## Overview

This API implements a hierarchical curriculum model representing education systems across Southern African countries and international curricula including Zimbabwe (ZIMSEC), South Africa (CAPS/DBE, IEB), Botswana (BEC), Zambia (ECZ), and Cambridge International (CAIE).

## Features

- **CQRS Pattern**: Separate command and query responsibilities using MediatR
- **Event Sourcing**: Store all state changes as immutable events in MongoDB
- **Clean Architecture**: Domain, Application, Infrastructure, and API layers
- **RESTful API**: Comprehensive endpoints for all education system entities
- **Swagger Documentation**: Interactive API documentation
- **Docker Support**: Containerized application with Docker Compose
- **Board-Specific Curriculum Services**: Dedicated implementations for Cambridge, IEB, BEC, and ECZ examination boards

## Architecture

### Project Structure

```
SchoolConnect.EducationSystem/
├── src/
│   ├── SchoolConnect.EducationSystem.Domain/       # Domain entities, events, aggregates
│   ├── SchoolConnect.EducationSystem.Application/  # Commands, queries, handlers, DTOs
│   ├── SchoolConnect.EducationSystem.Infrastructure/ # MongoDB persistence, event store
│   ├── SchoolConnect.EducationSystem.Api/          # Minimal API endpoints
│   ├── SchoolConnect.Curriculum.Cambridge/         # Cambridge (CAIE) curriculum framework
│   ├── SchoolConnect.Curriculum.Ieb/               # IEB (South Africa) curriculum framework
│   ├── SchoolConnect.Curriculum.Bec/               # BEC (Botswana) curriculum framework
│   └── SchoolConnect.Curriculum.Ecz/               # ECZ (Zambia) curriculum framework
├── Dockerfile
├── docker-compose.yml
├── README.md
└── CURRICULUM_BOARDS_IMPLEMENTATION.md
```

### Data Model

- **Country** - Southern African countries and international regions
- **Education System** - National education framework for each country
- **Assessment Board** - Examining bodies (ZIMSEC, CAPS/DBE, IEB, BEC, ECZ, Cambridge CAIE)
- **Education Phase** - Stages of education (Foundation, Intermediate, Senior, FET)
- **Program** - Qualification programs (O Level, A Level, NSC, BGCSE, IGCSE, etc.)
- **Subject** - Academic subjects offered in each program
- **Curriculum** - Detailed curriculum content for each subject

### Curriculum Board Services

The system includes specialized curriculum services for:
- **Cambridge (CAIE)**: IGCSE, AS & A Level with practical assessment support
- **IEB**: Independent Examinations Board for South Africa with PAT support
- **BEC**: Botswana Examinations Council with JC and BGCSE programs
- **ECZ**: Examinations Council of Zambia with division-based grading

See [CURRICULUM_BOARDS_IMPLEMENTATION.md](CURRICULUM_BOARDS_IMPLEMENTATION.md) for detailed information.

## Getting Started

### Prerequisites

- [.NET 10 SDK](https://dotnet.microsoft.com/download/dotnet/10.0)
- [Docker](https://www.docker.com/get-started) (optional, for containerized deployment)
- [MongoDB](https://www.mongodb.com/try/download/community) (if running locally without Docker)

### Running with Docker Compose (Recommended)

1. Clone the repository:
   ```bash
   git clone https://github.com/dirkdhure365/schoolconnect.git
   cd schoolconnect
   ```

2. Start the services:
   ```bash
   docker-compose up -d
   ```

3. The API will be available at:
   - HTTP: http://localhost:5000
   - HTTPS: https://localhost:5001
   - Swagger UI: http://localhost:5000

4. MongoDB will be available at: mongodb://localhost:27017

### Running Locally

1. Ensure MongoDB is running locally on port 27017

2. Update the connection string in `appsettings.json` if needed:
   ```json
   "MongoDB": {
     "ConnectionString": "mongodb://localhost:27017",
     "DatabaseName": "EducationSystemDb"
   }
   ```

3. Restore dependencies and run:
   ```bash
   dotnet restore
   dotnet run --project src/SchoolConnect.EducationSystem.Api
   ```

4. Navigate to http://localhost:5000 to view Swagger UI

## API Endpoints

### Countries
- `GET /api/countries` - Get all countries
- `GET /api/countries/{id}` - Get country by ID
- `POST /api/countries` - Create a new country
- `PUT /api/countries/{id}` - Update a country
- `DELETE /api/countries/{id}` - Delete a country

### Education Systems
- `GET /api/education-systems` - Get all education systems
- `GET /api/education-systems/{id}` - Get education system by ID
- `GET /api/countries/{countryId}/education-systems` - Get education systems by country
- `POST /api/education-systems` - Create a new education system
- `PUT /api/education-systems/{id}` - Update an education system
- `DELETE /api/education-systems/{id}` - Delete an education system

### Assessment Boards
- `GET /api/assessment-boards` - Get all assessment boards
- `GET /api/assessment-boards/{id}` - Get assessment board by ID
- `GET /api/education-systems/{educationSystemId}/assessment-boards` - Get boards by education system
- `POST /api/assessment-boards` - Create a new assessment board
- `PUT /api/assessment-boards/{id}` - Update an assessment board
- `DELETE /api/assessment-boards/{id}` - Delete an assessment board

### Education Phases
- `GET /api/education-phases` - Get all education phases
- `GET /api/education-phases/{id}` - Get education phase by ID
- `GET /api/education-systems/{educationSystemId}/education-phases` - Get phases by education system
- `POST /api/education-phases` - Create a new education phase
- `PUT /api/education-phases/{id}` - Update an education phase
- `DELETE /api/education-phases/{id}` - Delete an education phase

### Programs
- `GET /api/programs` - Get all programs
- `GET /api/programs/{id}` - Get program by ID
- `GET /api/assessment-boards/{assessmentBoardId}/programs` - Get programs by assessment board
- `POST /api/programs` - Create a new program
- `PUT /api/programs/{id}` - Update a program
- `DELETE /api/programs/{id}` - Delete a program

### Subjects
- `GET /api/subjects` - Get all subjects
- `GET /api/subjects/{id}` - Get subject by ID
- `GET /api/programs/{programId}/subjects` - Get subjects by program
- `POST /api/subjects` - Create a new subject
- `PUT /api/subjects/{id}` - Update a subject
- `DELETE /api/subjects/{id}` - Delete a subject

### Curricula
- `GET /api/curricula` - Get all curricula
- `GET /api/curricula/{id}` - Get curriculum by ID
- `GET /api/subjects/{subjectId}/curricula` - Get curricula by subject
- `POST /api/curricula` - Create a new curriculum
- `PUT /api/curricula/{id}` - Update a curriculum
- `DELETE /api/curricula/{id}` - Delete a curriculum

### Event Store
- `GET /api/events` - Get all events
- `GET /api/events/{aggregateId}` - Get events by aggregate ID

### Seed Data
- `POST /api/seed` - Seed database with sample data for all four countries

## Seed Data

The API includes comprehensive sample data for:

### Zimbabwe (ZIMSEC)
- O Level Program (Forms 1-4): English, Mathematics, Shona, Ndebele, Physics, Chemistry, Biology, Geography, History, Commerce, Accounting, Agriculture, Computer Science
- A Level Program (Lower 6, Upper 6): Mathematics, Physics, Chemistry, Biology, Economics, Business Studies, Accounting, Geography, History, English Literature, Computer Science

### South Africa (CAPS/DBE)
- Foundation Phase (Grade R-3): Home Language, First Additional Language, Mathematics, Life Skills
- Intermediate Phase (Grade 4-6): Home Language, FAL, Mathematics, Natural Sciences & Technology, Social Sciences, Life Skills
- Senior Phase (Grade 7-9): Home Language, FAL, Mathematics, Natural Sciences, Social Sciences, Technology, EMS, Life Orientation, Creative Arts
- FET Phase/NSC (Grade 10-12): Home Language, FAL, Mathematics/Mathematical Literacy, Life Orientation, plus choice subjects

### Botswana (BEC)
- Junior Certificate (Forms 1-3)
- BGCSE (Forms 4-5): English, Setswana, Mathematics, Sciences, Geography, History, Commerce, Accounting, Computer Studies

### Zambia (ECZ)
- Junior Secondary Leaving Examination (Grade 8-9)
- School Certificate (Grade 10-12): English, Mathematics, Civic Education, Sciences, Geography, History, Commerce, Accounts, Computer Studies, Religious Education, Agricultural Science

## Technologies

- **.NET 10** - Latest .NET framework
- **MongoDB Driver** - Document database
- **MediatR** - CQRS implementation
- **Swagger/OpenAPI** - API documentation
- **Docker** - Containerization

## Configuration

Configuration is managed through `appsettings.json`:

```json
{
  "MongoDB": {
    "ConnectionString": "mongodb://localhost:27017",
    "DatabaseName": "EducationSystemDb"
  }
}
```

Environment variables can override these settings:
- `MongoDB__ConnectionString`
- `MongoDB__DatabaseName`

## Building

```bash
dotnet build
```

## Testing

Navigate to Swagger UI at http://localhost:5000 and use the interactive documentation to test endpoints.

1. First, seed the database:
   ```
   POST /api/seed
   ```

2. Then query the data:
   ```
   GET /api/countries
   GET /api/education-systems
   GET /api/assessment-boards
   GET /api/programs
   GET /api/subjects
   ```

## Event Sourcing

All state changes are captured as events and stored in MongoDB. You can:
- View all events: `GET /api/events`
- View events for a specific entity: `GET /api/events/{aggregateId}`

This enables:
- Complete audit trail
- Event replay capabilities
- Temporal queries
- Event-driven integrations

## License

This project is licensed under the MIT License.

## Contributing

Contributions are welcome! Please feel free to submit a Pull Request.
