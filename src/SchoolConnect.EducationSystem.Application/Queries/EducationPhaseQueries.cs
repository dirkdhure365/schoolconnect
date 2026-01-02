using MediatR;
using SchoolConnect.EducationSystem.Application.DTOs;

namespace SchoolConnect.EducationSystem.Application.Queries;

public record GetAllEducationPhasesQuery : IRequest<List<EducationPhaseDto>>;

public record GetEducationPhaseByIdQuery(string Id) : IRequest<EducationPhaseDto?>;

public record GetEducationPhasesByEducationSystemQuery(string EducationSystemId) : IRequest<List<EducationPhaseDto>>;
