using MediatR;
using SchoolConnect.Institution.Application.Commands.Centres;
using SchoolConnect.Institution.Application.Queries.Centres;
using SchoolConnect.Institution.Domain.DTOs;
using SchoolConnect.Institution.Domain.Entities;
using SchoolConnect.Institution.Domain.Interfaces;
using SchoolConnect.Institution.Domain.ValueObjects;

namespace SchoolConnect.Institution.Application.Handlers.Centres;

// Command Handlers
public class CreateCentreHandler : IRequestHandler<CreateCentreCommand, CentreDto>
{
    private readonly ICentreRepository _repository;
    
    public CreateCentreHandler(ICentreRepository repository)
    {
        _repository = repository;
    }
    
    public async Task<CentreDto> Handle(CreateCentreCommand request, CancellationToken cancellationToken)
    {
        var contactInfo = new ContactInfo(request.Email, request.Phone, request.Website);
        var address = new Address(request.Street, request.City, request.State, request.PostalCode, request.Country);
        var workingHours = new WorkingHours(request.StartTime, request.EndTime, request.WorkingDays);
        
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
            workingHours,
            location,
            request.CentreAdminId
        );
        
        await _repository.AddAsync(centre);
        
        return MapToDto(centre);
    }
    
    private static CentreDto MapToDto(Centre centre) => new(
        centre.Id,
        centre.InstituteId,
        centre.Name,
        centre.Code,
        new AddressDto(centre.Address.Street, centre.Address.City, centre.Address.State, centre.Address.PostalCode, centre.Address.Country),
        new ContactInfoDto(centre.ContactInfo.Email, centre.ContactInfo.Phone, centre.ContactInfo.Website),
        centre.Location != null ? new GeoLocationDto(centre.Location.Latitude, centre.Location.Longitude) : null,
        centre.Capacity,
        centre.Status,
        new WorkingHoursDto(centre.WorkingHours.StartTime, centre.WorkingHours.EndTime, centre.WorkingHours.WorkingDays),
        centre.CentreAdminId,
        centre.CreatedAt,
        centre.UpdatedAt
    );
}

public class UpdateCentreHandler : IRequestHandler<UpdateCentreCommand, CentreDto>
{
    private readonly ICentreRepository _repository;
    
    public UpdateCentreHandler(ICentreRepository repository)
    {
        _repository = repository;
    }
    
    public async Task<CentreDto> Handle(UpdateCentreCommand request, CancellationToken cancellationToken)
    {
        var centre = await _repository.GetByIdAsync(request.Id);
        if (centre == null) throw new Exception($"Centre not found: {request.Id}");
        
        var contactInfo = new ContactInfo(request.Email, request.Phone, request.Website);
        var address = new Address(request.Street, request.City, request.State, request.PostalCode, request.Country);
        var workingHours = new WorkingHours(request.StartTime, request.EndTime, request.WorkingDays);
        
        GeoLocation? location = null;
        if (request.Latitude.HasValue && request.Longitude.HasValue)
        {
            location = new GeoLocation(request.Latitude.Value, request.Longitude.Value);
        }
        
        centre.Update(request.Name, address, contactInfo, request.Capacity, workingHours, location, request.CentreAdminId);
        
        await _repository.UpdateAsync(centre);
        
        return MapToDto(centre);
    }
    
    private static CentreDto MapToDto(Centre centre) => new(
        centre.Id,
        centre.InstituteId,
        centre.Name,
        centre.Code,
        new AddressDto(centre.Address.Street, centre.Address.City, centre.Address.State, centre.Address.PostalCode, centre.Address.Country),
        new ContactInfoDto(centre.ContactInfo.Email, centre.ContactInfo.Phone, centre.ContactInfo.Website),
        centre.Location != null ? new GeoLocationDto(centre.Location.Latitude, centre.Location.Longitude) : null,
        centre.Capacity,
        centre.Status,
        new WorkingHoursDto(centre.WorkingHours.StartTime, centre.WorkingHours.EndTime, centre.WorkingHours.WorkingDays),
        centre.CentreAdminId,
        centre.CreatedAt,
        centre.UpdatedAt
    );
}

public class DeactivateCentreHandler : IRequestHandler<DeactivateCentreCommand, bool>
{
    private readonly ICentreRepository _repository;
    
    public DeactivateCentreHandler(ICentreRepository repository)
    {
        _repository = repository;
    }
    
    public async Task<bool> Handle(DeactivateCentreCommand request, CancellationToken cancellationToken)
    {
        var centre = await _repository.GetByIdAsync(request.Id);
        if (centre == null) return false;
        
        centre.Deactivate();
        await _repository.UpdateAsync(centre);
        
        return true;
    }
}

