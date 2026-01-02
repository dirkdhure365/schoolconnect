using SchoolConnect.Curriculum.Api.Endpoints;
using SchoolConnect.Curriculum.Application.Services;

var builder = WebApplication.CreateBuilder(args);

// Add services to the container
builder.Services.AddEndpointsApiExplorer();
builder.Services.AddSwaggerGen(options =>
{
    options.SwaggerDoc("v1", new()
    {
        Title = "SchoolConnect Curriculum API",
        Version = "v1",
        Description = "Generic, extensible curriculum API supporting multiple education systems and examination boards"
    });
});

// Register curriculum service factory as singleton
builder.Services.AddSingleton<ICurriculumServiceFactory>(sp =>
{
    var factory = new CurriculumServiceFactory();
    
    // Register CAPS board
    // Note: In a real application, you would register actual repository implementations
    // factory.RegisterService("CAPS", () => new CapsCurriculumService(capsRepository),
    //     new BoardInfo
    //     {
    //         Code = "CAPS",
    //         Name = "Curriculum and Assessment Policy Statement",
    //         Country = "South Africa",
    //         ExaminationBoard = "Department of Basic Education",
    //         SupportsPracticalAssessments = true
    //     });
    
    // Register ZIMSEC board
    // factory.RegisterService("ZIMSEC", () => new ZimsecCurriculumService(zimsecRepository),
    //     new BoardInfo
    //     {
    //         Code = "ZIMSEC",
    //         Name = "Zimbabwe School Examinations Council Curriculum",
    //         Country = "Zimbabwe",
    //         ExaminationBoard = "Zimbabwe School Examinations Council",
    //         SupportsPracticalAssessments = false
    //     });
    
    return factory;
});

var app = builder.Build();

// Configure the HTTP request pipeline
if (app.Environment.IsDevelopment())
{
    app.UseSwagger();
    app.UseSwaggerUI();
}

app.UseHttpsRedirection();

// Map curriculum endpoints
app.MapCurriculumDiscoveryEndpoints();
app.MapCurriculumEndpoints();
app.MapPracticalCurriculumEndpoints();

app.Run();
