using MediatR;

namespace SchoolConnect.Collaboration.Application.Commands.Lists;

public record UpdateListCommand(
    Guid Id,
    string? Name = null,
    string? Color = null,
    int? WipLimit = null
) : IRequest<Unit>;
