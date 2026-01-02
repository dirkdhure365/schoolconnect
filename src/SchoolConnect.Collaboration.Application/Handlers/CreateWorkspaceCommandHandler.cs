using MediatR;
using SchoolConnect.Collaboration.Application.Commands.Workspaces;
using SchoolConnect.Collaboration.Domain.Entities;
using SchoolConnect.Collaboration.Domain.Interfaces;

namespace SchoolConnect.Collaboration.Application.Handlers;

public class CreateWorkspaceCommandHandler : IRequestHandler<CreateWorkspaceCommand, Guid>
{
    private readonly IWorkspaceRepository _workspaceRepository;

    public CreateWorkspaceCommandHandler(IWorkspaceRepository workspaceRepository)
    {
        _workspaceRepository = workspaceRepository;
    }

    public async Task<Guid> Handle(CreateWorkspaceCommand request, CancellationToken cancellationToken)
    {
        var workspace = Workspace.Create(
            request.InstituteId,
            request.Name,
            request.OwnerId,
            request.OwnerName,
            request.CentreId,
            request.Description,
            request.LogoUrl,
            request.Color,
            request.Visibility);

        await _workspaceRepository.AddAsync(workspace, cancellationToken);

        return workspace.Id;
    }
}
