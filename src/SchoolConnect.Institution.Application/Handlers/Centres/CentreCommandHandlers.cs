using MediatR;
using SchoolConnect.Institution.Domain.Entities;
using SchoolConnect.Institution.Domain.Exceptions;
using SchoolConnect.Institution.Domain.Interfaces;
using SchoolConnect.Institution.Domain.ValueObjects;

namespace SchoolConnect.Institution.Application.Commands.Centres;

public class CreateCentreCommandHandler : IRequestHandler<CreateCentreCommand, Guid>
{
    private readonly ICentreRepository _repository;

    public CreateCentreCommandHandler(ICentreRepository repository)
    {
        _repository = repository;
    }

    public async Task<Guid> Handle(CreateCentreCommand request, CancellationToken cancellationToken)
    {
        var contactInfo = new ContactInfo(request.Email, request.Phone, request.Website);
        var address = new Address(request.Street, request.City, request.State, request.PostalCode, request.Country);
        
        GeoLocation? location = null;
        if (request.Latitude.HasValue && request.Longitude.HasValue)
        {
            location = new GeoLocation(request.Latitude.Value, request.Longitude.Value);
        }

        var centre = Centre.Create(
            request.InstituteId,
            request.Name,
            request.Code,
            address,
            contactInfo,
            request.Capacity,
            null, // Working hours - can be added later
            location,
            request.CentreAdminId);

        await _repository.AddAsync(centre, cancellationToken);
        return centre.Id;
    }
}

public class UpdateCentreCommandHandler : IRequestHandler<UpdateCentreCommand, Unit>
{
    private readonly ICentreRepository _repository;

    public UpdateCentreCommandHandler(ICentreRepository repository)
    {
        _repository = repository;
    }

    public async Task<Unit> Handle(UpdateCentreCommand request, CancellationToken cancellationToken)
    {
        var centre = await _repository.GetByIdAsync(request.Id, cancellationToken)
            ?? throw new CentreNotFoundException(request.Id);

        var contactInfo = new ContactInfo(request.Email, request.Phone, request.Website);
        var address = new Address(request.Street, request.City, request.State, request.PostalCode, request.Country);
        
        GeoLocation? location = null;
        if (request.Latitude.HasValue && request.Longitude.HasValue)
        {
            location = new GeoLocation(request.Latitude.Value, request.Longitude.Value);
        }

        centre.Update(
            request.Name,
            address,
            contactInfo,
            request.Capacity,
            null, // Working hours
            location,
            request.CentreAdminId);

        await _repository.UpdateAsync(centre, cancellationToken);
        return Unit.Value;
    }
}

public class DeactivateCentreCommandHandler : IRequestHandler<DeactivateCentreCommand, Unit>
{
    private readonly ICentreRepository _repository;

    public DeactivateCentreCommandHandler(ICentreRepository repository)
    {
        _repository = repository;
    }

    public async Task<Unit> Handle(DeactivateCentreCommand request, CancellationToken cancellationToken)
    {
        var centre = await _repository.GetByIdAsync(request.Id, cancellationToken)
            ?? throw new CentreNotFoundException(request.Id);

        centre.Deactivate();
        await _repository.UpdateAsync(centre, cancellationToken);
        return Unit.Value;
    }
}
