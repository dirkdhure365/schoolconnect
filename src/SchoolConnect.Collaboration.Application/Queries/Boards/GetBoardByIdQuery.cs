using MediatR;
using SchoolConnect.Collaboration.Application.DTOs;

namespace SchoolConnect.Collaboration.Application.Queries.Boards;

public record GetBoardByIdQuery(Guid Id) : IRequest<BoardDto?>;
