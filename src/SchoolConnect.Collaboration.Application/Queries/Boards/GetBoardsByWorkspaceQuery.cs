using MediatR;
using SchoolConnect.Collaboration.Application.DTOs;

namespace SchoolConnect.Collaboration.Application.Queries.Boards;

public record GetBoardsByWorkspaceQuery(Guid WorkspaceId) : IRequest<List<BoardSummaryDto>>;
