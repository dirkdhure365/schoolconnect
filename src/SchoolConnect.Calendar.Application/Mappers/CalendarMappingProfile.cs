using AutoMapper;
using SchoolConnect.Calendar.Application.DTOs;
using SchoolConnect.Calendar.Domain.Entities;
using SchoolConnect.Calendar.Domain.ValueObjects;

namespace SchoolConnect.Calendar.Application.Mappers;

public class CalendarMappingProfile : Profile
{
    public CalendarMappingProfile()
    {
        CreateMap<CalendarEvent, CalendarEventDto>()
            .ForMember(dest => dest.CreatedBy, opt => opt.MapFrom(src => src.CreatedByUserId));

        CreateMap<EventAttendee, EventAttendeeDto>()
            .ForMember(dest => dest.Id, opt => opt.MapFrom(src => src.Id));

        CreateMap<EventReminder, EventReminderDto>();

        CreateMap<Timetable, TimetableDto>()
            .ForMember(dest => dest.CreatedBy, opt => opt.MapFrom(src => src.CreatedByUserId));

        CreateMap<TimetablePeriod, TimetablePeriodDto>();

        CreateMap<TimetableSlot, TimetableSlotDto>();

        CreateMap<TimetableChange, TimetableChangeDto>()
            .ForMember(dest => dest.CreatedBy, opt => opt.MapFrom(src => src.CreatedByUserId));

        CreateMap<EventLocation, EventLocationDto>().ReverseMap();
        CreateMap<RecurrenceRule, RecurrenceRuleDto>().ReverseMap();
        CreateMap<TimetableSettings, TimetableSettingsDto>().ReverseMap();
    }
}
