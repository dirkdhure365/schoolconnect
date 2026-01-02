using SchoolConnect.Enrolment.Infrastructure.Extensions;
using SchoolConnect.Enrolment.Application.Mappers;
using SchoolConnect.Enrolment.Api.Endpoints;

var builder = WebApplication.CreateBuilder(args);

// Add services to the container
builder.Services.AddEndpointsApiExplorer();
builder.Services.AddSwaggerGen();

// Add MediatR
builder.Services.AddMediatR(cfg => cfg.RegisterServicesFromAssembly(
    typeof(EnrolmentMappingProfile).Assembly));

// Add AutoMapper
builder.Services.AddAutoMapper(typeof(EnrolmentMappingProfile).Assembly);

// Add Infrastructure
var connectionString = builder.Configuration.GetConnectionString("MongoDB") 
    ?? "mongodb://localhost:27017";
var databaseName = builder.Configuration.GetValue<string>("MongoDB:DatabaseName") 
    ?? "SchoolConnectEnrolment";

builder.Services.AddEnrolmentInfrastructure(connectionString, databaseName);

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
app.MapAdmissionPeriodEndpoints();
app.MapStudentEndpoints();

app.Run();
