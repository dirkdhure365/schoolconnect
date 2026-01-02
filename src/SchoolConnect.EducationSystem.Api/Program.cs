using MediatR;
using SchoolConnect.EducationSystem.Application.Commands;
using AppInterfaces = SchoolConnect.EducationSystem.Application.Interfaces;
using SchoolConnect.EducationSystem.Application.Queries;
using SchoolConnect.EducationSystem.Domain.Entities;
using SchoolConnect.EducationSystem.Infrastructure.EventStore;
using SchoolConnect.EducationSystem.Infrastructure.Persistence;
using SchoolConnect.EducationSystem.Infrastructure.Repositories;
using SchoolConnect.EducationSystem.Infrastructure.Seed;

var builder = WebApplication.CreateBuilder(args);

// Add configuration
var mongoConnectionString = builder.Configuration.GetValue<string>("MongoDB:ConnectionString") ?? "mongodb://localhost:27017";
var mongoDatabaseName = builder.Configuration.GetValue<string>("MongoDB:DatabaseName") ?? "EducationSystemDb";

// Register MongoDB context
builder.Services.AddSingleton<IMongoDbContext>(sp => new MongoDbContext(mongoConnectionString, mongoDatabaseName));

// Register Event Store
builder.Services.AddSingleton<AppInterfaces.IEventStore, MongoEventStore>();

// Register Repositories
builder.Services.AddSingleton<AppInterfaces.IRepository<Country>>(sp => 
    new CountryRepository(sp.GetRequiredService<IMongoDbContext>(), sp.GetRequiredService<AppInterfaces.IEventStore>()));

builder.Services.AddSingleton<AppInterfaces.IRepository<SchoolConnect.EducationSystem.Domain.Entities.EducationSystem>>(sp => 
    new EducationSystemRepositoryExtended(sp.GetRequiredService<IMongoDbContext>(), sp.GetRequiredService<AppInterfaces.IEventStore>()));
builder.Services.AddSingleton<AppInterfaces.IEducationSystemRepository>(sp => 
    new EducationSystemRepositoryExtended(sp.GetRequiredService<IMongoDbContext>(), sp.GetRequiredService<AppInterfaces.IEventStore>()));

builder.Services.AddSingleton<AppInterfaces.IRepository<AssessmentBoard>>(sp => 
    new AssessmentBoardRepositoryExtended(sp.GetRequiredService<IMongoDbContext>(), sp.GetRequiredService<AppInterfaces.IEventStore>()));
builder.Services.AddSingleton<AppInterfaces.IAssessmentBoardRepository>(sp => 
    new AssessmentBoardRepositoryExtended(sp.GetRequiredService<IMongoDbContext>(), sp.GetRequiredService<AppInterfaces.IEventStore>()));

builder.Services.AddSingleton<AppInterfaces.IRepository<EducationPhase>>(sp => 
    new EducationPhaseRepositoryExtended(sp.GetRequiredService<IMongoDbContext>(), sp.GetRequiredService<AppInterfaces.IEventStore>()));
builder.Services.AddSingleton<AppInterfaces.IEducationPhaseRepository>(sp => 
    new EducationPhaseRepositoryExtended(sp.GetRequiredService<IMongoDbContext>(), sp.GetRequiredService<AppInterfaces.IEventStore>()));

builder.Services.AddSingleton<AppInterfaces.IRepository<SchoolConnect.EducationSystem.Domain.Entities.Program>>(sp => 
    new ProgramRepositoryExtended(sp.GetRequiredService<IMongoDbContext>(), sp.GetRequiredService<AppInterfaces.IEventStore>()));
builder.Services.AddSingleton<AppInterfaces.IProgramRepository>(sp => 
    new ProgramRepositoryExtended(sp.GetRequiredService<IMongoDbContext>(), sp.GetRequiredService<AppInterfaces.IEventStore>()));

builder.Services.AddSingleton<AppInterfaces.IRepository<Subject>>(sp => 
    new SubjectRepositoryExtended(sp.GetRequiredService<IMongoDbContext>(), sp.GetRequiredService<AppInterfaces.IEventStore>()));
builder.Services.AddSingleton<AppInterfaces.ISubjectRepository>(sp => 
    new SubjectRepositoryExtended(sp.GetRequiredService<IMongoDbContext>(), sp.GetRequiredService<AppInterfaces.IEventStore>()));

