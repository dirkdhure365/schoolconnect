using SchoolConnect.Institution.Infrastructure.Extensions;
using SchoolConnect.Institution.Application.Mappers;
using SchoolConnect.Institution.Api.Endpoints;

var builder = WebApplication.CreateBuilder(args);

// Add services to the container
builder.Services.AddEndpointsApiExplorer();
builder.Services.AddSwaggerGen();

// Add MediatR
builder.Services.AddMediatR(cfg => cfg.RegisterServicesFromAssembly(
    typeof(InstitutionMappingProfile).Assembly));

// Add AutoMapper
builder.Services.AddAutoMapper(typeof(InstitutionMappingProfile).Assembly);

// Add Infrastructure
var connectionString = builder.Configuration.GetConnectionString("MongoDB") 
    ?? "mongodb://localhost:27017";
var databaseName = builder.Configuration.GetValue<string>("MongoDB:DatabaseName") 
    ?? "SchoolConnectInstitution";

builder.Services.AddInstitutionInfrastructure(connectionString, databaseName);

// Add CORS
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

// Configure the HTTP request pipeline
if (app.Environment.IsDevelopment())
{
    app.UseSwagger();
    app.UseSwaggerUI();
}

app.UseHttpsRedirection();
app.UseCors();

// Map endpoints
app.MapInstituteEndpoints();
app.MapCentreEndpoints();
app.MapFacilityEndpoints();
app.MapResourceEndpoints();
app.MapStaffEndpoints();
app.MapTeamEndpoints();

app.Run();
