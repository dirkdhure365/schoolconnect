using Microsoft.Extensions.Logging;
using SchoolConnect.Common.Domain.Interfaces;
using SchoolConnect.Enrolment.Application.ReadModels;
using SchoolConnect.Enrolment.Domain.Events;
using SchoolConnect.Enrolment.Domain.Interfaces;

namespace SchoolConnect.Enrolment.Application.EventHandlers;

public class StudentEnrolledEventHandler : IDomainEventHandler<StudentEnrolledEvent>
{
    private readonly IStudentEnrolmentSummaryReadModelRepository _enrolmentReadModelRepository;
    private readonly IStudentRepository _studentRepository;
    private readonly ILogger<StudentEnrolledEventHandler> _logger;

    public StudentEnrolledEventHandler(
        IStudentEnrolmentSummaryReadModelRepository enrolmentReadModelRepository,
        IStudentRepository studentRepository,
        ILogger<StudentEnrolledEventHandler> logger)
    {
        _enrolmentReadModelRepository = enrolmentReadModelRepository;
        _studentRepository = studentRepository;
        _logger = logger;
    }

    public async Task Handle(StudentEnrolledEvent domainEvent, CancellationToken ct = default)
    {
        try
        {
            _logger.LogInformation(
                "Processing StudentEnrolledEvent for enrolment {EnrolmentId} (Student: {StudentId})",
                domainEvent.AggregateId,
                domainEvent.StudentId);

            // Check if read model already exists (idempotency)
            var existing = await _enrolmentReadModelRepository.GetByIdAsync(domainEvent.AggregateId, ct);
            if (existing != null && existing.Version >= domainEvent.Version)
            {
                _logger.LogInformation(
                    "Student enrolment summary read model {EnrolmentId} already exists with version {Version}, skipping",
                    domainEvent.AggregateId,
                    existing.Version);
                return;
            }

            // Get student details to populate denormalized fields
            var student = await _studentRepository.GetByIdAsync(domainEvent.StudentId, ct);
            
            // Project event to read model
            var readModel = new StudentEnrolmentSummaryReadModel
            {
                Id = domainEvent.AggregateId,
                StudentId = domainEvent.StudentId,
                StudentCode = student?.StudentCode ?? "Unknown",
                StudentFullName = student != null 
                    ? $"{student.FirstName} {student.LastName}" 
                    : "Unknown",
                StreamId = domainEvent.StreamId,
                CohortId = domainEvent.CohortId,
                Status = "Active",
                EnrolledAt = domainEvent.EnrolledAt,
                LastUpdated = domainEvent.OccurredOn,
                Version = domainEvent.Version
            };

            await _enrolmentReadModelRepository.UpsertAsync(readModel, ct);

            _logger.LogInformation(
                "Successfully processed StudentEnrolledEvent for enrolment {EnrolmentId}",
                domainEvent.AggregateId);
        }
        catch (Exception ex)
        {
            _logger.LogError(
                ex,
                "Error processing StudentEnrolledEvent for enrolment {EnrolmentId}",
                domainEvent.AggregateId);
            throw;
        }
    }
}