builder.Services.AddSingleton<AppInterfaces.IRepository<Curriculum>>(sp => 
    new CurriculumRepositoryExtended(sp.GetRequiredService<IMongoDbContext>(), sp.GetRequiredService<AppInterfaces.IEventStore>()));
builder.Services.AddSingleton<AppInterfaces.ICurriculumRepository>(sp => 
    new CurriculumRepositoryExtended(sp.GetRequiredService<IMongoDbContext>(), sp.GetRequiredService<AppInterfaces.IEventStore>()));

// Register DataSeeder
builder.Services.AddSingleton<DataSeeder>();

// Add MediatR
builder.Services.AddMediatR(cfg => {
    cfg.RegisterServicesFromAssembly(typeof(SchoolConnect.EducationSystem.Application.Commands.CreateCountryCommand).Assembly);
    cfg.RegisterServicesFromAssembly(typeof(Program).Assembly);
});

// Add Swagger/OpenAPI
builder.Services.AddEndpointsApiExplorer();
builder.Services.AddSwaggerGen(c =>
{
    c.SwaggerDoc("v1", new() { 
        Title = "SchoolConnect Education System API", 
        Version = "v1",
        Description = "A comprehensive API for managing Southern African education systems using CQRS and Event Sourcing patterns"
    });
});

builder.Services.AddCors(options =>
{
    options.AddDefaultPolicy(policy =>
    {
        policy.AllowAnyOrigin()
              .AllowAnyMethod()
              .AllowAnyHeader();
    });
});

var app = builder.Build();

// Configure the HTTP request pipeline.
if (app.Environment.IsDevelopment())
{
    app.UseSwagger();
    app.UseSwaggerUI(c =>
    {
        c.SwaggerEndpoint("/swagger/v1/swagger.json", "SchoolConnect Education System API v1");
        c.RoutePrefix = string.Empty; // Set Swagger UI at the app's root
    });
}

app.UseCors();
app.UseHttpsRedirection();

var mediator = app.Services.GetRequiredService<IMediator>();

// Country Endpoints
app.MapGet("/api/countries", async () => 
    await mediator.Send(new GetAllCountriesQuery()))
    .WithName("GetAllCountries")
    .WithTags("Countries")
    .Produces(200);

app.MapGet("/api/countries/{id}", async (string id) => 
{
    var result = await mediator.Send(new GetCountryByIdQuery(id));
    return result != null ? Results.Ok(result) : Results.NotFound();
})
    .WithName("GetCountryById")
    .WithTags("Countries")
    .Produces(200)
    .Produces(404);

app.MapPost("/api/countries", async (SchoolConnect.EducationSystem.Application.DTOs.CreateCountryDto dto) => 
{
    var result = await mediator.Send(new CreateCountryCommand(dto.Name, dto.Code, dto.Region));
    return Results.Created($"/api/countries/{result.Id}", result);
})
    .WithName("CreateCountry")
    .WithTags("Countries")
    .Produces(201);

app.MapPut("/api/countries/{id}", async (string id, SchoolConnect.EducationSystem.Application.DTOs.UpdateCountryDto dto) => 
{
    var result = await mediator.Send(new UpdateCountryCommand(id, dto.Name, dto.Code, dto.Region));
    return Results.Ok(result);
})
    .WithName("UpdateCountry")
    .WithTags("Countries")
    .Produces(200);

app.MapDelete("/api/countries/{id}", async (string id) => 
{
    var result = await mediator.Send(new DeleteCountryCommand(id));
    return result ? Results.NoContent() : Results.NotFound();
})
    .WithName("DeleteCountry")
    .WithTags("Countries")
    .Produces(204)
    .Produces(404);

// Education System Endpoints
app.MapGet("/api/education-systems", async () => 
    await mediator.Send(new GetAllEducationSystemsQuery()))
    .WithName("GetAllEducationSystems")
    .WithTags("Education Systems")
    .Produces(200);

app.MapGet("/api/education-systems/{id}", async (string id) => 
{
    var result = await mediator.Send(new GetEducationSystemByIdQuery(id));
    return result != null ? Results.Ok(result) : Results.NotFound();
})
    .WithName("GetEducationSystemById")
    .WithTags("Education Systems")
    .Produces(200)
    .Produces(404);

