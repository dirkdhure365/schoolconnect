using MediatR;
using SchoolConnect.Enrolment.Application.DTOs;

namespace SchoolConnect.Enrolment.Application.Queries.Students;

public record GetStudentByIdQuery : IRequest<StudentDto?>
{
    public Guid Id { get; init; }
}

public record GetStudentsByInstituteQuery : IRequest<IEnumerable<StudentDto>>
{
    public Guid InstituteId { get; init; }
}
