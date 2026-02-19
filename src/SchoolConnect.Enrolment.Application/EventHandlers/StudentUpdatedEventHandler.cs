using Microsoft.Extensions.Logging;
using SchoolConnect.Common.Domain.Interfaces;
using SchoolConnect.Enrolment.Application.ReadModels;
using SchoolConnect.Enrolment.Domain.Events;

namespace SchoolConnect.Enrolment.Application.EventHandlers;

public class StudentUpdatedEventHandler : IDomainEventHandler<StudentUpdatedEvent>
{
    private readonly IStudentReadModelRepository _readModelRepository;
    private readonly ILogger<StudentUpdatedEventHandler> _logger;

    public StudentUpdatedEventHandler(
        IStudentReadModelRepository readModelRepository,
        ILogger<StudentUpdatedEventHandler> logger)
    {
        _readModelRepository = readModelRepository;
        _logger = logger;
    }

    public async Task Handle(StudentUpdatedEvent domainEvent, CancellationToken ct = default)
    {
        try
        {
            _logger.LogInformation(
                "Processing StudentUpdatedEvent for student {StudentId} (Code: {StudentCode})",
                domainEvent.AggregateId,
                domainEvent.StudentCode);

            // Get existing read model
            var readModel = await _readModelRepository.GetByIdAsync(domainEvent.AggregateId, ct);
            
            if (readModel == null)
            {
                _logger.LogWarning(
                    "Student read model {StudentId} not found, cannot update",
                    domainEvent.AggregateId);
                return;
            }

            // Check idempotency
            if (readModel.Version >= domainEvent.Version)
            {
                _logger.LogInformation(
                    "Student read model {StudentId} already has version {Version}, skipping",
                    domainEvent.AggregateId,
                    readModel.Version);
                return;
            }

            // Update version and timestamp
            readModel.Version = domainEvent.Version;
            readModel.LastUpdated = domainEvent.OccurredOn;

            await _readModelRepository.UpsertAsync(readModel, ct);

            _logger.LogInformation(
                "Successfully processed StudentUpdatedEvent for student {StudentId}",
                domainEvent.AggregateId);
        }
        catch (Exception ex)
        {
            _logger.LogError(
                ex,
                "Error processing StudentUpdatedEvent for student {StudentId}",
                domainEvent.AggregateId);
            throw;
        }
    }
}