app.MapGet("/api/countries/{countryId}/education-systems", async (string countryId) => 
    await mediator.Send(new GetEducationSystemsByCountryQuery(countryId)))
    .WithName("GetEducationSystemsByCountry")
    .WithTags("Education Systems")
    .Produces(200);

app.MapPost("/api/education-systems", async (SchoolConnect.EducationSystem.Application.DTOs.CreateEducationSystemDto dto) => 
{
    var result = await mediator.Send(new CreateEducationSystemCommand(dto.CountryId, dto.Name, dto.Description));
    return Results.Created($"/api/education-systems/{result.Id}", result);
})
    .WithName("CreateEducationSystem")
    .WithTags("Education Systems")
    .Produces(201);

app.MapPut("/api/education-systems/{id}", async (string id, SchoolConnect.EducationSystem.Application.DTOs.UpdateEducationSystemDto dto) => 
{
    var result = await mediator.Send(new UpdateEducationSystemCommand(id, dto.Name, dto.Description));
    return Results.Ok(result);
})
    .WithName("UpdateEducationSystem")
    .WithTags("Education Systems")
    .Produces(200);

app.MapDelete("/api/education-systems/{id}", async (string id) => 
{
    var result = await mediator.Send(new DeleteEducationSystemCommand(id));
    return result ? Results.NoContent() : Results.NotFound();
})
    .WithName("DeleteEducationSystem")
    .WithTags("Education Systems")
    .Produces(204)
    .Produces(404);

// Assessment Board Endpoints
app.MapGet("/api/assessment-boards", async () => 
    await mediator.Send(new GetAllAssessmentBoardsQuery()))
    .WithName("GetAllAssessmentBoards")
    .WithTags("Assessment Boards")
    .Produces(200);

app.MapGet("/api/assessment-boards/{id}", async (string id) => 
{
    var result = await mediator.Send(new GetAssessmentBoardByIdQuery(id));
    return result != null ? Results.Ok(result) : Results.NotFound();
})
    .WithName("GetAssessmentBoardById")
    .WithTags("Assessment Boards")
    .Produces(200)
    .Produces(404);

app.MapGet("/api/education-systems/{educationSystemId}/assessment-boards", async (string educationSystemId) => 
    await mediator.Send(new GetAssessmentBoardsByEducationSystemQuery(educationSystemId)))
    .WithName("GetAssessmentBoardsByEducationSystem")
    .WithTags("Assessment Boards")
    .Produces(200);

app.MapPost("/api/assessment-boards", async (SchoolConnect.EducationSystem.Application.DTOs.CreateAssessmentBoardDto dto) => 
{
    var result = await mediator.Send(new CreateAssessmentBoardCommand(dto.EducationSystemId, dto.Name, dto.Abbreviation, dto.Description));
    return Results.Created($"/api/assessment-boards/{result.Id}", result);
})
    .WithName("CreateAssessmentBoard")
    .WithTags("Assessment Boards")
    .Produces(201);

app.MapPut("/api/assessment-boards/{id}", async (string id, SchoolConnect.EducationSystem.Application.DTOs.UpdateAssessmentBoardDto dto) => 
{
    var result = await mediator.Send(new UpdateAssessmentBoardCommand(id, dto.Name, dto.Abbreviation, dto.Description));
    return Results.Ok(result);
})
    .WithName("UpdateAssessmentBoard")
    .WithTags("Assessment Boards")
    .Produces(200);

app.MapDelete("/api/assessment-boards/{id}", async (string id) => 
{
    var result = await mediator.Send(new DeleteAssessmentBoardCommand(id));
    return result ? Results.NoContent() : Results.NotFound();
})
    .WithName("DeleteAssessmentBoard")
    .WithTags("Assessment Boards")
    .Produces(204)
    .Produces(404);

// Education Phase Endpoints
app.MapGet("/api/education-phases", async () => 
    await mediator.Send(new GetAllEducationPhasesQuery()))
    .WithName("GetAllEducationPhases")
    .WithTags("Education Phases")
    .Produces(200);

app.MapGet("/api/education-phases/{id}", async (string id) => 
{
    var result = await mediator.Send(new GetEducationPhaseByIdQuery(id));
    return result != null ? Results.Ok(result) : Results.NotFound();
})
    .WithName("GetEducationPhaseById")
    .WithTags("Education Phases")
    .Produces(200)
    .Produces(404);

