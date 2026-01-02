using MediatR;
using SchoolConnect.LessonDelivery.Application.Commands.Sessions;
using SchoolConnect.LessonDelivery.Domain.Entities;
using SchoolConnect.LessonDelivery.Domain.Interfaces;

namespace SchoolConnect.LessonDelivery.Application.Handlers.Sessions;

public class StartLessonCommandHandler : IRequestHandler<StartLessonCommand, Guid>
{
    private readonly ILessonSessionRepository _sessionRepository;
    private readonly IScheduledLessonRepository _scheduledLessonRepository;

    public StartLessonCommandHandler(
        ILessonSessionRepository sessionRepository,
        IScheduledLessonRepository scheduledLessonRepository)
    {
        _sessionRepository = sessionRepository;
        _scheduledLessonRepository = scheduledLessonRepository;
    }

    public async Task<Guid> Handle(StartLessonCommand request, CancellationToken cancellationToken)
    {
        var session = LessonSession.Create(
            request.ScheduledLessonId,
            request.ClassId,
            request.TeacherId,
            request.LessonPlanId
        );

        await _sessionRepository.AddAsync(session, cancellationToken);
        
        // Update scheduled lesson status
        var scheduledLesson = await _scheduledLessonRepository.GetByIdAsync(request.ScheduledLessonId, cancellationToken);
        if (scheduledLesson != null)
        {
            scheduledLesson.MarkAsInProgress();
            await _scheduledLessonRepository.UpdateAsync(scheduledLesson, cancellationToken);
        }
        
        return session.Id;
    }
}
