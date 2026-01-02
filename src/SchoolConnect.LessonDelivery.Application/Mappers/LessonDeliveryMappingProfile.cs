using AutoMapper;
using SchoolConnect.LessonDelivery.Application.DTOs;
using SchoolConnect.LessonDelivery.Domain.Entities;

namespace SchoolConnect.LessonDelivery.Application.Mappers;

public class LessonDeliveryMappingProfile : Profile
{
    public LessonDeliveryMappingProfile()
    {
        CreateMap<LessonPlan, LessonPlanDto>();
        CreateMap<LessonPlan, LessonPlanSummaryDto>();
        
        CreateMap<ScheduledLesson, ScheduledLessonDto>();
        
        CreateMap<LessonSession, LessonSessionDto>();
        
        CreateMap<Attendance, AttendanceDto>();
        
        CreateMap<Homework, HomeworkDto>();
        CreateMap<HomeworkSubmission, HomeworkSubmissionDto>();
        
        CreateMap<CurriculumCoverage, CurriculumCoverageDto>()
            .ForMember(dest => dest.RemainingHours, opt => opt.MapFrom(src => src.RemainingHours))
            .ForMember(dest => dest.ProgressPercentage, opt => opt.MapFrom(src => src.ProgressPercentage));
    }
}
