using MediatR;
using SchoolConnect.Institution.Application.Commands.Institutes;
using SchoolConnect.Institution.Application.Commands.Centres;
using SchoolConnect.Institution.Application.Commands.Facilities;
using SchoolConnect.Institution.Application.Commands.Resources;
using SchoolConnect.Institution.Application.Commands.Staff;
using SchoolConnect.Institution.Application.Commands.Teams;
using SchoolConnect.Institution.Application.Queries.Institutes;
using SchoolConnect.Institution.Application.Queries.Centres;
using SchoolConnect.Institution.Application.Queries.Facilities;
using SchoolConnect.Institution.Application.Queries.Resources;
using SchoolConnect.Institution.Application.Queries.Staff;
using SchoolConnect.Institution.Application.Queries.Teams;
using SchoolConnect.Institution.Infrastructure.Extensions;
using SchoolConnect.Institution.Infrastructure.Seed;

var builder = WebApplication.CreateBuilder(args);

// Add configuration
var mongoConnectionString = builder.Configuration.GetValue<string>("MongoDB:ConnectionString") ?? "mongodb://localhost:27017";
var mongoDatabaseName = builder.Configuration.GetValue<string>("MongoDB:DatabaseName") ?? "InstitutionDb";

// Add Infrastructure services
builder.Services.AddInstitutionInfrastructure(mongoConnectionString, mongoDatabaseName);

// Add MediatR
builder.Services.AddMediatR(cfg => {
    cfg.RegisterServicesFromAssembly(typeof(CreateInstituteCommand).Assembly);
});

