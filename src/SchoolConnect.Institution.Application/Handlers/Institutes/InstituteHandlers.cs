using MediatR;
using SchoolConnect.Institution.Application.Commands.Institutes;
using SchoolConnect.Institution.Application.Queries.Institutes;
using SchoolConnect.Institution.Domain.DTOs;
using SchoolConnect.Institution.Domain.Entities;
using SchoolConnect.Institution.Domain.Interfaces;
using SchoolConnect.Institution.Domain.ValueObjects;

namespace SchoolConnect.Institution.Application.Handlers.Institutes;

// Command Handlers
public class CreateInstituteHandler : IRequestHandler<CreateInstituteCommand, InstituteDto>
{
    private readonly IInstituteRepository _repository;
    
    public CreateInstituteHandler(IInstituteRepository repository)
    {
        _repository = repository;
    }
    
    public async Task<InstituteDto> Handle(CreateInstituteCommand request, CancellationToken cancellationToken)
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
            request.Description,
            request.SubscriptionId
        );
        
        await _repository.AddAsync(institute);
        
        return MapToDto(institute);
    }
    
    private static InstituteDto MapToDto(Institute institute) => new(
        institute.Id,
        institute.Name,
        institute.Code,
        institute.LogoUrl,
        institute.Description,
        institute.Type,
        institute.Status,
        new ContactInfoDto(institute.ContactInfo.Email, institute.ContactInfo.Phone, institute.ContactInfo.Website),
        new AddressDto(institute.Address.Street, institute.Address.City, institute.Address.State, institute.Address.PostalCode, institute.Address.Country),
        institute.Country,
        institute.Timezone,
        institute.AcademicYearStartMonth,
        new InstituteSettingsDto(
            institute.Settings.DefaultLanguage,
            institute.Settings.DateFormat,
            institute.Settings.TimeFormat,
            institute.Settings.Currency,
            institute.Settings.EnabledFeatures
        ),
        institute.SubscriptionId,
        institute.CreatedAt,
        institute.UpdatedAt
    );
}

public class UpdateInstituteHandler : IRequestHandler<UpdateInstituteCommand, InstituteDto>
{
    private readonly IInstituteRepository _repository;
    
    public UpdateInstituteHandler(IInstituteRepository repository)
    {
        _repository = repository;
    }
    
    public async Task<InstituteDto> Handle(UpdateInstituteCommand request, CancellationToken cancellationToken)
    {
        var institute = await _repository.GetByIdAsync(request.Id);
        if (institute == null) throw new Exception($"Institute not found: {request.Id}");
        
        var contactInfo = new ContactInfo(request.Email, request.Phone, request.Website);
        var address = new Address(request.Street, request.City, request.State, request.PostalCode, request.Country);
        
        institute.Update(
            request.Name,
            request.Description,
            contactInfo,
            address,
            request.Country,
            request.Timezone,
            request.AcademicYearStartMonth
        );
        
        await _repository.UpdateAsync(institute);
        
        return MapToDto(institute);
    }
    
    private static InstituteDto MapToDto(Institute institute) => new(
        institute.Id,
        institute.Name,
        institute.Code,
        institute.LogoUrl,
        institute.Description,
        institute.Type,
        institute.Status,
        new ContactInfoDto(institute.ContactInfo.Email, institute.ContactInfo.Phone, institute.ContactInfo.Website),
        new AddressDto(institute.Address.Street, institute.Address.City, institute.Address.State, institute.Address.PostalCode, institute.Address.Country),
        institute.Country,
        institute.Timezone,
        institute.AcademicYearStartMonth,
        new InstituteSettingsDto(
            institute.Settings.DefaultLanguage,
            institute.Settings.DateFormat,
            institute.Settings.TimeFormat,
            institute.Settings.Currency,
            institute.Settings.EnabledFeatures
        ),
        institute.SubscriptionId,
        institute.CreatedAt,
        institute.UpdatedAt
    );
}

// Query Handlers
public class GetInstituteByIdHandler : IRequestHandler<GetInstituteByIdQuery, InstituteDto?>
{
    private readonly IInstituteRepository _repository;
    
    public GetInstituteByIdHandler(IInstituteRepository repository)
    {
        _repository = repository;
    }
    
    public async Task<InstituteDto?> Handle(GetInstituteByIdQuery request, CancellationToken cancellationToken)
    {
        var institute = await _repository.GetByIdAsync(request.Id);
        if (institute == null) return null;
        
        return MapToDto(institute);
    }
    
    private static InstituteDto MapToDto(Institute institute) => new(
        institute.Id,
        institute.Name,
        institute.Code,
        institute.LogoUrl,
        institute.Description,
        institute.Type,
        institute.Status,
        new ContactInfoDto(institute.ContactInfo.Email, institute.ContactInfo.Phone, institute.ContactInfo.Website),
        new AddressDto(institute.Address.Street, institute.Address.City, institute.Address.State, institute.Address.PostalCode, institute.Address.Country),
        institute.Country,
        institute.Timezone,
        institute.AcademicYearStartMonth,
        new InstituteSettingsDto(
            institute.Settings.DefaultLanguage,
            institute.Settings.DateFormat,
            institute.Settings.TimeFormat,
            institute.Settings.Currency,
            institute.Settings.EnabledFeatures
        ),
        institute.SubscriptionId,
        institute.CreatedAt,
        institute.UpdatedAt
    );
}

public class GetInstitutesHandler : IRequestHandler<GetInstitutesQuery, IEnumerable<InstituteDto>>
{
    private readonly IInstituteRepository _repository;
    
    public GetInstitutesHandler(IInstituteRepository repository)
    {
        _repository = repository;
    }
    
    public async Task<IEnumerable<InstituteDto>> Handle(GetInstitutesQuery request, CancellationToken cancellationToken)
    {
        var institutes = await _repository.GetAllAsync();
        return institutes.Select(MapToDto);
    }
    
    private static InstituteDto MapToDto(Institute institute) => new(
        institute.Id,
        institute.Name,
        institute.Code,
        institute.LogoUrl,
        institute.Description,
        institute.Type,
        institute.Status,
        new ContactInfoDto(institute.ContactInfo.Email, institute.ContactInfo.Phone, institute.ContactInfo.Website),
        new AddressDto(institute.Address.Street, institute.Address.City, institute.Address.State, institute.Address.PostalCode, institute.Address.Country),
        institute.Country,
        institute.Timezone,
        institute.AcademicYearStartMonth,
        new InstituteSettingsDto(
            institute.Settings.DefaultLanguage,
            institute.Settings.DateFormat,
            institute.Settings.TimeFormat,
            institute.Settings.Currency,
            institute.Settings.EnabledFeatures
        ),
        institute.SubscriptionId,
        institute.CreatedAt,
        institute.UpdatedAt
    );
}
