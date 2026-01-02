using SchoolConnect.LessonDelivery.Infrastructure.Extensions;
using SchoolConnect.LessonDelivery.Application.Mappers;
using SchoolConnect.LessonDelivery.Api.Endpoints;

var builder = WebApplication.CreateBuilder(args);

// Add services to the container
builder.Services.AddEndpointsApiExplorer();
builder.Services.AddSwaggerGen();

// Add MediatR
builder.Services.AddMediatR(cfg => cfg.RegisterServicesFromAssembly(
    typeof(LessonDeliveryMappingProfile).Assembly));

// Add AutoMapper
builder.Services.AddAutoMapper(typeof(LessonDeliveryMappingProfile).Assembly);

// Add Infrastructure
var connectionString = builder.Configuration.GetConnectionString("MongoDB") 
    ?? "mongodb://localhost:27017";
var databaseName = builder.Configuration.GetValue<string>("MongoDB:DatabaseName") 
    ?? "SchoolConnectLessonDelivery";

builder.Services.AddLessonDeliveryInfrastructure(connectionString, databaseName);

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
app.MapLessonPlanEndpoints();
app.MapScheduledLessonEndpoints();
app.MapLessonSessionEndpoints();
app.MapAttendanceEndpoints();
app.MapHomeworkEndpoints();

app.Run();
