using AutoMapper;
using MediatR;
using SchoolConnect.Common.Application.Models;
using SchoolConnect.Identity.Application.Commands.Roles;
using SchoolConnect.Identity.Application.DTOs;
using SchoolConnect.Identity.Domain.Entities;
using SchoolConnect.Identity.Domain.Interfaces;

namespace SchoolConnect.Identity.Application.Handlers.Roles;

public class CreateRoleCommandHandler : IRequestHandler<CreateRoleCommand, Result<RoleDto>>
{
    private readonly IRoleRepository _roleRepository;
    private readonly IMapper _mapper;

    public CreateRoleCommandHandler(IRoleRepository roleRepository, IMapper mapper)
    {
        _roleRepository = roleRepository;
        _mapper = mapper;
    }

    public async Task<Result<RoleDto>> Handle(CreateRoleCommand request, CancellationToken cancellationToken)
    {
        try
        {
            // Check if role with name exists
            var existingRole = await _roleRepository.GetByNameAsync(request.Name, cancellationToken);
            if (existingRole != null)
            {
                return Result.Failure<RoleDto>($"Role with name '{request.Name}' already exists");
            }

            // Create role
            var role = new Role(request.Name, request.Description, false);

            // Add permissions if provided
            if (request.PermissionIds != null && request.PermissionIds.Any())
            {
                foreach (var permissionId in request.PermissionIds)
                {
                    role.AddPermission(permissionId);
                }
            }

            await _roleRepository.AddAsync(role, cancellationToken);

            var roleDto = _mapper.Map<RoleDto>(role);
            return Result.Success(roleDto);
        }
        catch (Exception ex)
        {
            return Result.Failure<RoleDto>($"Failed to create role: {ex.Message}");
        }
    }
}
