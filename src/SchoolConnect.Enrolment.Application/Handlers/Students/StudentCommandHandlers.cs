using MediatR;
using SchoolConnect.Enrolment.Domain.Entities;
using SchoolConnect.Enrolment.Domain.Interfaces;

namespace SchoolConnect.Enrolment.Application.Handlers.Students;

public class CreateStudentCommandHandler : IRequestHandler<Commands.Students.CreateStudentCommand, Guid>
{
    private readonly IStudentRepository _repository;

    public CreateStudentCommandHandler(IStudentRepository repository)
    {
        _repository = repository;
    }

    public async Task<Guid> Handle(Commands.Students.CreateStudentCommand request, CancellationToken cancellationToken)
    {
        var student = Student.Create(
            request.InstituteId,
            request.StudentCode,
            request.FirstName,
            request.LastName,
            request.DateOfBirth,
            request.Gender,
            request.MiddleName,
            request.Nationality,
            request.IdNumber,
            null, // passport number
            request.Email,
            request.PhoneNumber,
            null, // address
            null, // previous school
            null, // admission number
            request.ApplicationId,
            request.UserId);

        await _repository.AddAsync(student, cancellationToken);
        return student.Id;
    }
}

public class UpdateStudentCommandHandler : IRequestHandler<Commands.Students.UpdateStudentCommand, Unit>
{
    private readonly IStudentRepository _repository;

    public UpdateStudentCommandHandler(IStudentRepository repository)
    {
        _repository = repository;
    }

    public async Task<Unit> Handle(Commands.Students.UpdateStudentCommand request, CancellationToken cancellationToken)
    {
        var student = await _repository.GetByIdAsync(request.Id, cancellationToken)
            ?? throw new Exception($"Student {request.Id} not found");

        student.Update(
            request.FirstName,
            request.LastName,
            request.MiddleName,
            request.Nationality,
            request.IdNumber,
            null, // passport
            request.Email,
            request.PhoneNumber);

        await _repository.UpdateAsync(student, cancellationToken);
        return Unit.Value;
    }
}

public class WithdrawStudentCommandHandler : IRequestHandler<Commands.Students.WithdrawStudentCommand, Unit>
{
    private readonly IStudentRepository _repository;

    public WithdrawStudentCommandHandler(IStudentRepository repository)
    {
        _repository = repository;
    }

    public async Task<Unit> Handle(Commands.Students.WithdrawStudentCommand request, CancellationToken cancellationToken)
    {
        var student = await _repository.GetByIdAsync(request.Id, cancellationToken)
            ?? throw new Exception($"Student {request.Id} not found");

        student.Withdraw(request.Reason);
        await _repository.UpdateAsync(student, cancellationToken);
        return Unit.Value;
    }
}
