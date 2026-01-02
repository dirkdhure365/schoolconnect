using MediatR;
using SchoolConnect.Collaboration.Application.DTOs;

namespace SchoolConnect.Collaboration.Application.Queries.Cards;

public record GetCardByIdQuery(Guid Id) : IRequest<CardDto?>;
