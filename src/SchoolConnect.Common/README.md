# SchoolConnect Common Libraries

Shared common libraries and foundational infrastructure for the SchoolConnect microservices platform. This includes base classes, CQRS infrastructure, Event Sourcing, MongoDB configuration, and Azure Service Bus integration.

## Overview

The SchoolConnect Common libraries provide a robust foundation for building microservices with:
- **Domain-Driven Design (DDD)** primitives
- **CQRS (Command Query Responsibility Segregation)** pattern support
- **Event Sourcing** with MongoDB
- **Azure Service Bus** integration for messaging
- **Clean Architecture** layers
- **API middleware** and utilities

## Projects

### SchoolConnect.Common.Domain

Core domain primitives and interfaces following DDD principles.

**Key Components:**
- `AggregateRoot` - Base class for aggregate roots with domain event support
- `Entity` - Base entity with Id, timestamps, and audit fields
- `ValueObject` - Base class for value objects with value-based equality
- `DomainEvent` - Base record for domain events
- `IRepository<T>` - Generic repository interface
- `IUnitOfWork` - Unit of work pattern interface
- `IDomainEventHandler<T>` - Domain event handler interface

**Example Usage:**
```csharp
public class Student : AggregateRoot
{
    public string Name { get; private set; }
    public Email Email { get; private set; } // ValueObject

    protected override void When(DomainEvent @event)
    {
        switch (@event)
        {
            case StudentEnrolled e:
                Name = e.StudentName;
                Email = e.Email;
                break;
        }
    }
}
```

### SchoolConnect.Common.Application

Application layer with CQRS support and cross-cutting concerns.

**Key Components:**
- **CQRS Interfaces:** `ICommand`, `IQuery`, `ICommandHandler`, `IQueryHandler`
- **Behaviors:** Logging, Validation, Transaction, Performance monitoring
- **Exceptions:** ApplicationException, NotFoundException, ValidationException, ConflictException, ForbiddenException
- **Models:** Result, PagedResult, PagedRequest

**Example Usage:**
```csharp
// Command
public record CreateStudentCommand(string Name, string Email) : ICommand<Guid>;

// Handler
public class CreateStudentCommandHandler : ICommandHandler<CreateStudentCommand, Guid>
{
    public async Task<Result<Guid>> Handle(CreateStudentCommand request, CancellationToken ct)
    {
        // Implementation
        return Result.Success(studentId);
    }
}

// Query
public record GetStudentQuery(Guid Id) : IQuery<StudentDto>;

// Handler
public class GetStudentQueryHandler : IQueryHandler<GetStudentQuery, StudentDto>
{
    public async Task<StudentDto> Handle(GetStudentQuery request, CancellationToken ct)
    {
        // Implementation
        return studentDto;
    }
}
```

### SchoolConnect.Common.Infrastructure

Infrastructure implementations for persistence, messaging, and event sourcing.

**Key Components:**
- **Event Store:** MongoDB-based event store for event sourcing
- **Messaging:** Azure Service Bus publisher/consumer
- **Persistence:** MongoDB repository base class with CRUD operations
- **Integration Events:** 11 predefined integration events for cross-service communication
- **Logging:** Correlation ID middleware and logging extensions

**Integration Events:**
1. `StudentEnrolledIntegrationEvent`
2. `StudentWithdrawnIntegrationEvent`
3. `LessonCompletedIntegrationEvent`
4. `AttendanceRecordedIntegrationEvent`
5. `AssessmentGradedIntegrationEvent`
6. `HomeworkAssignedIntegrationEvent`
7. `PaymentReceivedIntegrationEvent`
8. `PaymentOverdueIntegrationEvent`
9. `GoalAchievedIntegrationEvent`
10. `AnnouncementPublishedIntegrationEvent`
11. `TimetableChangedIntegrationEvent`

**Configuration Example:**
```csharp
// appsettings.json
{
  "MongoDB": {
    "ConnectionString": "mongodb://localhost:27017",
    "DatabaseName": "SchoolConnectDb"
  },
  "EventStore": {
    "ConnectionString": "mongodb://localhost:27017",
    "DatabaseName": "EventStoreDb",
    "CollectionName": "Events"
  },
  "ServiceBus": {
    "ConnectionString": "Endpoint=sb://...",
    "TopicName": "schoolconnect-events",
    "SubscriptionName": "my-service"
  }
}

// Program.cs
builder.Services.AddMongoDb(builder.Configuration);
builder.Services.AddEventStore(builder.Configuration);
builder.Services.AddServiceBusMessaging(builder.Configuration);
```

**Repository Usage:**
```csharp
public class StudentRepository : MongoRepository<Student>
{
    public StudentRepository(
        MongoDbContext context,
        ILogger<StudentRepository> logger,
        IEventStore eventStore,
        IMessagePublisher messagePublisher)
        : base(context, logger, eventStore, messagePublisher)
    {
    }

    // Add custom methods as needed
}
```

