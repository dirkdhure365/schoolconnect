using MediatR;
using SchoolConnect.Collaboration.Application.DTOs;

namespace SchoolConnect.Collaboration.Application.Queries.Workspaces;

public record GetWorkspaceByIdQuery(Guid Id) : IRequest<WorkspaceDto?>;
