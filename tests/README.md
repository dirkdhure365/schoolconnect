# SchoolConnect Curriculum Tests

This directory contains comprehensive unit tests for the SchoolConnect Curriculum and Education System.

## Test Projects

### SchoolConnect.Curriculum.Tests.Common
Shared test utilities, builders, fixtures, and mocks used across all test projects.
- **Builders**: Test data builders for creating domain entities with fluent API
  - `SubjectBuilder` - Build subject entities with customizable properties
  - `TopicBuilder` - Build topic entities  
  - `EducationalPhaseBuilder` - Build phase entities
  - `AssessmentPolicyBuilder` - Build assessment policies

### SchoolConnect.Curriculum.Domain.Tests (75 tests)
Tests for domain entities and core business logic.
- **Coverage: 87.6%**
- Tests for:
  - CurriculumFrameworkBase (10 tests)
  - EducationalPhaseEntity (7 tests)
  - SubjectEntity (10 tests)
  - TopicEntity (10 tests)
  - GradeCurriculumEntity (5 tests)
  - AssessmentEntities (11 tests)
  - ResourceEntities (16 tests)
  - TermPlan and related entities (6 tests)

### SchoolConnect.Curriculum.Application.Tests (9 tests)
Tests for application services and business logic.
- **Coverage: 96.5%**
- Tests for:
  - CurriculumServiceFactory (9 tests)
    - Service registration and retrieval
    - Case-insensitive board code lookup
    - Board filtering by country
    - Error handling for invalid codes

### SchoolConnect.Curriculum.Caps.Tests (15 tests)
Tests specific to the South African CAPS curriculum framework.
- **Coverage: 100%** (Framework initialization)
- Tests for:
  - CapsFramework initialization
  - Principles verification
  - General aims verification
  - Metadata validation

### SchoolConnect.Curriculum.Zimsec.Tests (16 tests)
Tests specific to the Zimbabwe ZIMSEC curriculum framework.
- **Coverage: 100%** (Framework initialization)
- Tests for:
  - ZimsecFramework initialization
  - Principles verification
  - General aims verification
  - Metadata validation

## Running Tests

### Run All Tests
```bash
dotnet test SchoolConnect.Curriculum.sln
```

### Run Specific Test Project
```bash
dotnet test tests/SchoolConnect.Curriculum.Domain.Tests
dotnet test tests/SchoolConnect.Curriculum.Application.Tests
dotnet test tests/SchoolConnect.Curriculum.Caps.Tests
dotnet test tests/SchoolConnect.Curriculum.Zimsec.Tests
```

### Run Tests with Coverage
```bash
dotnet test SchoolConnect.Curriculum.sln --collect:"XPlat Code Coverage" --results-directory ./coverage
```

### Generate Coverage Report
```bash
# Install report generator (one-time)
dotnet tool install -g dotnet-reportgenerator-globaltool

# Generate HTML report
reportgenerator -reports:"coverage/**/coverage.cobertura.xml" -targetdir:"coveragereport" -reporttypes:"Html;TextSummary"

# View report
open coveragereport/index.html  # macOS
start coveragereport/index.html # Windows
xdg-open coveragereport/index.html # Linux
```

## Current Test Statistics

- **Total Tests: 115**
- **All Passing: ✅**
- **Overall Line Coverage: 50%**
- **Branch Coverage: 8.8%**
- **Method Coverage: 66.7%**

### Coverage by Assembly
| Assembly | Line Coverage | Status |
|----------|--------------|--------|
| SchoolConnect.Curriculum.Domain | 87.6% | ✅ Excellent |
| SchoolConnect.Curriculum.Application | 96.5% | ✅ Excellent |
| SchoolConnect.Curriculum.Caps (Framework) | 100% | ✅ Complete |
| SchoolConnect.Curriculum.Zimsec (Framework) | 100% | ✅ Complete |
| SchoolConnect.Curriculum.Caps (Service) | 0% | ⚠️ Needs tests |
| SchoolConnect.Curriculum.Zimsec (Service) | 0% | ⚠️ Needs tests |

## Test Patterns and Conventions

### Naming Conventions
- Test classes: `{ClassName}Tests` (e.g., `SubjectEntityTests`)
- Test methods: `{Method}_{Scenario}_{ExpectedResult}` (e.g., `Constructor_ShouldGenerateNewId`)

### Assertion Library
Tests use **FluentAssertions** for more readable assertions:
```csharp
result.Should().NotBeNull();
result.Should().Be(expectedValue);
collection.Should().ContainSingle();
```

### Mocking
Tests use **Moq** for creating mock dependencies:
```csharp
var mockService = new Mock<ICurriculumService>();
mockService.Setup(x => x.GetFrameworkAsync()).ReturnsAsync(framework);
```

### Test Data Builders
Use builder classes for creating test data:
```csharp
var subject = new SubjectBuilder()
    .WithName("Mathematics")
    .WithCode("MATH101")
    .WithGrades(10, 11, 12)
    .Build();
```

## Next Steps to Reach 90% Coverage

To achieve the target of 90% code coverage, the following areas need additional tests:

1. **CAPS Curriculum Service** (0% → 90%)
   - GetFrameworkAsync
   - GetPhasesAsync
   - GetSubjectsAsync
   - GetTopicsBySubjectAsync
   - GetAssessmentPolicyAsync
   - SearchContentAsync

2. **ZIMSEC Curriculum Service** (0% → 90%)
   - Similar coverage as CAPS service

3. **Edge Cases and Error Handling**
   - Null parameter handling
   - Invalid data scenarios
   - Boundary conditions

4. **Branch Coverage Improvement** (8.8% → 85%)
   - Test all conditional branches
   - Test exception paths
   - Test validation logic

5. **Integration Tests**
   - End-to-end workflow tests
   - Cross-service interaction tests
   - Repository integration tests

## Contributing

When adding new tests:
1. Follow existing naming conventions
2. Use FluentAssertions for assertions
3. Keep tests focused and single-purpose
4. Use builders for complex test data
5. Run tests locally before committing
6. Ensure new code maintains >80% coverage

## Dependencies

- **xUnit** 2.5.3 - Test framework
- **FluentAssertions** 6.12.0 - Fluent assertion library
- **Moq** 4.20.70 - Mocking framework
- **Bogus** 35.4.0 - Fake data generation
- **coverlet.collector** 6.0.0 - Code coverage collector
