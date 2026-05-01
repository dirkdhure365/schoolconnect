using Microsoft.Extensions.Logging;
using SchoolConnect.Common.Domain.Interfaces;
using SchoolConnect.Enrolment.Application.ReadModels;
using SchoolConnect.Enrolment.Domain.Events;

namespace SchoolConnect.Enrolment.Application.EventHandlers;

public class StudentWithdrawnEventHandler : IDomainEventHandler<StudentWithdrawnEvent>
{
    private readonly IStudentReadModelRepository _studentReadModelRepository;
    private readonly IStudentEnrolmentSummaryReadModelRepository _enrolmentReadModelRepository;
    private readonly ILogger<StudentWithdrawnEventHandler> _logger;

    public StudentWithdrawnEventHandler(
        IStudentReadModelRepository studentReadModelRepository,
        IStudentEnrolmentSummaryReadModelRepository enrolmentReadModelRepository,
        ILogger<StudentWithdrawnEventHandler> logger)
    {
        _studentReadModelRepository = studentReadModelRepository;
        _enrolmentReadModelRepository = enrolmentReadModelRepository;
        _logger = logger;
    }

    public async Task Handle(StudentWithdrawnEvent domainEvent, CancellationToken ct = default)
    {
        try
        {
            _logger.LogInformation(
                "Processing StudentWithdrawnEvent for student {StudentId}",
                domainEvent.StudentId);

            // Update student read model
            var studentReadModel = await _studentReadModelRepository.GetByIdAsync(domainEvent.StudentId, ct);
            
            if (studentReadModel != null)
            {
                // Check idempotency
                if (studentReadModel.Version >= domainEvent.Version)
                {
                    _logger.LogInformation(
                        "Student read model {StudentId} already has version {Version}, skipping",
                        domainEvent.StudentId,
                        studentReadModel.Version);
                    return;
                }

                studentReadModel.Status = "Withdrawn";
                studentReadModel.WithdrawnAt = domainEvent.WithdrawnAt;
                studentReadModel.WithdrawalReason = domainEvent.Reason;
                studentReadModel.Version = domainEvent.Version;
                studentReadModel.LastUpdated = domainEvent.OccurredOn;

                await _studentReadModelRepository.UpsertAsync(studentReadModel, ct);
            }
            else
            {
                _logger.LogWarning(
                    "Student read model {StudentId} not found, cannot update withdrawal status",
                    domainEvent.StudentId);
            }

            // Update all active enrolments for this student
            var enrolments = await _enrolmentReadModelRepository.GetByStudentIdAsync(domainEvent.StudentId, ct);
            foreach (var enrolment in enrolments.Where(e => e.Status == "Active"))
            {
                enrolment.Status = "Withdrawn";
                enrolment.WithdrawnAt = domainEvent.WithdrawnAt;
                enrolment.WithdrawalReason = domainEvent.Reason;
                enrolment.Version = domainEvent.Version;
                enrolment.LastUpdated = domainEvent.OccurredOn;

                await _enrolmentReadModelRepository.UpsertAsync(enrolment, ct);
            }

            _logger.LogInformation(
                "Successfully processed StudentWithdrawnEvent for student {StudentId}",
                domainEvent.StudentId);
        }
        catch (Exception ex)
        {
            _logger.LogError(
                ex,
                "Error processing StudentWithdrawnEvent for student {StudentId}",
                domainEvent.StudentId);
            throw;
        }
    }
}
