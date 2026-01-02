using MediatR;
using SchoolConnect.EducationSystem.Domain.Events;

namespace SchoolConnect.EducationSystem.Application.Queries;

public record GetAllEventsQuery : IRequest<List<DomainEvent>>;

public record GetEventsByAggregateQuery(string AggregateId) : IRequest<List<DomainEvent>>;
