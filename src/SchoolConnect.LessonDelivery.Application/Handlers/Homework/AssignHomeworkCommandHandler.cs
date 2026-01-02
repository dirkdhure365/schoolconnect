using MediatR;
using SchoolConnect.LessonDelivery.Application.Commands.Homework;
using SchoolConnect.LessonDelivery.Domain.Entities;
using SchoolConnect.LessonDelivery.Domain.Interfaces;

namespace SchoolConnect.LessonDelivery.Application.Handlers.Homework;

public class AssignHomeworkCommandHandler : IRequestHandler<AssignHomeworkCommand, Guid>
{
    private readonly IHomeworkRepository _repository;

    public AssignHomeworkCommandHandler(IHomeworkRepository repository)
    {
        _repository = repository;
    }

    public async Task<Guid> Handle(AssignHomeworkCommand request, CancellationToken cancellationToken)
    {
        var homework = Domain.Entities.Homework.Create(
            request.ClassId,
            request.ClassName,
            request.SubjectId,
            request.SubjectName,
            request.TeacherId,
            request.TeacherName,
            request.Title,
            request.DueDate,
            request.LessonSessionId,
            request.Description,
            request.Instructions,
            request.MaxMarks,
            request.AllowLateSubmission
        );

        await _repository.AddAsync(homework, cancellationToken);
        
        return homework.Id;
    }
}
