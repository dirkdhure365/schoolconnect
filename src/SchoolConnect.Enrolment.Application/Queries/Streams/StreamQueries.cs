using MediatR;
using SchoolConnect.Enrolment.Application.DTOs;

namespace SchoolConnect.Enrolment.Application.Queries.Streams;

public record GetStreamByIdQuery : IRequest<StreamDto?>
{
    public Guid Id { get; init; }
}

public record GetStreamsByInstituteQuery : IRequest<IEnumerable<StreamDto>>
{
    public Guid InstituteId { get; init; }
}
