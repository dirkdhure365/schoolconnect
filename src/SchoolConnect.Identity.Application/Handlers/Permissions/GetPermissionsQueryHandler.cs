using AutoMapper;
using MediatR;
using SchoolConnect.Identity.Application.DTOs;
using SchoolConnect.Identity.Application.Queries.Permissions;
using SchoolConnect.Identity.Domain.Interfaces;

namespace SchoolConnect.Identity.Application.Handlers.Permissions;

public class GetPermissionsQueryHandler : IRequestHandler<GetPermissionsQuery, List<PermissionDto>>
{
    private readonly IPermissionRepository _permissionRepository;
    private readonly IMapper _mapper;

    public GetPermissionsQueryHandler(IPermissionRepository permissionRepository, IMapper mapper)
    {
        _permissionRepository = permissionRepository;
        _mapper = mapper;
    }

    public async Task<List<PermissionDto>> Handle(GetPermissionsQuery request, CancellationToken cancellationToken)
    {
        var permissions = await _permissionRepository.GetAllAsync(cancellationToken);
        return _mapper.Map<List<PermissionDto>>(permissions);
    }
}
