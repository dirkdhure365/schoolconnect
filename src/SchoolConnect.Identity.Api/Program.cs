using MediatR;
using Microsoft.AspNetCore.Authentication.JwtBearer;
using Microsoft.IdentityModel.Tokens;
using SchoolConnect.Identity.Application.Commands.Auth;
using SchoolConnect.Identity.Application.Commands.Roles;
using SchoolConnect.Identity.Application.Mappers;
using SchoolConnect.Identity.Application.Queries.Permissions;
using SchoolConnect.Identity.Application.Queries.Roles;
using SchoolConnect.Identity.Application.Queries.Users;
using SchoolConnect.Identity.Application.Services;
using SchoolConnect.Identity.Domain.Interfaces;
using SchoolConnect.Identity.Infrastructure.Persistence;
using SchoolConnect.Identity.Infrastructure.Repositories;
using SchoolConnect.Identity.Infrastructure.Seed;
using SchoolConnect.Identity.Infrastructure.Services;
using System.Text;
using FluentValidation;
using FluentValidation.AspNetCore;

var builder = WebApplication.CreateBuilder(args);

// Configuration
var mongoConnectionString = builder.Configuration.GetValue<string>("MongoDB:ConnectionString") ?? "mongodb://localhost:27017";
var mongoDatabaseName = builder.Configuration.GetValue<string>("MongoDB:DatabaseName") ?? "IdentityDb";
var jwtSecret = builder.Configuration.GetValue<string>("JWT:Secret") ?? "your-256-bit-secret-key-change-this-in-production-minimum-32-characters";
var jwtIssuer = builder.Configuration.GetValue<string>("JWT:Issuer") ?? "SchoolConnect.Identity";
var jwtAudience = builder.Configuration.GetValue<string>("JWT:Audience") ?? "SchoolConnect";

// Register MongoDB Context
builder.Services.AddSingleton(sp => new IdentityDbContext(mongoConnectionString, mongoDatabaseName));

// Register Repositories
builder.Services.AddScoped<IUserRepository, UserRepository>();
builder.Services.AddScoped<IRoleRepository, RoleRepository>();
builder.Services.AddScoped<IPermissionRepository, PermissionRepository>();
builder.Services.AddScoped<ISessionRepository, SessionRepository>();

// Register Services
builder.Services.AddSingleton<ITokenService>(sp => 
    new TokenService(jwtSecret, jwtIssuer, jwtAudience));
builder.Services.AddSingleton<IPasswordHasher, PasswordHasher>();
builder.Services.AddSingleton<IMfaService, MfaService>();
builder.Services.AddScoped<IEmailService, EmailService>();

// Register Seed Service
builder.Services.AddScoped<IdentitySeedService>();

// Add AutoMapper
builder.Services.AddAutoMapper(typeof(IdentityMappingProfile));

// Add FluentValidation
builder.Services.AddFluentValidationAutoValidation();
builder.Services.AddValidatorsFromAssemblyContaining<RegisterUserCommand>();

// Add MediatR
builder.Services.AddMediatR(cfg => {
    cfg.RegisterServicesFromAssembly(typeof(RegisterUserCommand).Assembly);
});

// Add Authentication
builder.Services.AddAuthentication(JwtBearerDefaults.AuthenticationScheme)
    .AddJwtBearer(options =>
    {
        options.TokenValidationParameters = new TokenValidationParameters
        {
            ValidateIssuer = true,
            ValidateAudience = true,
            ValidateLifetime = true,
            ValidateIssuerSigningKey = true,
            ValidIssuer = jwtIssuer,
            ValidAudience = jwtAudience,
            IssuerSigningKey = new SymmetricSecurityKey(Encoding.UTF8.GetBytes(jwtSecret))
        };
    });

builder.Services.AddAuthorization();

// Add Swagger/OpenAPI
builder.Services.AddEndpointsApiExplorer();
builder.Services.AddSwaggerGen();

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
    app.UseSwaggerUI(c =>
    {
        c.SwaggerEndpoint("/swagger/v1/swagger.json", "SchoolConnect Identity & Access API v1");
        c.RoutePrefix = string.Empty;
    });
}

app.UseCors();
app.UseHttpsRedirection();
app.UseAuthentication();
app.UseAuthorization();

var mediator = app.Services.GetRequiredService<IMediator>();

// Auth Endpoints
app.MapPost("/api/auth/register", async (RegisterUserCommand command) =>
{
    var result = await mediator.Send(command);
    return result.IsSuccess ? Results.Ok(result.Value) : Results.BadRequest(result.Error);
})
.WithName("Register")
.WithTags("Authentication")
.Produces(200)
.Produces(400);

app.MapPost("/api/auth/login", async (LoginCommand command) =>
{
    var result = await mediator.Send(command);
    return result.IsSuccess ? Results.Ok(result.Value) : Results.BadRequest(result.Error);
})
.WithName("Login")
.WithTags("Authentication")
.Produces(200)
.Produces(400);

app.MapPost("/api/auth/logout", async (LogoutCommand command) =>
{
    var result = await mediator.Send(command);
    return result.IsSuccess ? Results.Ok() : Results.BadRequest(result.Error);
})
.WithName("Logout")
.WithTags("Authentication")
.RequireAuthorization()
.Produces(200)
.Produces(400);

app.MapPost("/api/auth/change-password", async (ChangePasswordCommand command) =>
{
    var result = await mediator.Send(command);
    return result.IsSuccess ? Results.Ok() : Results.BadRequest(result.Error);
})
.WithName("ChangePassword")
.WithTags("Authentication")
.RequireAuthorization()
.Produces(200)
.Produces(400);

// User Endpoints
app.MapGet("/api/users/{id}", async (Guid id) =>
{
    var user = await mediator.Send(new GetUserByIdQuery(id));
    return user != null ? Results.Ok(user) : Results.NotFound();
})
.WithName("GetUserById")
.WithTags("Users")
.RequireAuthorization()
.Produces(200)
.Produces(404);

// Role Endpoints
app.MapGet("/api/roles", async () =>
{
    var roles = await mediator.Send(new GetRolesQuery());
    return Results.Ok(roles);
})
.WithName("GetRoles")
.WithTags("Roles")
.RequireAuthorization()
.Produces(200);

app.MapPost("/api/roles", async (CreateRoleCommand command) =>
{
    var result = await mediator.Send(command);
    return result.IsSuccess ? Results.Created($"/api/roles/{result.Value!.Id}", result.Value) : Results.BadRequest(result.Error);
})
.WithName("CreateRole")
.WithTags("Roles")
.RequireAuthorization()
.Produces(201)
.Produces(400);

// Permission Endpoints
app.MapGet("/api/permissions", async () =>
{
    var permissions = await mediator.Send(new GetPermissionsQuery());
    return Results.Ok(permissions);
})
.WithName("GetPermissions")
.WithTags("Permissions")
.RequireAuthorization()
.Produces(200);

// Seed Data Endpoint
app.MapPost("/api/seed", async () =>
{
    var seeder = app.Services.GetRequiredService<IdentitySeedService>();
    await seeder.SeedAsync();
    return Results.Ok(new { Message = "Database seeded successfully" });
})
.WithName("SeedDatabase")
.WithTags("Seed Data")
.Produces(200);

app.Run();
