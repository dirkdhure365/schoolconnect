# SchoolConnect Curriculum System

A generic, extensible curriculum entity model that can be implemented by different education systems and examination boards. The model uses interfaces and abstract classes to allow for curriculum-specific implementations.

## Overview

This system provides a comprehensive framework for managing educational curricula across different examination boards (CAPS, ZIMSEC, Cambridge, etc.). It uses an interface-first design pattern that allows for maximum flexibility and extensibility.

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
