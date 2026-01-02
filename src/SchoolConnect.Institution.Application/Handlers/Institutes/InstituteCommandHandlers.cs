using MediatR;
using SchoolConnect.Institution.Domain.Entities;
using SchoolConnect.Institution.Domain.Enums;
using SchoolConnect.Institution.Domain.Interfaces;
using SchoolConnect.Institution.Domain.ValueObjects;
using SchoolConnect.Institution.Domain.Exceptions;

namespace SchoolConnect.Institution.Application.Commands.Institutes;

public class CreateInstituteCommandHandler : IRequestHandler<CreateInstituteCommand, Guid>
{
    private readonly IInstituteRepository _repository;

    public CreateInstituteCommandHandler(IInstituteRepository repository)
    {
        _repository = repository;
    }

    public async Task<Guid> Handle(CreateInstituteCommand request, CancellationToken cancellationToken)
    {
        var contactInfo = new ContactInfo(request.Email, request.Phone, request.Website);
        var address = new Address(request.Street, request.City, request.State, request.PostalCode, request.Country);

        var institute = Institute.Create(
            request.Name,
            request.Code,
            request.Type,
            contactInfo,
            address,
            request.Country,
            request.Timezone,
            request.AcademicYearStartMonth,
            request.Description);

        await _repository.AddAsync(institute, cancellationToken);
        return institute.Id;
    }
}

public class UpdateInstituteCommandHandler : IRequestHandler<UpdateInstituteCommand, Unit>
{
    private readonly IInstituteRepository _repository;

    public UpdateInstituteCommandHandler(IInstituteRepository repository)
    {
        _repository = repository;
    }

    public async Task<Unit> Handle(UpdateInstituteCommand request, CancellationToken cancellationToken)
    {
        var institute = await _repository.GetByIdAsync(request.Id, cancellationToken)
            ?? throw new InstituteNotFoundException(request.Id);

        var contactInfo = new ContactInfo(request.Email, request.Phone, request.Website);
        var address = new Address(request.Street, request.City, request.State, request.PostalCode, request.Country);

        institute.Update(
            request.Name,
            request.Description,
            contactInfo,
            address,
            request.Country,
            request.Timezone,
            request.AcademicYearStartMonth);

        await _repository.UpdateAsync(institute, cancellationToken);
        return Unit.Value;
    }
}

public class DeactivateInstituteCommandHandler : IRequestHandler<DeactivateInstituteCommand, Unit>
{
    private readonly IInstituteRepository _repository;

    public DeactivateInstituteCommandHandler(IInstituteRepository repository)
    {
        _repository = repository;
    }

    public async Task<Unit> Handle(DeactivateInstituteCommand request, CancellationToken cancellationToken)
    {
        var institute = await _repository.GetByIdAsync(request.Id, cancellationToken)
            ?? throw new InstituteNotFoundException(request.Id);

        institute.Deactivate();
        await _repository.UpdateAsync(institute, cancellationToken);
        return Unit.Value;
    }
}

public class UpdateInstituteSettingsCommandHandler : IRequestHandler<UpdateInstituteSettingsCommand, Unit>
{
    private readonly IInstituteRepository _repository;

    public UpdateInstituteSettingsCommandHandler(IInstituteRepository repository)
    {
        _repository = repository;
    }

    public async Task<Unit> Handle(UpdateInstituteSettingsCommand request, CancellationToken cancellationToken)
    {
        var institute = await _repository.GetByIdAsync(request.Id, cancellationToken)
            ?? throw new InstituteNotFoundException(request.Id);

        var settings = new InstituteSettings
        {
            DefaultLanguage = request.DefaultLanguage,
            DateFormat = request.DateFormat,
            TimeFormat = request.TimeFormat,
            Currency = request.Currency,
            EnabledFeatures = request.EnabledFeatures
        };

        institute.UpdateSettings(settings);
        await _repository.UpdateAsync(institute, cancellationToken);
        return Unit.Value;
    }
}

public class UploadInstituteLogoCommandHandler : IRequestHandler<UploadInstituteLogoCommand, Unit>
{
    private readonly IInstituteRepository _repository;

    public UploadInstituteLogoCommandHandler(IInstituteRepository repository)
    {
        _repository = repository;
    }

    public async Task<Unit> Handle(UploadInstituteLogoCommand request, CancellationToken cancellationToken)
    {
        var institute = await _repository.GetByIdAsync(request.Id, cancellationToken)
            ?? throw new InstituteNotFoundException(request.Id);

        institute.UploadLogo(request.LogoUrl);
        await _repository.UpdateAsync(institute, cancellationToken);
        return Unit.Value;
    }
}
