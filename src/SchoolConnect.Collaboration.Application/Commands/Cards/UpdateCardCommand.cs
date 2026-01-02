using MediatR;
using SchoolConnect.Collaboration.Domain.Enums;

namespace SchoolConnect.Collaboration.Application.Commands.Cards;

public record UpdateCardCommand(
    Guid Id,
    string? Title = null,
    string? Description = null,
    CardPriority? Priority = null
) : IRequest<Unit>;
