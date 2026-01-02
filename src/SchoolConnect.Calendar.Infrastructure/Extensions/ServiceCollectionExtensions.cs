using Microsoft.Extensions.DependencyInjection;
using SchoolConnect.Calendar.Application.Services;
using SchoolConnect.Calendar.Domain.Interfaces;
using SchoolConnect.Calendar.Infrastructure.Repositories;
using SchoolConnect.Calendar.Infrastructure.Services;
using SchoolConnect.Calendar.Infrastructure.Persistence;

namespace SchoolConnect.Calendar.Infrastructure.Extensions;

public static class ServiceCollectionExtensions
{
    public static IServiceCollection AddCalendarInfrastructure(this IServiceCollection services)
    {
        services.AddScoped<CalendarDbContext>();
        
        services.AddScoped<IEventRepository, EventRepository>();
        services.AddScoped<ITimetableRepository, TimetableRepository>();
        services.AddScoped<ITimetableSlotRepository, TimetableSlotRepository>();
        services.AddScoped<IReminderRepository, ReminderRepository>();
        
        services.AddScoped<IConflictDetectionService, ConflictDetectionService>();
        services.AddScoped<IReminderSchedulerService, ReminderSchedulerService>();
        
        return services;
    }
}
