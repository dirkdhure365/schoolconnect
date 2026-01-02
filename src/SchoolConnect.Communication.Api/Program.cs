using SchoolConnect.Communication.Api.Endpoints;
using SchoolConnect.Communication.Infrastructure.Extensions;

var builder = WebApplication.CreateBuilder(args);

// Add services to the container
builder.Services.AddEndpointsApiExplorer();
builder.Services.AddSwaggerGen();

// Add MediatR
builder.Services.AddMediatR(cfg => 
    cfg.RegisterServicesFromAssembly(typeof(SchoolConnect.Communication.Application.DTOs.MessageDto).Assembly));

// Add AutoMapper
builder.Services.AddAutoMapper(typeof(SchoolConnect.Communication.Application.Mappers.CommunicationMappingProfile));

// Add Communication Infrastructure
var mongoConnectionString = builder.Configuration.GetConnectionString("MongoDB") 
    ?? "mongodb://localhost:27017";
var databaseName = builder.Configuration["MongoDB:DatabaseName"] ?? "schoolconnect_communication";

builder.Services.AddCommunicationInfrastructure(mongoConnectionString, databaseName);

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
app.MapMessageEndpoints();
app.MapNotificationEndpoints();
app.MapAnnouncementEndpoints();
app.MapFeedEndpoints();

app.MapGet("/", () => "SchoolConnect Communication API - Running");

app.Run();
