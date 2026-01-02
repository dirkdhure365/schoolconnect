using AutoMapper;
using SchoolConnect.Institution.Domain.Entities;
using SchoolConnect.Institution.Domain.ValueObjects;
using SchoolConnect.Institution.Application.DTOs;

namespace SchoolConnect.Institution.Application.Mappers;

public class InstitutionMappingProfile : Profile
{
    public InstitutionMappingProfile()
    {
        // Institute mappings
        CreateMap<Institute, InstituteDto>();
        CreateMap<Institute, InstituteSummaryDto>();
        CreateMap<InstituteSettings, InstituteSettingsDto>().ReverseMap();

        // Centre mappings
        CreateMap<Centre, CentreDto>();
        CreateMap<Centre, CentreSummaryDto>();

        // Facility mappings
        CreateMap<Facility, FacilityDto>();
        CreateMap<FacilityBooking, FacilityBookingDto>();

        // Resource mappings
        CreateMap<Resource, ResourceDto>();
        CreateMap<ResourceAllocation, ResourceAllocationDto>();

        // Staff mappings
        CreateMap<StaffMember, StaffMemberDto>();
        CreateMap<StaffMember, StaffSummaryDto>();

        // Team mappings
        CreateMap<Team, TeamDto>();
        CreateMap<TeamMember, TeamMemberDto>();

        // Value Object mappings
        CreateMap<Address, AddressDto>().ReverseMap();
        CreateMap<ContactInfo, ContactInfoDto>().ReverseMap();
        CreateMap<GeoLocation, GeoLocationDto>().ReverseMap();
        CreateMap<WorkingHours, WorkingHoursDto>().ReverseMap();
    }
}
