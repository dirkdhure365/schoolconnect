using MediatR;
using SchoolConnect.LessonDelivery.Application.Commands.LessonPlans;
using SchoolConnect.LessonDelivery.Domain.Exceptions;
using SchoolConnect.LessonDelivery.Domain.Interfaces;

namespace SchoolConnect.LessonDelivery.Application.Handlers.LessonPlans;

public class UpdateLessonPlanCommandHandler : IRequestHandler<UpdateLessonPlanCommand, Unit>
{
    private readonly ILessonPlanRepository _repository;

    public UpdateLessonPlanCommandHandler(ILessonPlanRepository repository)
    {
        _repository = repository;
    }

    public async Task<Unit> Handle(UpdateLessonPlanCommand request, CancellationToken cancellationToken)
    {
        var lessonPlan = await _repository.GetByIdAsync(request.Id, cancellationToken);
        
        if (lessonPlan == null)
        {
            throw new LessonPlanNotFoundException(request.Id);
        }

        lessonPlan.Update(request.Title, request.Description, request.DurationMinutes);
        
        await _repository.UpdateAsync(lessonPlan, cancellationToken);
        
        return Unit.Value;
    }
}
