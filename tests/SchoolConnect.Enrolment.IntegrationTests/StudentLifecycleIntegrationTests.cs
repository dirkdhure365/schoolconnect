using Microsoft.Extensions.DependencyInjection;
using Microsoft.Extensions.Logging;
using MongoDB.Driver;
using SchoolConnect.Common.Infrastructure.EventDispatcher;
using SchoolConnect.Enrolment.Application.ReadModels;
using SchoolConnect.Enrolment.Domain.Entities;
using SchoolConnect.Enrolment.Domain.Enums;
using SchoolConnect.Enrolment.Domain.Interfaces;
using SchoolConnect.Enrolment.Infrastructure.Extensions;
using SchoolConnect.Enrolment.Infrastructure.Persistence;
using Testcontainers.MongoDb;
using FluentAssertions;

namespace SchoolConnect.Enrolment.IntegrationTests;

public class StudentLifecycleIntegrationTests : IAsyncLifetime
{
    private MongoDbContainer? _mongoContainer;
    private ServiceProvider? _serviceProvider;
    private string _connectionString = string.Empty;

    public async Task InitializeAsync()
    {
        // Start MongoDB container
        _mongoContainer = new MongoDbBuilder()
            .WithImage("mongo:7.0")
            .Build();

        await _mongoContainer.StartAsync();
        _connectionString = _mongoContainer.GetConnectionString();

        // Setup DI container
        var services = new ServiceCollection();

        // Add logging
        services.AddLogging(builder => builder.AddConsole());

        // Add infrastructure with test database
        services.AddEnrolmentInfrastructure(_connectionString, "SchoolConnectEnrolment_Test");

        _serviceProvider = services.BuildServiceProvider();
    }

    public async Task DisposeAsync()
    {
        if (_serviceProvider != null)
        {
            await _serviceProvider.DisposeAsync();
        }

        if (_mongoContainer != null)
        {
            await _mongoContainer.StopAsync();
            await _mongoContainer.DisposeAsync();
        }
    }

    [Fact]
    public async Task CreateStudent_ShouldStoreEventAndUpdateReadModel()
    {
        // Arrange
        using var scope = _serviceProvider!.CreateScope();
        var studentRepository = scope.ServiceProvider.GetRequiredService<IStudentRepository>();
        var readModelRepository = scope.ServiceProvider.GetRequiredService<IStudentReadModelRepository>();

        var instituteId = Guid.NewGuid();
        var studentCode = "STU-001";

        // Act - Create student
        var student = Student.Create(
            instituteId: instituteId,
            studentCode: studentCode,
            firstName: "John",
            lastName: "Doe",
            dateOfBirth: new DateTime(2010, 1, 15),
            gender: Gender.Male);

        // Save student (this should trigger event dispatching automatically)
        await studentRepository.AddAsync(student);

        // Wait a bit for async processing
        await Task.Delay(100);

        // Assert - Check read model was created
        var readModel = await readModelRepository.GetByIdAsync(student.Id);

        readModel.Should().NotBeNull();
        readModel!.Id.Should().Be(student.Id);
        readModel.InstituteId.Should().Be(instituteId);
        readModel.StudentCode.Should().Be(studentCode);
        readModel.FirstName.Should().Be("John");
        readModel.LastName.Should().Be("Doe");
        readModel.FullName.Should().Be("John Doe");
        readModel.Status.Should().Be("Active");
    }

    [Fact]
    public async Task UpdateStudent_ShouldStoreEventAndUpdateReadModel()
    {
        // Arrange
        using var scope = _serviceProvider!.CreateScope();
        var studentRepository = scope.ServiceProvider.GetRequiredService<IStudentRepository>();
        var readModelRepository = scope.ServiceProvider.GetRequiredService<IStudentReadModelRepository>();

        var instituteId = Guid.NewGuid();
        var student = Student.Create(
            instituteId: instituteId,
            studentCode: "STU-002",
            firstName: "Jane",
            lastName: "Smith",
            dateOfBirth: new DateTime(2011, 5, 20),
            gender: Gender.Female);

        await studentRepository.AddAsync(student);
        await Task.Delay(100);

        // Act - Update student
        student.Update(
            firstName: "Jane",
            lastName: "Smith-Johnson",
            email: "jane.smith@example.com");

        await studentRepository.UpdateAsync(student);
        await Task.Delay(100);

        // Assert
        var readModel = await readModelRepository.GetByIdAsync(student.Id);

        readModel.Should().NotBeNull();
        readModel!.Version.Should().BeGreaterThan(0);
    }