### SchoolConnect.Common.Api

API layer with middleware, filters, and extensions for ASP.NET Core.

**Key Components:**
- **Middleware:** Exception handling, request logging, correlation ID
- **Filters:** Model validation filter
- **Extensions:** Swagger configuration, service registration, endpoint helpers

**Usage Example:**
```csharp
// Program.cs
var builder = WebApplication.CreateBuilder(args);

// Add services
builder.Services.AddCommonApiServices();
builder.Services.AddSwaggerDocumentation("My API", "v1", "API Description");

var app = builder.Build();

// Use middleware
app.UseCommonMiddleware(); // Adds CorrelationId, RequestLogging, ExceptionHandling

// Use Swagger
app.UseSwaggerDocumentation();

app.Run();
```

## Package Dependencies

### Common.Domain
- MediatR 12.4.0

### Common.Application
- FluentValidation 11.9.0
- FluentValidation.DependencyInjectionExtensions 11.9.0
- MediatR 12.4.0
- Microsoft.Extensions.Logging.Abstractions 9.0.0

### Common.Infrastructure
- MongoDB.Driver 2.28.0
- Azure.Messaging.ServiceBus 7.17.0
- Microsoft.Extensions.Logging.Abstractions 9.0.0
- Polly 8.3.0

### Common.Api
- Swashbuckle.AspNetCore 6.9.0
- Microsoft.AspNetCore.OpenApi 9.0.0

## Getting Started

1. **Add references to your microservice projects:**
   ```xml
   <ItemGroup>
     <ProjectReference Include="..\..\SchoolConnect.Common.Domain\SchoolConnect.Common.Domain.csproj" />
     <ProjectReference Include="..\..\SchoolConnect.Common.Application\SchoolConnect.Common.Application.csproj" />
     <ProjectReference Include="..\..\SchoolConnect.Common.Infrastructure\SchoolConnect.Common.Infrastructure.csproj" />
     <ProjectReference Include="..\..\SchoolConnect.Common.Api\SchoolConnect.Common.Api.csproj" />
   </ItemGroup>
   ```

2. **Configure services in Program.cs:**
   ```csharp
   // Add MediatR with behaviors
   builder.Services.AddMediatR(cfg => {
       cfg.RegisterServicesFromAssembly(typeof(Program).Assembly);
       cfg.AddOpenBehavior(typeof(LoggingBehavior<,>));
       cfg.AddOpenBehavior(typeof(ValidationBehavior<,>));
       cfg.AddOpenBehavior(typeof(PerformanceBehavior<,>));
   });

   // Add FluentValidation
   builder.Services.AddValidatorsFromAssembly(typeof(Program).Assembly);

   // Add MongoDB
   builder.Services.AddMongoDb(builder.Configuration);

   // Add Event Store
   builder.Services.AddEventStore(builder.Configuration);

   // Add Service Bus
   builder.Services.AddServiceBusMessaging(builder.Configuration);
   ```

3. **Create your domain entities:**
   ```csharp
   public class MyAggregate : AggregateRoot
   {
       protected override void When(DomainEvent @event)
       {
           // Handle domain events
       }
   }
   ```

4. **Create commands and queries:**
   ```csharp
   public record MyCommand : ICommand<MyResult>;
   public record MyQuery : IQuery<MyDto>;
   ```

5. **Implement handlers:**
   ```csharp
   public class MyCommandHandler : ICommandHandler<MyCommand, MyResult> { }
   public class MyQueryHandler : IQueryHandler<MyQuery, MyDto> { }
   ```

## Architecture Patterns

### CQRS Pattern
Commands and queries are separated for better scalability and maintainability.

### Event Sourcing
All state changes are stored as immutable events in MongoDB, enabling:
- Complete audit trail
- Event replay capabilities
- Temporal queries
- Event-driven integrations

### Repository Pattern
Generic repository with MongoDB implementation provides:
- Standard CRUD operations
- Pagination support
- Automatic event storage
- Integration event publishing

### Middleware Pipeline
Request pipeline includes:
1. Correlation ID injection
2. Request logging
3. Exception handling
4. Response formatting

## Best Practices

1. **Always use domain events** for state changes in aggregates
2. **Validate commands** using FluentValidation validators
3. **Use Result pattern** for command handlers to avoid exceptions for business logic failures
4. **Implement IDisposable** when using Service Bus publisher/consumer
5. **Configure correlation IDs** for distributed tracing
6. **Use integration events** for cross-service communication
7. **Keep aggregates small** and focused on a single business concept

## Testing

The Common libraries are designed to be testable:
- Interfaces can be mocked
- Repositories can be substituted with in-memory implementations
- Behaviors can be tested independently
- Event sourcing allows replaying events for testing

## License

This is a demonstration project for the SchoolConnect system.
