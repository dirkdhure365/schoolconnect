using MediatR;
using SchoolConnect.Collaboration.Application.DTOs;

namespace SchoolConnect.Collaboration.Application.Queries.Cards;

public record GetCardsByListQuery(Guid ListId) : IRequest<List<CardDto>>;
