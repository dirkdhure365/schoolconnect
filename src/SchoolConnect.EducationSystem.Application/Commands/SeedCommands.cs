using MediatR;

namespace SchoolConnect.EducationSystem.Application.Commands;

public record SeedDatabaseCommand : IRequest<bool>;
