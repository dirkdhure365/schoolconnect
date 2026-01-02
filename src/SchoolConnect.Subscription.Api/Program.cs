using MediatR;
using SchoolConnect.Subscription.Application.Commands;
using SchoolConnect.Subscription.Application.Queries;
using SchoolConnect.Subscription.Infrastructure.Extensions;

var builder = WebApplication.CreateBuilder(args);

// Add configuration
var mongoConnectionString = builder.Configuration.GetValue<string>("MongoDB:ConnectionString") ?? "mongodb://localhost:27017";
var mongoDatabaseName = builder.Configuration.GetValue<string>("MongoDB:DatabaseName") ?? "SubscriptionDb";

// Add Infrastructure services
builder.Services.AddSubscriptionInfrastructure(mongoConnectionString, mongoDatabaseName);

// Add MediatR
builder.Services.AddMediatR(cfg => {
    cfg.RegisterServicesFromAssembly(typeof(CreateSubscriptionPlanCommand).Assembly);
});

// Add Swagger/OpenAPI
builder.Services.AddEndpointsApiExplorer();
builder.Services.AddSwaggerGen(c =>
{
    c.SwaggerDoc("v1", new() { 
        Title = "SchoolConnect Subscription Management API", 
        Version = "v1",
        Description = "API for managing subscription plans, subscriptions, trials, and usage tracking"
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
        c.SwaggerEndpoint("/swagger/v1/swagger.json", "SchoolConnect Subscription Management API v1");
        c.RoutePrefix = string.Empty;
    });
}

app.UseCors();
app.UseHttpsRedirection();

var mediator = app.Services.GetRequiredService<IMediator>();

// ===================== PLAN ENDPOINTS =====================

app.MapGet("/api/plans", async () => 
    await mediator.Send(new GetActivePlansQuery()))
    .WithName("GetAllActivePlans")
    .WithTags("Plans")
    .Produces(200);

app.MapGet("/api/plans/{id}", async (Guid id) => 
{
    var result = await mediator.Send(new GetSubscriptionPlanByIdQuery(id));
    return result != null ? Results.Ok(result) : Results.NotFound();
})
    .WithName("GetPlanById")
    .WithTags("Plans")
    .Produces(200)
    .Produces(404);

app.MapPost("/api/plans", async (CreateSubscriptionPlanCommand command) => 
{
    var result = await mediator.Send(command);
    return Results.Created($"/api/plans/{result.Id}", result);
})
    .WithName("CreatePlan")
    .WithTags("Plans")
    .Produces(201);

app.MapPut("/api/plans/{id}", async (Guid id, UpdateSubscriptionPlanCommand command) => 
{
    if (id != command.Id) return Results.BadRequest();
    var result = await mediator.Send(command);
    return Results.Ok(result);
})
    .WithName("UpdatePlan")
    .WithTags("Plans")
    .Produces(200)
    .Produces(400);

app.MapDelete("/api/plans/{id}", async (Guid id) => 
{
    var result = await mediator.Send(new DeactivatePlanCommand(id));
    return result ? Results.Ok() : Results.NotFound();
})
    .WithName("DeactivatePlan")
    .WithTags("Plans")
    .Produces(200)
    .Produces(404);

// ===================== SUBSCRIPTION ENDPOINTS =====================

app.MapPost("/api/subscriptions", async (CreateSubscriptionCommand command) => 
{
    var result = await mediator.Send(command);
    return Results.Created($"/api/subscriptions/{result.Id}", result);
})
    .WithName("CreateSubscription")
    .WithTags("Subscriptions")
    .Produces(201);

app.MapGet("/api/subscriptions/{id}", async (Guid id) => 
{
    var result = await mediator.Send(new GetSubscriptionByIdQuery(id));
    return result != null ? Results.Ok(result) : Results.NotFound();
})
    .WithName("GetSubscriptionById")
    .WithTags("Subscriptions")
    .Produces(200)
    .Produces(404);

app.MapGet("/api/institutes/{instituteId}/subscription", async (Guid instituteId) => 
{
    var result = await mediator.Send(new GetSubscriptionByInstituteQuery(instituteId));
    return result != null ? Results.Ok(result) : Results.NotFound();
})
    .WithName("GetInstituteSubscription")
    .WithTags("Subscriptions")
    .Produces(200)
    .Produces(404);

app.MapPut("/api/subscriptions/{id}/upgrade", async (Guid id, UpgradeSubscriptionCommand command) => 
{
    if (id != command.Id) return Results.BadRequest();
    var result = await mediator.Send(command);
    return Results.Ok(result);
})
    .WithName("UpgradeSubscription")
    .WithTags("Subscriptions")
    .Produces(200)
    .Produces(400);

app.MapPut("/api/subscriptions/{id}/downgrade", async (Guid id, DowngradeSubscriptionCommand command) => 
{
    if (id != command.Id) return Results.BadRequest();
    var result = await mediator.Send(command);
    return Results.Ok(result);
})
    .WithName("DowngradeSubscription")
    .WithTags("Subscriptions")
    .Produces(200)
    .Produces(400);

app.MapPut("/api/subscriptions/{id}/cancel", async (Guid id, CancelSubscriptionCommand command) => 
{
    if (id != command.Id) return Results.BadRequest();
    var result = await mediator.Send(command);
    return Results.Ok(result);
})
    .WithName("CancelSubscription")
    .WithTags("Subscriptions")
    .Produces(200)
    .Produces(400);

app.MapPut("/api/subscriptions/{id}/renew", async (Guid id) => 
{
    var result = await mediator.Send(new RenewSubscriptionCommand(id));
    return Results.Ok(result);
})
    .WithName("RenewSubscription")
    .WithTags("Subscriptions")
    .Produces(200);

// ===================== TRIAL ENDPOINTS =====================

app.MapPost("/api/subscriptions/{id}/trial", async (Guid id, StartTrialCommand command) => 
{
    if (id != command.SubscriptionId) return Results.BadRequest();
    var result = await mediator.Send(command);
    return Results.Created($"/api/subscriptions/{id}/trial", result);
})
    .WithName("StartTrial")
    .WithTags("Trials")
    .Produces(201)
    .Produces(400);

app.MapGet("/api/subscriptions/{id}/trial", async (Guid id) => 
{
    var result = await mediator.Send(new GetTrialStatusQuery(id));
    return result != null ? Results.Ok(result) : Results.NotFound();
})
    .WithName("GetTrialStatus")
    .WithTags("Trials")
    .Produces(200)
    .Produces(404);

app.MapPost("/api/subscriptions/{id}/trial/convert", async (Guid id, ConvertTrialCommand command) => 
{
    var result = await mediator.Send(command);
    return Results.Ok(result);
})
    .WithName("ConvertTrial")
    .WithTags("Trials")
    .Produces(200);

// ===================== USAGE ENDPOINTS =====================

app.MapGet("/api/subscriptions/{id}/usage", async (Guid id, int? month, int? year) => 
{
    var result = await mediator.Send(new GetSubscriptionUsageQuery(id, month, year));
    return Results.Ok(result);
})
    .WithName("GetUsageMetrics")
    .WithTags("Usage")
    .Produces(200);

app.MapPost("/api/subscriptions/{id}/usage", async (Guid id, RecordUsageCommand command) => 
{
    if (id != command.SubscriptionId) return Results.BadRequest();
    var result = await mediator.Send(command);
    return Results.Created($"/api/subscriptions/{id}/usage", result);
})
    .WithName("RecordUsage")
    .WithTags("Usage")
    .Produces(201)
    .Produces(400);

app.Run();
