using AutoMapper;
using MediatR;
using SchoolConnect.Identity.Application.DTOs;
using SchoolConnect.Identity.Application.Queries.Roles;
using SchoolConnect.Identity.Domain.Interfaces;

namespace SchoolConnect.Identity.Application.Handlers.Roles;

public class GetRolesQueryHandler : IRequestHandler<GetRolesQuery, List<RoleDto>>
{
    private readonly IRoleRepository _roleRepository;
    private readonly IMapper _mapper;

    public GetRolesQueryHandler(IRoleRepository roleRepository, IMapper mapper)
    {
        _roleRepository = roleRepository;
        _mapper = mapper;
    }

    public async Task<List<RoleDto>> Handle(GetRolesQuery request, CancellationToken cancellationToken)
    {
        var roles = await _roleRepository.GetAllAsync(cancellationToken);
        return _mapper.Map<List<RoleDto>>(roles);
    }
}
