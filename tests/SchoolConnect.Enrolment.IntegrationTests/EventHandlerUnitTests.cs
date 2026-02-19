using Xunit;
using Moq;
using FluentAssertions;
using Microsoft.Extensions.Logging;
using SchoolConnect.Enrolment.Application.EventHandlers;
using SchoolConnect.Enrolment.Application.ReadModels;
using SchoolConnect.Enrolment.Domain.Events;

namespace SchoolConnect.Enrolment.IntegrationTests;

public class EventHandlerUnitTests
{
    [Fact]
    public async Task StudentCreatedEventHandler_ShouldCreateReadModel()
    {
        // Arrange
        var mockRepository = new Mock<IStudentReadModelRepository>();
        var mockLogger = new Mock<ILogger<StudentCreatedEventHandler>>();
        
        StudentReadModel? capturedModel = null;
        mockRepository
            .Setup(r => r.UpsertAsync(It.IsAny<StudentReadModel>(), It.IsAny<CancellationToken>()))
            .Callback<StudentReadModel, CancellationToken>((model, ct) => capturedModel = model)
            .Returns(Task.CompletedTask);

        var handler = new StudentCreatedEventHandler(mockRepository.Object, mockLogger.Object);

        var studentId = Guid.NewGuid();
        var instituteId = Guid.NewGuid();
        var @event = new StudentCreatedEvent
        {
            AggregateId = studentId,
            AggregateType = "Student",
            InstituteId = instituteId,
            StudentCode = "STU-001",
            FirstName = "John",
            LastName = "Doe",
            Version = 1
        };

        // Act
        await handler.Handle(@event, CancellationToken.None);

        // Assert
        capturedModel.Should().NotBeNull();
        capturedModel!.Id.Should().Be(studentId);
        capturedModel.InstituteId.Should().Be(instituteId);
        capturedModel.StudentCode.Should().Be("STU-001");
        capturedModel.FirstName.Should().Be("John");
        capturedModel.LastName.Should().Be("Doe");
        capturedModel.FullName.Should().Be("John Doe");
        capturedModel.Status.Should().Be("Active");
        capturedModel.Version.Should().Be(1);
    }

    [Fact]
    public async Task StudentUpdatedEventHandler_ShouldUpdateReadModel()
    {
        // Arrange
        var mockRepository = new Mock<IStudentReadModelRepository>();
        var mockLogger = new Mock<ILogger<StudentUpdatedEventHandler>>();

        var studentId = Guid.NewGuid();
        var existingModel = new StudentReadModel
        {
            Id = studentId,
            StudentCode = "STU-002",
            Version = 1,
            LastUpdated = DateTime.UtcNow.AddHours(-1)
        };

        mockRepository
            .Setup(r => r.GetByIdAsync(studentId, It.IsAny<CancellationToken>()))
            .ReturnsAsync(existingModel);

        StudentReadModel? capturedModel = null;
        mockRepository
            .Setup(r => r.UpsertAsync(It.IsAny<StudentReadModel>(), It.IsAny<CancellationToken>()))
            .Callback<StudentReadModel, CancellationToken>((model, ct) => capturedModel = model)
            .Returns(Task.CompletedTask);

        var handler = new StudentUpdatedEventHandler(mockRepository.Object, mockLogger.Object);

        var @event = new StudentUpdatedEvent
        {
            AggregateId = studentId,
            AggregateType = "Student",
            StudentCode = "STU-002",
            Version = 2
        };

        // Act
        await handler.Handle(@event, CancellationToken.None);

        // Assert
        capturedModel.Should().NotBeNull();
        capturedModel!.Version.Should().Be(2);
    }

    [Fact]
    public async Task StudentWithdrawnEventHandler_ShouldUpdateStatus()
    {
        // Arrange
        var mockStudentRepository = new Mock<IStudentReadModelRepository>();
        var mockEnrolmentRepository = new Mock<IStudentEnrolmentSummaryReadModelRepository>();
        var mockLogger = new Mock<ILogger<StudentWithdrawnEventHandler>>();

        var studentId = Guid.NewGuid();
        var existingModel = new StudentReadModel
        {
            Id = studentId,
            StudentCode = "STU-003",
            Status = "Active",
            Version = 1
        };

        mockStudentRepository
            .Setup(r => r.GetByIdAsync(studentId, It.IsAny<CancellationToken>()))
            .ReturnsAsync(existingModel);

        mockEnrolmentRepository
            .Setup(r => r.GetByStudentIdAsync(studentId, It.IsAny<CancellationToken>()))
            .ReturnsAsync(new List<StudentEnrolmentSummaryReadModel>());

        StudentReadModel? capturedModel = null;
        mockStudentRepository
            .Setup(r => r.UpsertAsync(It.IsAny<StudentReadModel>(), It.IsAny<CancellationToken>()))
            .Callback<StudentReadModel, CancellationToken>((model, ct) => capturedModel = model)
            .Returns(Task.CompletedTask);

        var handler = new StudentWithdrawnEventHandler(
            mockStudentRepository.Object,
            mockEnrolmentRepository.Object,
            mockLogger.Object);

        var withdrawnAt = DateTime.UtcNow;
        var @event = new StudentWithdrawnEvent
        {
            AggregateId = Guid.NewGuid(),
            AggregateType = "Student",
            StudentId = studentId,
            Reason = "Test reason",
            WithdrawnAt = withdrawnAt,
            Version = 2
        };

        // Act
        await handler.Handle(@event, CancellationToken.None);

        // Assert
        capturedModel.Should().NotBeNull();
        capturedModel!.Status.Should().Be("Withdrawn");
        capturedModel.WithdrawnAt.Should().Be(withdrawnAt);
        capturedModel.WithdrawalReason.Should().Be("Test reason");
    }

    [Fact]
    public async Task StudentCreatedEventHandler_ShouldBeIdempotent()
    {
        // Arrange
        var mockRepository = new Mock<IStudentReadModelRepository>();
        var mockLogger = new Mock<ILogger<StudentCreatedEventHandler>>();

        var studentId = Guid.NewGuid();
        var existingModel = new StudentReadModel
        {
            Id = studentId,
            Version = 2  // Higher version than event
        };

        mockRepository
            .Setup(r => r.GetByIdAsync(studentId, It.IsAny<CancellationToken>()))
            .ReturnsAsync(existingModel);

        var handler = new StudentCreatedEventHandler(mockRepository.Object, mockLogger.Object);

        var @event = new StudentCreatedEvent
        {
            AggregateId = studentId,
            Version = 1  // Lower version
        };

        // Act
        await handler.Handle(@event, CancellationToken.None);

        // Assert - Should not call upsert
        mockRepository.Verify(
            r => r.UpsertAsync(It.IsAny<StudentReadModel>(), It.IsAny<CancellationToken>()),
            Times.Never);
    }
}
