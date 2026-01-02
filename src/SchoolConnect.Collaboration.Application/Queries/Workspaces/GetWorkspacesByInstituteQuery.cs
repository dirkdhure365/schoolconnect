using MediatR;
using SchoolConnect.Collaboration.Application.DTOs;

namespace SchoolConnect.Collaboration.Application.Queries.Workspaces;

public record GetWorkspacesByInstituteQuery(Guid InstituteId) : IRequest<List<WorkspaceSummaryDto>>;
