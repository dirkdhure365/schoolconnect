using SchoolConnect.Collaboration.Api.Endpoints;
using SchoolConnect.Collaboration.Infrastructure.Extensions;

var builder = WebApplication.CreateBuilder(args);

// Add services to the container
builder.Services.AddEndpointsApiExplorer();
builder.Services.AddSwaggerGen();

// Add MediatR
builder.Services.AddMediatR(cfg => 
    cfg.RegisterServicesFromAssembly(typeof(SchoolConnect.Collaboration.Application.DTOs.WorkspaceDto).Assembly));

// Add AutoMapper
builder.Services.AddAutoMapper(typeof(SchoolConnect.Collaboration.Application.Mappers.CollaborationMappingProfile));

// Add Collaboration Infrastructure
var mongoConnectionString = builder.Configuration.GetConnectionString("MongoDB") 
    ?? "mongodb://localhost:27017";
var databaseName = builder.Configuration["MongoDB:DatabaseName"] ?? "schoolconnect_collaboration";

builder.Services.AddCollaborationInfrastructure(mongoConnectionString, databaseName);

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
app.MapWorkspaceEndpoints();
app.MapBoardEndpoints();
app.MapListEndpoints();
app.MapCardEndpoints();

app.MapGet("/", () => "SchoolConnect Collaboration API - Running");

app.Run();
