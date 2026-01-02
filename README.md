# SchoolConnect Curriculum System

A generic, extensible curriculum entity model that can be implemented by different education systems and examination boards. The model uses interfaces and abstract classes to allow for curriculum-specific implementations.

## Overview

This system provides a comprehensive framework for managing educational curricula across different examination boards (CAPS, ZIMSEC, Cambridge, etc.). It uses an interface-first design pattern that allows for maximum flexibility and extensibility.
# SchoolConnect Education System API

A comprehensive .NET 10 Minimal API for managing Southern African education systems using CQRS (Command Query Responsibility Segregation) and Event Sourcing patterns with MongoDB as the data store.

## Overview

This API implements a hierarchical curriculum model representing education systems across Southern African countries including Zimbabwe, South Africa, Botswana, and Zambia.

## Features

- **CQRS Pattern**: Separate command and query responsibilities using MediatR
- **Event Sourcing**: Store all state changes as immutable events in MongoDB
- **Clean Architecture**: Domain, Application, Infrastructure, and API layers
- **RESTful API**: Comprehensive endpoints for all education system entities
- **Swagger Documentation**: Interactive API documentation
- **Docker Support**: Containerized application with Docker Compose

## Architecture

### Project Structure

```
src/
├── SchoolConnect.Curriculum.Domain/         # Core domain models and abstractions
│   ├── Abstractions/                        # Interface definitions
│   │   ├── ICurriculumFramework.cs
│   │   ├── IEducationalPhase.cs
│   │   ├── ISubject.cs
│   │   ├── ITopic.cs
│   │   ├── IGradeCurriculum.cs
│   │   ├── IAssessment.cs
│   │   ├── IResources.cs
│   │   ├── IProject.cs
│   │   ├── IPracticalAssessment.cs
│   │   └── IGlossary.cs
│   ├── Entities/                            # Entity implementations
│   │   ├── CurriculumFrameworkBase.cs
│   │   ├── EducationalPhaseEntity.cs
│   │   ├── SubjectEntity.cs
│   │   ├── TopicEntity.cs
│   │   ├── GradeCurriculumEntity.cs
│   │   ├── AssessmentEntities.cs
│   │   └── ResourceEntities.cs
│   └── Enums/                               # Enumeration types
│       ├── SubjectType.cs
│       ├── SkillCategory.cs
│       ├── CognitiveLevel.cs
│       ├── DifficultyLevel.cs
│       ├── AssessmentType.cs
│       ├── SchoolTerm.cs
│       ├── ResourceCategory.cs
│       └── ComponentType.cs
├── SchoolConnect.Curriculum.Application/    # Application services layer
│   └── Services/
│       ├── ICurriculumService.cs
│       ├── IPracticalCurriculumService.cs
│       ├── ICurriculumServiceFactory.cs
│       └── CurriculumServiceFactory.cs
├── SchoolConnect.Curriculum.Caps/           # CAPS implementation (South Africa)
│   ├── CapsFramework.cs
│   ├── Services/
│   │   └── CapsCurriculumService.cs
│   └── Repositories/
│       └── ICapsRepository.cs
├── SchoolConnect.Curriculum.Zimsec/         # ZIMSEC implementation (Zimbabwe)
│   ├── ZimsecFramework.cs
│   ├── Services/
│   │   └── ZimsecCurriculumService.cs
│   └── Repositories/
│       └── IZimsecRepository.cs
└── SchoolConnect.Curriculum.Api/            # Web API
    ├── Endpoints/
    │   ├── CurriculumDiscoveryEndpoints.cs
    │   ├── CurriculumEndpoints.cs
    │   └── PracticalCurriculumEndpoints.cs
    └── Program.cs
```

## Key Design Principles

1. **Interface-First Design**: All core abstractions are interfaces for maximum flexibility
2. **Board-Specific Implementations**: Each examination board (CAPS, ZIMSEC, Cambridge, etc.) has its own service implementation
3. **Factory Pattern**: CurriculumServiceFactory creates the appropriate service based on board code
4. **Extensibility**: New boards can be added by implementing ICurriculumService
5. **Practical Subject Support**: IPracticalCurriculumService extends ICurriculumService for subjects with PAT (Practical Assessment Tasks)

## Core Interfaces

### ICurriculumFramework
Base framework interface for any curriculum with:
- Framework metadata (Id, Name, Code, Country, ExaminationBoard, Version, EffectiveDate)
- Collections of Phases, Subjects, Principles, GeneralAims
- Methods to get subjects by phase or grade

### IEducationalPhase
Educational phases interface with:
- Phase metadata and grade ranges
- Time allocations and grade curricula
- Helper method to check if a grade falls within the phase

### ISubject
Subject interface with:
- Subject metadata (Name, Code, Type, IsCompulsory)
- Applicable phases and grades
- Collections of Aims, Skills, Concepts, Topics, Resources
- Assessment policy
- Support for composite subjects (components)

### ITopic / ISubTopic
Hierarchical topic structure with:
- Topic metadata and content weighting
- SubTopics, ContentItems, LearningObjectives
- Topic progressions across grades
- Linked topics for cross-referencing

### IGradeCurriculum
Grade-specific curriculum with:
- Term plans with topics and assessments
- Time allocations
- Assessment requirements

### Assessment Interfaces
- **IAssessmentPolicy**: Subject assessment policies
- **IGradeAssessmentRequirements**: Grade-specific requirements
- **IAssessmentComponent**: Exam papers, PAT, coursework
- **IFormalAssessmentTask**: Term-based formal assessments
- **IProgrammeOfAssessment**: Annual assessment programme

