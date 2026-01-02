using MediatR;
using SchoolConnect.EducationSystem.Application.DTOs;

namespace SchoolConnect.EducationSystem.Application.Commands;

public record CreateEducationSystemCommand(string CountryId, string Name, string Description) : IRequest<EducationSystemDto>;

public record UpdateEducationSystemCommand(string Id, string Name, string Description) : IRequest<EducationSystemDto>;

public record DeleteEducationSystemCommand(string Id) : IRequest<bool>;
