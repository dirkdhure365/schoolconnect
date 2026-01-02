using AutoMapper;
using SchoolConnect.Calendar.Domain.Entities;
using SchoolConnect.Calendar.Domain.ValueObjects;
using SchoolConnect.Calendar.Application.DTOs;

namespace SchoolConnect.Calendar.Application.Mappers;

public class CalendarMappingProfile : Profile
{
    public CalendarMappingProfile()
    {
        // CalendarEvent mappings
        CreateMap<CalendarEvent, CalendarEventDto>();
        CreateMap<EventLocation, EventLocationDto>();
        CreateMap<RecurrenceRule, RecurrenceRuleDto>();
        
        // EventAttendee mappings
        CreateMap<EventAttendee, EventAttendeeDto>();
        
        // EventReminder mappings
        CreateMap<EventReminder, EventReminderDto>();
        
        // Timetable mappings
        CreateMap<Timetable, TimetableDto>();
        CreateMap<TimetablePeriod, TimetablePeriodDto>()
            .ForMember(dest => dest.DurationMinutes, 
                opt => opt.MapFrom(src => (int)(src.EndTime - src.StartTime).TotalMinutes));
        CreateMap<TimetableSlot, TimetableSlotDto>();
        CreateMap<TimetableChange, TimetableChangeDto>();
    }
}
