using MediatR;
using SchoolConnect.Collaboration.Application.DTOs;

namespace SchoolConnect.Collaboration.Application.Queries.Lists;

public record GetListsByBoardQuery(Guid BoardId) : IRequest<List<ListDto>>;