// Query Handlers
public class GetCentreByIdHandler : IRequestHandler<GetCentreByIdQuery, CentreDto?>
{
    private readonly ICentreRepository _repository;
    
    public GetCentreByIdHandler(ICentreRepository repository)
    {
        _repository = repository;
    }
    
    public async Task<CentreDto?> Handle(GetCentreByIdQuery request, CancellationToken cancellationToken)
    {
        var centre = await _repository.GetByIdAsync(request.Id);
        if (centre == null) return null;
        
        return MapToDto(centre);
    }
    
    private static CentreDto MapToDto(Centre centre) => new(
        centre.Id,
        centre.InstituteId,
        centre.Name,
        centre.Code,
        new AddressDto(centre.Address.Street, centre.Address.City, centre.Address.State, centre.Address.PostalCode, centre.Address.Country),
        new ContactInfoDto(centre.ContactInfo.Email, centre.ContactInfo.Phone, centre.ContactInfo.Website),
        centre.Location != null ? new GeoLocationDto(centre.Location.Latitude, centre.Location.Longitude) : null,
        centre.Capacity,
        centre.Status,
        new WorkingHoursDto(centre.WorkingHours.StartTime, centre.WorkingHours.EndTime, centre.WorkingHours.WorkingDays),
        centre.CentreAdminId,
        centre.CreatedAt,
        centre.UpdatedAt
    );
}

public class GetCentresByInstituteHandler : IRequestHandler<GetCentresByInstituteQuery, IEnumerable<CentreDto>>
{
    private readonly ICentreRepository _repository;
    
    public GetCentresByInstituteHandler(ICentreRepository repository)
    {
        _repository = repository;
    }
    
    public async Task<IEnumerable<CentreDto>> Handle(GetCentresByInstituteQuery request, CancellationToken cancellationToken)
    {
        var centres = await _repository.GetByInstituteIdAsync(request.InstituteId);
        return centres.Select(MapToDto);
    }
    
    private static CentreDto MapToDto(Centre centre) => new(
        centre.Id,
        centre.InstituteId,
        centre.Name,
        centre.Code,
        new AddressDto(centre.Address.Street, centre.Address.City, centre.Address.State, centre.Address.PostalCode, centre.Address.Country),
        new ContactInfoDto(centre.ContactInfo.Email, centre.ContactInfo.Phone, centre.ContactInfo.Website),
        centre.Location != null ? new GeoLocationDto(centre.Location.Latitude, centre.Location.Longitude) : null,
        centre.Capacity,
        centre.Status,
        new WorkingHoursDto(centre.WorkingHours.StartTime, centre.WorkingHours.EndTime, centre.WorkingHours.WorkingDays),
        centre.CentreAdminId,
        centre.CreatedAt,
        centre.UpdatedAt
    );
}

public class GetCentreDashboardHandler : IRequestHandler<GetCentreDashboardQuery, CentreDashboardDto?>
{
    private readonly ICentreRepository _centreRepository;
    private readonly IFacilityRepository _facilityRepository;
    private readonly IResourceRepository _resourceRepository;
    private readonly IStaffCentreAssignmentRepository _staffAssignmentRepository;
    
    public GetCentreDashboardHandler(
        ICentreRepository centreRepository,
        IFacilityRepository facilityRepository,
        IResourceRepository resourceRepository,
        IStaffCentreAssignmentRepository staffAssignmentRepository)
    {
        _centreRepository = centreRepository;
        _facilityRepository = facilityRepository;
        _resourceRepository = resourceRepository;
        _staffAssignmentRepository = staffAssignmentRepository;
    }
    
    public async Task<CentreDashboardDto?> Handle(GetCentreDashboardQuery request, CancellationToken cancellationToken)
    {
        var centre = await _centreRepository.GetByIdAsync(request.CentreId);
        if (centre == null) return null;
        
        var facilities = await _facilityRepository.GetByCentreIdAsync(request.CentreId);
        var resources = await _resourceRepository.GetByCentreIdAsync(request.CentreId);
        var staffAssignments = await _staffAssignmentRepository.GetByCentreIdAsync(request.CentreId);
        
        var availableFacilities = facilities.Count(f => f.Status == Domain.Enums.FacilityStatus.Available);
        var availableResources = resources.Count(r => r.Status == Domain.Enums.ResourceStatus.Available);
        
        return new CentreDashboardDto(
            centre.Id,
            centre.Name,
            facilities.Count(),
            availableFacilities,
            resources.Count(),
            availableResources,
            staffAssignments.Count(sa => sa.EndDate == null)
        );
    }
}
