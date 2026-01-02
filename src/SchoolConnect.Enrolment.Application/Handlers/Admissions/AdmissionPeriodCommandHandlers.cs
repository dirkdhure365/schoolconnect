using MediatR;
using SchoolConnect.Enrolment.Domain.Entities;
using SchoolConnect.Enrolment.Domain.Interfaces;

namespace SchoolConnect.Enrolment.Application.Handlers.Admissions;

public class CreateAdmissionPeriodCommandHandler : IRequestHandler<Commands.Admissions.CreateAdmissionPeriodCommand, Guid>
{
    private readonly IAdmissionPeriodRepository _repository;

    public CreateAdmissionPeriodCommandHandler(IAdmissionPeriodRepository repository)
    {
        _repository = repository;
    }

    public async Task<Guid> Handle(Commands.Admissions.CreateAdmissionPeriodCommand request, CancellationToken cancellationToken)
    {
        var period = AdmissionPeriod.Create(
            request.InstituteId,
            request.Name,
            request.AcademicYear,
            request.ApplicationStartDate,
            request.ApplicationEndDate,
            request.ProgramOfferingIds,
            request.Description,
            request.MaxApplications,
            request.RequiredDocuments,
            request.ApplicationFee,
            request.Currency);

        await _repository.AddAsync(period, cancellationToken);
        return period.Id;
    }
}

public class OpenAdmissionPeriodCommandHandler : IRequestHandler<Commands.Admissions.OpenAdmissionPeriodCommand, Unit>
{
    private readonly IAdmissionPeriodRepository _repository;

    public OpenAdmissionPeriodCommandHandler(IAdmissionPeriodRepository repository)
    {
        _repository = repository;
    }

    public async Task<Unit> Handle(Commands.Admissions.OpenAdmissionPeriodCommand request, CancellationToken cancellationToken)
    {
        var period = await _repository.GetByIdAsync(request.Id, cancellationToken)
            ?? throw new Exception($"Admission period {request.Id} not found");

        period.Open();
        await _repository.UpdateAsync(period, cancellationToken);
        return Unit.Value;
    }
}

public class CloseAdmissionPeriodCommandHandler : IRequestHandler<Commands.Admissions.CloseAdmissionPeriodCommand, Unit>
{
    private readonly IAdmissionPeriodRepository _repository;

    public CloseAdmissionPeriodCommandHandler(IAdmissionPeriodRepository repository)
    {
        _repository = repository;
    }

    public async Task<Unit> Handle(Commands.Admissions.CloseAdmissionPeriodCommand request, CancellationToken cancellationToken)
    {
        var period = await _repository.GetByIdAsync(request.Id, cancellationToken)
            ?? throw new Exception($"Admission period {request.Id} not found");

        period.Close();
        await _repository.UpdateAsync(period, cancellationToken);
        return Unit.Value;
    }
}