    [Fact]
    public async Task EnrolStudent_ShouldStoreEventAndUpdateEnrolmentReadModel()
    {
        // Arrange
        using var scope = _serviceProvider!.CreateScope();
        var context = scope.ServiceProvider.GetRequiredService<EnrolmentDbContext>();
        var studentRepository = scope.ServiceProvider.GetRequiredService<IStudentRepository>();
        var enrolmentReadModelRepository = scope.ServiceProvider.GetRequiredService<IStudentEnrolmentSummaryReadModelRepository>();
        var dispatcher = scope.ServiceProvider.GetRequiredService<IDomainEventDispatcher>();

        // Create student first
        var instituteId = Guid.NewGuid();
        var student = Student.Create(
            instituteId: instituteId,
            studentCode: "STU-003",
            firstName: "Bob",
            lastName: "Wilson",
            dateOfBirth: new DateTime(2012, 3, 10),
            gender: Gender.Male);

        await studentRepository.AddAsync(student);
        await dispatcher.DispatchAsync(student.DomainEvents);
        await Task.Delay(100);

        var streamId = Guid.NewGuid();
        var cohortId = Guid.NewGuid();

        // Act - Create student enrolment
        var enrolment = StudentEnrolment.Create(
            studentId: student.Id,
            streamId: streamId,
            cohortId: cohortId,
            currentGradeLevel: 1,
            enrolledBy: Guid.NewGuid());

        // Save enrolment
        await context.StudentEnrolments.InsertOneAsync(enrolment);
        await dispatcher.DispatchAsync(enrolment.DomainEvents);
        await Task.Delay(100);

        // Assert - Check enrolment read model
        var enrolmentReadModel = await enrolmentReadModelRepository.GetByIdAsync(enrolment.Id);

        enrolmentReadModel.Should().NotBeNull();
        enrolmentReadModel!.Id.Should().Be(enrolment.Id);
        enrolmentReadModel.StudentId.Should().Be(student.Id);
        enrolmentReadModel.StreamId.Should().Be(streamId);
        enrolmentReadModel.CohortId.Should().Be(cohortId);
        enrolmentReadModel.Status.Should().Be("Active");
    }

    [Fact]
    public async Task WithdrawStudent_ShouldStoreEventAndUpdateStatusInReadModel()
    {
        // Arrange
        using var scope = _serviceProvider!.CreateScope();
        var studentRepository = scope.ServiceProvider.GetRequiredService<IStudentRepository>();
        var readModelRepository = scope.ServiceProvider.GetRequiredService<IStudentReadModelRepository>();

        var instituteId = Guid.NewGuid();
        var student = Student.Create(
            instituteId: instituteId,
            studentCode: "STU-004",
            firstName: "Alice",
            lastName: "Brown",
            dateOfBirth: new DateTime(2013, 7, 25),
            gender: Gender.Female);

        await studentRepository.AddAsync(student);
        await Task.Delay(100);

        // Act - Withdraw student
        student.Withdraw("Moving to another school");
        await studentRepository.UpdateAsync(student);
        await Task.Delay(100);

        // Assert
        var readModel = await readModelRepository.GetByIdAsync(student.Id);

        readModel.Should().NotBeNull();
        readModel!.Status.Should().Be("Withdrawn");
        readModel.WithdrawnAt.Should().NotBeNull();
        readModel.WithdrawalReason.Should().Be("Moving to another school");
    }

    [Fact]
    public async Task EventHandlers_ShouldBeIdempotent()
    {
        // Arrange
        using var scope = _serviceProvider!.CreateScope();
        var studentRepository = scope.ServiceProvider.GetRequiredService<IStudentRepository>();
        var readModelRepository = scope.ServiceProvider.GetRequiredService<IStudentReadModelRepository>();
        var dispatcher = scope.ServiceProvider.GetRequiredService<IDomainEventDispatcher>();

        var instituteId = Guid.NewGuid();
        var student = Student.Create(
            instituteId: instituteId,
            studentCode: "STU-005",
            firstName: "Charlie",
            lastName: "Davis",
            dateOfBirth: new DateTime(2014, 9, 30),
            gender: Gender.Male);

        await studentRepository.AddAsync(student);

        // Act - Dispatch the same events multiple times
        var events = student.DomainEvents.ToList();
        await dispatcher.DispatchAsync(events);
        await Task.Delay(100);
        await dispatcher.DispatchAsync(events);
        await Task.Delay(100);
        await dispatcher.DispatchAsync(events);
        await Task.Delay(100);

        // Assert - Should still only have one read model with correct data
        var readModels = await readModelRepository.GetByInstituteAsync(instituteId);
        var readModelsList = readModels.ToList();

        readModelsList.Should().HaveCount(1);
        readModelsList[0].StudentCode.Should().Be("STU-005");
    }
}
