using SchoolConnect.Billing.Infrastructure.Extensions;

var builder = WebApplication.CreateBuilder(args);

// Add configuration
var mongoConnectionString = builder.Configuration.GetValue<string>("MongoDB:ConnectionString") ?? "mongodb://localhost:27017";
var mongoDatabaseName = builder.Configuration.GetValue<string>("MongoDB:DatabaseName") ?? "BillingDb";

// Add Infrastructure services
builder.Services.AddBillingInfrastructure(mongoConnectionString, mongoDatabaseName);

// NOTE: MediatR handlers need to be fully implemented before enabling
// builder.Services.AddMediatR(cfg => {
//     cfg.RegisterServicesFromAssembly(typeof(CreateBillingAccountCommand).Assembly);
// });

// Add Swagger/OpenAPI
builder.Services.AddEndpointsApiExplorer();
builder.Services.AddSwaggerGen(c =>
{
    c.SwaggerDoc("v1", new() { 
        Title = "SchoolConnect Billing Management API", 
        Version = "v1",
        Description = "API for managing billing accounts, invoices, payments, payment methods, transactions, and credit notes"
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
        c.SwaggerEndpoint("/swagger/v1/swagger.json", "SchoolConnect Billing Management API v1");
        c.RoutePrefix = string.Empty;
    });
}

app.UseCors();
app.UseHttpsRedirection();

// NOTE: Full API endpoints will be implemented once handlers are complete
// Endpoints include:
// - Billing Accounts: POST, GET, PUT for CRUD operations  
// - Invoices: POST, GET, POST send, POST cancel, GET PDF
// - Payments: POST process, GET, POST refund
// - Payment Methods: POST, GET, DELETE, PUT default
// - Transactions: GET history
// - Credit Notes: POST issue, GET, POST apply

app.MapGet("/api/health", () => Results.Ok(new { Status = "Healthy", Service = "Billing" }))
    .WithName("HealthCheck")
    .WithTags("Health")
    .Produces(200);

app.Run();