## Supported Examination Boards

### CAPS (South Africa)
- **Framework**: Curriculum and Assessment Policy Statement
- **Board**: Department of Basic Education
- **Phases**: Foundation (R-3), Intermediate (4-6), Senior (7-9), FET (10-12)
- **Features**: 7-point achievement scale, PAT support for IT and CAT subjects
- **Service**: CapsCurriculumService (implements IPracticalCurriculumService)

### ZIMSEC (Zimbabwe)
- **Framework**: Zimbabwe School Examinations Council Curriculum
- **Board**: Zimbabwe School Examinations Council
- **Phases**: Primary, O-Level (Form 1-4), A-Level (Form 5-6)
- **Features**: O-Level (1-9 scale) and A-Level (A-E scale) grading
- **Service**: ZimsecCurriculumService (implements ICurriculumService)

## API Endpoints

### Curriculum Discovery
- `GET /api/curriculum/boards` - List all registered boards
- `GET /api/curriculum/boards/country/{country}` - Get boards by country

### Curriculum Operations (parameterized by board code)
- `GET /api/curriculum/{boardCode}/framework` - Get framework
- `GET /api/curriculum/{boardCode}/phases` - Get educational phases
- `GET /api/curriculum/{boardCode}/subjects` - Get subjects (optionally filter by phase or grade)
- `GET /api/curriculum/{boardCode}/subjects/{id}/topics` - Get topics for a subject
- `GET /api/curriculum/{boardCode}/subjects/{id}/grades/{grade}/curriculum` - Get grade curriculum
- `GET /api/curriculum/{boardCode}/subjects/{id}/assessment-policy` - Get assessment policy
- `GET /api/curriculum/{boardCode}/subjects/{id}/glossary` - Get subject glossary
- `GET /api/curriculum/{boardCode}/search/content` - Search content
- `GET /api/curriculum/{boardCode}/search/objectives` - Search learning objectives

### Practical Curriculum (for boards with PAT support)
- `GET /api/curriculum/{boardCode}/practical/subjects/{id}/grades/{grade}/pat` - Get practical assessment (PAT)
- `GET /api/curriculum/{boardCode}/practical/subjects/{id}/grades/{grade}/projects` - Get projects
- `GET /api/curriculum/{boardCode}/practical/subjects/{id}/grades/{grade}/programming-components` - Get programming components

## Enumeration Types

- **SubjectType**: Core, Elective, Vocational, Language, Practical, Composite, LifeSkills
- **SkillCategory**: CriticalThinking, ProblemSolving, Research, Communication, Collaboration, Creativity, etc.
- **CognitiveLevel**: Knowledge, Comprehension, Application, Analysis, Synthesis, Evaluation (Bloom's Taxonomy)
- **DifficultyLevel**: Easy, Moderate, Difficult, VeryDifficult
- **AssessmentType**: WrittenExamination, OralExamination, PracticalExamination, Project, Coursework, Portfolio, PAT, etc.
- **SchoolTerm**: Term1-4, Semester1-2, FullYear
- **ResourceCategory**: Textbook, Software, Hardware, Equipment, Online, Multimedia, Reference, etc.
- **ComponentType**: Component, Event, Method, Function, Procedure, Class, Object, Property, etc.

## Building the Solution

```bash
# Build the entire solution
dotnet build SchoolConnect.Curriculum.sln

# Run the API
cd src/SchoolConnect.Curriculum.Api
dotnet run
```

## Adding a New Curriculum Board

1. Create a new class library project (e.g., `SchoolConnect.Curriculum.Cambridge`)
2. Create a framework class inheriting from `CurriculumFrameworkBase`
3. Create a repository interface for data access
4. Create a service class implementing `ICurriculumService` (or `IPracticalCurriculumService` if PAT support is needed)
5. Register the service with the factory in `Program.cs`

Example:
```csharp
var factory = new CurriculumServiceFactory();
factory.RegisterService("CAMBRIDGE", 
    () => new CambridgeCurriculumService(repository),
    new BoardInfo
    {
        Code = "CAMBRIDGE",
        Name = "Cambridge International Curriculum",
        Country = "United Kingdom",
        ExaminationBoard = "Cambridge Assessment International Education",
        SupportsPracticalAssessments = true
    });
```

## Future Enhancements

- Repository implementations with database support
- Caching layer for improved performance
- Authentication and authorization
- Import/export functionality for curriculum data
- Analytics and reporting
- Integration with school management systems

## License

This is a demonstration project for the SchoolConnect system.
SchoolConnect.EducationSystem/
├── src/
│   ├── SchoolConnect.EducationSystem.Domain/       # Domain entities, events, aggregates
│   ├── SchoolConnect.EducationSystem.Application/  # Commands, queries, handlers, DTOs
│   ├── SchoolConnect.EducationSystem.Infrastructure/ # MongoDB persistence, event store
│   └── SchoolConnect.EducationSystem.Api/          # Minimal API endpoints
├── Dockerfile
├── docker-compose.yml
└── README.md
```

### Data Model

- **Country** - Southern African countries (Zimbabwe, South Africa, Botswana, Zambia)
- **Education System** - National education framework for each country
- **Assessment Board** - Examining bodies (ZIMSEC, CAPS/DBE, BEC, ECZ)
- **Education Phase** - Stages of education (Foundation, Intermediate, Senior, FET)
- **Program** - Qualification programs (O Level, A Level, NSC, BGCSE)
- **Subject** - Academic subjects offered in each program
- **Curriculum** - Detailed curriculum content for each subject

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
