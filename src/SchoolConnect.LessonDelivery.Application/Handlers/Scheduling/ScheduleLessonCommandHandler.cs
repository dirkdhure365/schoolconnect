using MediatR;
using SchoolConnect.LessonDelivery.Application.Commands.Scheduling;
using SchoolConnect.LessonDelivery.Domain.Entities;
using SchoolConnect.LessonDelivery.Domain.Interfaces;

namespace SchoolConnect.LessonDelivery.Application.Handlers.Scheduling;

public class ScheduleLessonCommandHandler : IRequestHandler<ScheduleLessonCommand, Guid>
{
    private readonly IScheduledLessonRepository _repository;

    public ScheduleLessonCommandHandler(IScheduledLessonRepository repository)
    {
        _repository = repository;
    }

    public async Task<Guid> Handle(ScheduleLessonCommand request, CancellationToken cancellationToken)
    {
        var lesson = ScheduledLesson.Create(
            request.ClassId,
            request.ClassName,
            request.SubjectId,
            request.SubjectName,
            request.TeacherId,
            request.TeacherName,
            request.ScheduledStart,
            request.ScheduledEnd,
            request.LessonPlanId,
            request.FacilityId,
            request.FacilityName,
            request.Title
        );

        await _repository.AddAsync(lesson, cancellationToken);
        
        return lesson.Id;
    }
}