app.MapGet("/api/education-systems/{educationSystemId}/education-phases", async (string educationSystemId) => 
    await mediator.Send(new GetEducationPhasesByEducationSystemQuery(educationSystemId)))
    .WithName("GetEducationPhasesByEducationSystem")
    .WithTags("Education Phases")
    .Produces(200);

app.MapPost("/api/education-phases", async (SchoolConnect.EducationSystem.Application.DTOs.CreateEducationPhaseDto dto) => 
{
    var result = await mediator.Send(new CreateEducationPhaseCommand(dto.EducationSystemId, dto.Name, dto.Description, dto.StartAge, dto.EndAge));
    return Results.Created($"/api/education-phases/{result.Id}", result);
})
    .WithName("CreateEducationPhase")
    .WithTags("Education Phases")
    .Produces(201);

app.MapPut("/api/education-phases/{id}", async (string id, SchoolConnect.EducationSystem.Application.DTOs.UpdateEducationPhaseDto dto) => 
{
    var result = await mediator.Send(new UpdateEducationPhaseCommand(id, dto.Name, dto.Description, dto.StartAge, dto.EndAge));
    return Results.Ok(result);
})
    .WithName("UpdateEducationPhase")
    .WithTags("Education Phases")
    .Produces(200);

app.MapDelete("/api/education-phases/{id}", async (string id) => 
{
    var result = await mediator.Send(new DeleteEducationPhaseCommand(id));
    return result ? Results.NoContent() : Results.NotFound();
})
    .WithName("DeleteEducationPhase")
    .WithTags("Education Phases")
    .Produces(204)
    .Produces(404);

// Program Endpoints
app.MapGet("/api/programs", async () => 
    await mediator.Send(new GetAllProgramsQuery()))
    .WithName("GetAllPrograms")
    .WithTags("Programs")
    .Produces(200);

app.MapGet("/api/programs/{id}", async (string id) => 
{
    var result = await mediator.Send(new GetProgramByIdQuery(id));
    return result != null ? Results.Ok(result) : Results.NotFound();
})
    .WithName("GetProgramById")
    .WithTags("Programs")
    .Produces(200)
    .Produces(404);

app.MapGet("/api/assessment-boards/{assessmentBoardId}/programs", async (string assessmentBoardId) => 
    await mediator.Send(new GetProgramsByAssessmentBoardQuery(assessmentBoardId)))
    .WithName("GetProgramsByAssessmentBoard")
    .WithTags("Programs")
    .Produces(200);

app.MapPost("/api/programs", async (SchoolConnect.EducationSystem.Application.DTOs.CreateProgramDto dto) => 
{
    var result = await mediator.Send(new CreateProgramCommand(dto.AssessmentBoardId, dto.EducationPhaseId, dto.Name, dto.Description, dto.DurationYears));
    return Results.Created($"/api/programs/{result.Id}", result);
})
    .WithName("CreateProgram")
    .WithTags("Programs")
    .Produces(201);

app.MapPut("/api/programs/{id}", async (string id, SchoolConnect.EducationSystem.Application.DTOs.UpdateProgramDto dto) => 
{
    var result = await mediator.Send(new UpdateProgramCommand(id, dto.Name, dto.Description, dto.DurationYears));
    return Results.Ok(result);
})
    .WithName("UpdateProgram")
    .WithTags("Programs")
    .Produces(200);

app.MapDelete("/api/programs/{id}", async (string id) => 
{
    var result = await mediator.Send(new DeleteProgramCommand(id));
    return result ? Results.NoContent() : Results.NotFound();
})
    .WithName("DeleteProgram")
    .WithTags("Programs")
    .Produces(204)
    .Produces(404);

// Subject Endpoints
app.MapGet("/api/subjects", async () => 
    await mediator.Send(new GetAllSubjectsQuery()))
    .WithName("GetAllSubjects")
    .WithTags("Subjects")
    .Produces(200);

app.MapGet("/api/subjects/{id}", async (string id) => 
{
    var result = await mediator.Send(new GetSubjectByIdQuery(id));
    return result != null ? Results.Ok(result) : Results.NotFound();
})
    .WithName("GetSubjectById")
    .WithTags("Subjects")
    .Produces(200)
    .Produces(404);

app.MapGet("/api/programs/{programId}/subjects", async (string programId) => 
    await mediator.Send(new GetSubjectsByProgramQuery(programId)))
    .WithName("GetSubjectsByProgram")
    .WithTags("Subjects")
    .Produces(200);

