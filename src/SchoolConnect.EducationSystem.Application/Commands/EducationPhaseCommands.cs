using MediatR;
using SchoolConnect.EducationSystem.Application.DTOs;

namespace SchoolConnect.EducationSystem.Application.Commands;

public record CreateEducationPhaseCommand(string EducationSystemId, string Name, string Description, int StartAge, int EndAge) : IRequest<EducationPhaseDto>;

public record UpdateEducationPhaseCommand(string Id, string Name, string Description, int StartAge, int EndAge) : IRequest<EducationPhaseDto>;

public record DeleteEducationPhaseCommand(string Id) : IRequest<bool>;
