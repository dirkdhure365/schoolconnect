using MediatR;
using SchoolConnect.LessonDelivery.Application.Commands.LessonPlans;
using SchoolConnect.LessonDelivery.Domain.Entities;
using SchoolConnect.LessonDelivery.Domain.Interfaces;

namespace SchoolConnect.LessonDelivery.Application.Handlers.LessonPlans;

public class CreateLessonPlanCommandHandler : IRequestHandler<CreateLessonPlanCommand, Guid>
{
    private readonly ILessonPlanRepository _repository;

    public CreateLessonPlanCommandHandler(ILessonPlanRepository repository)
    {
        _repository = repository;
    }

    public async Task<Guid> Handle(CreateLessonPlanCommand request, CancellationToken cancellationToken)
    {
        var lessonPlan = LessonPlan.Create(
            request.ClassId,
            request.ClassName,
            request.SubjectId,
            request.SubjectName,
            request.TeacherId,
            request.TeacherName,
            request.Title,
            request.DurationMinutes,
            request.Description
        );

        await _repository.AddAsync(lessonPlan, cancellationToken);
        
        return lessonPlan.Id;
    }
}