app.MapPost("/api/subjects", async (SchoolConnect.EducationSystem.Application.DTOs.CreateSubjectDto dto) => 
{
    var result = await mediator.Send(new CreateSubjectCommand(dto.ProgramId, dto.Name, dto.Code, dto.Description, dto.IsCore));
    return Results.Created($"/api/subjects/{result.Id}", result);
})
    .WithName("CreateSubject")
    .WithTags("Subjects")
    .Produces(201);

app.MapPut("/api/subjects/{id}", async (string id, SchoolConnect.EducationSystem.Application.DTOs.UpdateSubjectDto dto) => 
{
    var result = await mediator.Send(new UpdateSubjectCommand(id, dto.Name, dto.Code, dto.Description, dto.IsCore));
    return Results.Ok(result);
})
    .WithName("UpdateSubject")
    .WithTags("Subjects")
    .Produces(200);

app.MapDelete("/api/subjects/{id}", async (string id) => 
{
    var result = await mediator.Send(new DeleteSubjectCommand(id));
    return result ? Results.NoContent() : Results.NotFound();
})
    .WithName("DeleteSubject")
    .WithTags("Subjects")
    .Produces(204)
    .Produces(404);

// Curriculum Endpoints
app.MapGet("/api/curricula", async () => 
    await mediator.Send(new GetAllCurriculaQuery()))
    .WithName("GetAllCurricula")
    .WithTags("Curricula")
    .Produces(200);

app.MapGet("/api/curricula/{id}", async (string id) => 
{
    var result = await mediator.Send(new GetCurriculumByIdQuery(id));
    return result != null ? Results.Ok(result) : Results.NotFound();
})
    .WithName("GetCurriculumById")
    .WithTags("Curricula")
    .Produces(200)
    .Produces(404);

app.MapGet("/api/subjects/{subjectId}/curricula", async (string subjectId) => 
    await mediator.Send(new GetCurriculaBySubjectQuery(subjectId)))
    .WithName("GetCurriculaBySubject")
    .WithTags("Curricula")
    .Produces(200);

app.MapPost("/api/curricula", async (SchoolConnect.EducationSystem.Application.DTOs.CreateCurriculumDto dto) => 
{
    var result = await mediator.Send(new CreateCurriculumCommand(dto.SubjectId, dto.Title, dto.Content, dto.LearningObjectives, dto.Assessment, dto.Year));
    return Results.Created($"/api/curricula/{result.Id}", result);
})
    .WithName("CreateCurriculum")
    .WithTags("Curricula")
    .Produces(201);

app.MapPut("/api/curricula/{id}", async (string id, SchoolConnect.EducationSystem.Application.DTOs.UpdateCurriculumDto dto) => 
{
    var result = await mediator.Send(new UpdateCurriculumCommand(id, dto.Title, dto.Content, dto.LearningObjectives, dto.Assessment, dto.Year));
    return Results.Ok(result);
})
    .WithName("UpdateCurriculum")
    .WithTags("Curricula")
    .Produces(200);

app.MapDelete("/api/curricula/{id}", async (string id) => 
{
    var result = await mediator.Send(new DeleteCurriculumCommand(id));
    return result ? Results.NoContent() : Results.NotFound();
})
    .WithName("DeleteCurriculum")
    .WithTags("Curricula")
    .Produces(204)
    .Produces(404);

// Event Store Endpoints
app.MapGet("/api/events", async () => 
    await mediator.Send(new GetAllEventsQuery()))
    .WithName("GetAllEvents")
    .WithTags("Event Store")
    .Produces(200);

app.MapGet("/api/events/{aggregateId}", async (string aggregateId) => 
    await mediator.Send(new GetEventsByAggregateQuery(aggregateId)))
    .WithName("GetEventsByAggregate")
    .WithTags("Event Store")
    .Produces(200);

// Seed Data Endpoint
app.MapPost("/api/seed", async () => 
{
    var result = await mediator.Send(new SeedDatabaseCommand());
    return result ? Results.Ok(new { Message = "Database seeded successfully" }) : Results.BadRequest(new { Message = "Failed to seed database" });
})
    .WithName("SeedDatabase")
    .WithTags("Seed Data")
    .Produces(200)
    .Produces(400);

app.Run();
