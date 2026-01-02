using AutoMapper;
using SchoolConnect.Identity.Application.DTOs;
using SchoolConnect.Identity.Domain.Entities;
using SchoolConnect.Identity.Domain.ValueObjects;

namespace SchoolConnect.Identity.Application.Mappers;

public class IdentityMappingProfile : Profile
{
    public IdentityMappingProfile()
    {
        CreateMap<User, UserDto>()
            .ForMember(dest => dest.Status, opt => opt.MapFrom(src => src.Status.ToString()))
            .ForMember(dest => dest.Type, opt => opt.MapFrom(src => src.Type.ToString()));

        CreateMap<UserProfile, UserProfileDto>()
            .ForMember(dest => dest.Gender, opt => opt.MapFrom(src => src.Gender.HasValue ? src.Gender.ToString() : null));

        CreateMap<Address, AddressDto>();

        CreateMap<Role, RoleDto>();

        CreateMap<Permission, PermissionDto>()
            .ForMember(dest => dest.Category, opt => opt.MapFrom(src => src.Category.ToString()));

        CreateMap<Session, SessionDto>();
    }
}
