using AutoMapper;
using MediatR;
using SchoolConnect.LessonDelivery.Application.DTOs;
using SchoolConnect.LessonDelivery.Application.Queries.LessonPlans;
using SchoolConnect.LessonDelivery.Domain.Interfaces;

namespace SchoolConnect.LessonDelivery.Application.Handlers.LessonPlans;

public class GetLessonPlanByIdQueryHandler : IRequestHandler<GetLessonPlanByIdQuery, LessonPlanDto?>
{
    private readonly ILessonPlanRepository _repository;
    private readonly IMapper _mapper;

    public GetLessonPlanByIdQueryHandler(ILessonPlanRepository repository, IMapper mapper)
    {
        _repository = repository;
        _mapper = mapper;
    }

    public async Task<LessonPlanDto?> Handle(GetLessonPlanByIdQuery request, CancellationToken cancellationToken)
    {
        var lessonPlan = await _repository.GetByIdAsync(request.Id, cancellationToken);
        
        return lessonPlan == null ? null : _mapper.Map<LessonPlanDto>(lessonPlan);
    }
}
