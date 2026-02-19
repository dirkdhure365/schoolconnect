using Microsoft.Extensions.Logging;
using SchoolConnect.Common.Domain.Interfaces;
using SchoolConnect.Enrolment.Application.ReadModels;
using SchoolConnect.Enrolment.Domain.Events;

namespace SchoolConnect.Enrolment.Application.EventHandlers;

public class StudentCreatedEventHandler : IDomainEventHandler<StudentCreatedEvent>
{
    private readonly IStudentReadModelRepository _readModelRepository;
    private readonly ILogger<StudentCreatedEventHandler> _logger;

    public StudentCreatedEventHandler(
        IStudentReadModelRepository readModelRepository,
        ILogger<StudentCreatedEventHandler> logger)
    {
        _readModelRepository = readModelRepository;
        _logger = logger;
    }

    public async Task Handle(StudentCreatedEvent domainEvent, CancellationToken ct = default)
    {
        try
        {
            _logger.LogInformation(
                "Processing StudentCreatedEvent for student {StudentId} (Code: {StudentCode})",
                domainEvent.AggregateId,
                domainEvent.StudentCode);

            // Check if read model already exists (idempotency)
            var existing = await _readModelRepository.GetByIdAsync(domainEvent.AggregateId, ct);
            if (existing != null && existing.Version >= domainEvent.Version)
            {
                _logger.LogInformation(
                    "Student read model {StudentId} already exists with version {Version}, skipping",
                    domainEvent.AggregateId,
                    existing.Version);
                return;
            }

            // Project event to read model
            var readModel = new StudentReadModel
            {
                Id = domainEvent.AggregateId,
                InstituteId = domainEvent.InstituteId,
                StudentCode = domainEvent.StudentCode,
                FirstName = domainEvent.FirstName,
                LastName = domainEvent.LastName,
                FullName = $"{domainEvent.FirstName} {domainEvent.LastName}",
                Status = "Active",
                EnrolledAt = domainEvent.OccurredOn,
                LastUpdated = domainEvent.OccurredOn,
                Version = domainEvent.Version
            };

            await _readModelRepository.UpsertAsync(readModel, ct);

            _logger.LogInformation(
                "Successfully processed StudentCreatedEvent for student {StudentId}",
                domainEvent.AggregateId);
        }
        catch (Exception ex)
        {
            _logger.LogError(
                ex,
                "Error processing StudentCreatedEvent for student {StudentId}",
                domainEvent.AggregateId);
            throw;
        }
    }
}
