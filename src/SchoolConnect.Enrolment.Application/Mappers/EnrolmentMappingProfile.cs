using AutoMapper;
using SchoolConnect.Enrolment.Application.DTOs;
using SchoolConnect.Enrolment.Domain.Entities;

namespace SchoolConnect.Enrolment.Application.Mappers;

public class EnrolmentMappingProfile : Profile
{
    public EnrolmentMappingProfile()
    {
        CreateMap<AdmissionPeriod, AdmissionPeriodDto>();
        CreateMap<Student, StudentDto>();
        CreateMap<Student, StudentSummaryDto>()
            .ForMember(dest => dest.Status, opt => opt.MapFrom(src => src.Status.ToString()));
        CreateMap<Domain.Entities.Stream, StreamDto>();
        CreateMap<Cohort, CohortDto>();
        CreateMap<Class, ClassDto>();
    }
}