// Add Swagger/OpenAPI
builder.Services.AddEndpointsApiExplorer();
builder.Services.AddSwaggerGen(c =>
{
    c.SwaggerDoc("v1", new() { 
        Title = "SchoolConnect Institution Management API", 
        Version = "v1",
        Description = "API for managing institutions, centres, facilities, resources, staff, and teams"
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
        c.SwaggerEndpoint("/swagger/v1/swagger.json", "SchoolConnect Institution Management API v1");
        c.RoutePrefix = string.Empty;
    });
}

app.UseCors();
app.UseHttpsRedirection();

var mediator = app.Services.GetRequiredService<IMediator>();

// Institute Endpoints
app.MapGet("/api/institutes", async () => 
    await mediator.Send(new GetInstitutesQuery()))
    .WithName("GetAllInstitutes")
    .WithTags("Institutes")
    .Produces(200);

app.MapGet("/api/institutes/{id}", async (Guid id) => 
{
    var result = await mediator.Send(new GetInstituteByIdQuery(id));
    return result != null ? Results.Ok(result) : Results.NotFound();
})
    .WithName("GetInstituteById")
    .WithTags("Institutes")
    .Produces(200)
    .Produces(404);

app.MapPost("/api/institutes", async (CreateInstituteCommand command) => 
{
    var result = await mediator.Send(command);
    return Results.Created($"/api/institutes/{result.Id}", result);
})
    .WithName("CreateInstitute")
    .WithTags("Institutes")
    .Produces(201);

app.MapPut("/api/institutes/{id}", async (Guid id, UpdateInstituteCommand command) => 
{
    if (id != command.Id) return Results.BadRequest();
    var result = await mediator.Send(command);
    return Results.Ok(result);
})
    .WithName("UpdateInstitute")
    .WithTags("Institutes")
    .Produces(200)
    .Produces(400);

// Centre Endpoints
app.MapGet("/api/institutes/{instituteId}/centres", async (Guid instituteId) => 
    await mediator.Send(new GetCentresByInstituteQuery(instituteId)))
    .WithName("GetCentresByInstitute")
    .WithTags("Centres")
    .Produces(200);

app.MapGet("/api/centres/{id}", async (Guid id) => 
{
    var result = await mediator.Send(new GetCentreByIdQuery(id));
    return result != null ? Results.Ok(result) : Results.NotFound();
})
    .WithName("GetCentreById")
    .WithTags("Centres")
    .Produces(200)
    .Produces(404);

app.MapPost("/api/institutes/{instituteId}/centres", async (Guid instituteId, CreateCentreCommand command) => 
{
    if (instituteId != command.InstituteId) return Results.BadRequest();
    var result = await mediator.Send(command);
    return Results.Created($"/api/centres/{result.Id}", result);
})
    .WithName("CreateCentre")
    .WithTags("Centres")
    .Produces(201)
    .Produces(400);

// Facility Endpoints
app.MapGet("/api/centres/{centreId}/facilities", async (Guid centreId) => 
    await mediator.Send(new GetFacilitiesByCentreQuery(centreId)))
    .WithName("GetFacilitiesByCentre")
    .WithTags("Facilities")
    .Produces(200);

app.MapGet("/api/facilities/{id}", async (Guid id) => 
{
    var result = await mediator.Send(new GetFacilityByIdQuery(id));
    return result != null ? Results.Ok(result) : Results.NotFound();
})
    .WithName("GetFacilityById")
    .WithTags("Facilities")
    .Produces(200)
    .Produces(404);

app.MapPost("/api/centres/{centreId}/facilities", async (Guid centreId, CreateFacilityCommand command) => 
{
    if (centreId != command.CentreId) return Results.BadRequest();
    var result = await mediator.Send(command);
    return Results.Created($"/api/facilities/{result.Id}", result);
})
    .WithName("CreateFacility")
    .WithTags("Facilities")
    .Produces(201)
    .Produces(400);

// Resource Endpoints
app.MapGet("/api/centres/{centreId}/resources", async (Guid centreId) => 
    await mediator.Send(new GetResourcesByCentreQuery(centreId)))
    .WithName("GetResourcesByCentre")
    .WithTags("Resources")
    .Produces(200);

app.MapGet("/api/resources/{id}", async (Guid id) => 
{
    var result = await mediator.Send(new GetResourceByIdQuery(id));
    return result != null ? Results.Ok(result) : Results.NotFound();
})
    .WithName("GetResourceById")
    .WithTags("Resources")
    .Produces(200)
    .Produces(404);

app.MapPost("/api/centres/{centreId}/resources", async (Guid centreId, CreateResourceCommand command) => 
{
    if (centreId != command.CentreId) return Results.BadRequest();
    var result = await mediator.Send(command);
    return Results.Created($"/api/resources/{result.Id}", result);
})
    .WithName("CreateResource")
    .WithTags("Resources")
    .Produces(201)
    .Produces(400);

// Staff Endpoints
app.MapGet("/api/institutes/{instituteId}/staff", async (Guid instituteId) => 
    await mediator.Send(new GetStaffByInstituteQuery(instituteId)))
    .WithName("GetStaffByInstitute")
    .WithTags("Staff")
    .Produces(200);

app.MapGet("/api/staff/{id}", async (Guid id) => 
{
    var result = await mediator.Send(new GetStaffByIdQuery(id));
    return result != null ? Results.Ok(result) : Results.NotFound();
})
    .WithName("GetStaffById")
    .WithTags("Staff")
    .Produces(200)
    .Produces(404);

app.MapPost("/api/institutes/{instituteId}/staff", async (Guid instituteId, OnboardStaffCommand command) => 
{
    if (instituteId != command.InstituteId) return Results.BadRequest();
    var result = await mediator.Send(command);
    return Results.Created($"/api/staff/{result.Id}", result);
})
    .WithName("OnboardStaff")
    .WithTags("Staff")
    .Produces(201)
    .Produces(400);

// Team Endpoints
app.MapGet("/api/institutes/{instituteId}/teams", async (Guid instituteId) => 
    await mediator.Send(new GetTeamsByInstituteQuery(instituteId)))
    .WithName("GetTeamsByInstitute")
    .WithTags("Teams")
    .Produces(200);

app.MapGet("/api/teams/{id}", async (Guid id) => 
{
    var result = await mediator.Send(new GetTeamByIdQuery(id));
    return result != null ? Results.Ok(result) : Results.NotFound();
})
    .WithName("GetTeamById")
    .WithTags("Teams")
    .Produces(200)
    .Produces(404);

app.MapPost("/api/institutes/{instituteId}/teams", async (Guid instituteId, CreateTeamCommand command) => 
{
    if (instituteId != command.InstituteId) return Results.BadRequest();
    var result = await mediator.Send(command);
    return Results.Created($"/api/teams/{result.Id}", result);
})
    .WithName("CreateTeam")
    .WithTags("Teams")
    .Produces(201)
    .Produces(400);

app.MapGet("/api/teams/{id}/members", async (Guid id) => 
    await mediator.Send(new GetTeamMembersQuery(id)))
    .WithName("GetTeamMembers")
    .WithTags("Teams")
    .Produces(200);

// Seed Data Endpoint
app.MapPost("/api/seed", async () => 
{
    var seeder = app.Services.GetRequiredService<InstitutionSeedService>();
    await seeder.SeedAsync();
    return Results.Ok(new { Message = "Database seeded successfully" });
})
    .WithName("SeedDatabase")
    .WithTags("Seed Data")
    .Produces(200);

app.Run();
