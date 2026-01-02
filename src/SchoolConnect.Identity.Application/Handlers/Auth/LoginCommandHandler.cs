using AutoMapper;
using MediatR;
using SchoolConnect.Common.Application.Models;
using SchoolConnect.Identity.Application.Commands.Auth;
using SchoolConnect.Identity.Application.DTOs;
using SchoolConnect.Identity.Application.Services;
using SchoolConnect.Identity.Domain.Entities;
using SchoolConnect.Identity.Domain.Enums;
using SchoolConnect.Identity.Domain.Exceptions;
using SchoolConnect.Identity.Domain.Interfaces;

namespace SchoolConnect.Identity.Application.Handlers.Auth;

public class LoginCommandHandler : IRequestHandler<LoginCommand, Result<LoginResultDto>>
{
    private readonly IUserRepository _userRepository;
    private readonly IPasswordHasher _passwordHasher;
    private readonly ITokenService _tokenService;
    private readonly IRoleRepository _roleRepository;
    private readonly IPermissionRepository _permissionRepository;
    private readonly IMapper _mapper;

    public LoginCommandHandler(
        IUserRepository userRepository,
        IPasswordHasher passwordHasher,
        ITokenService tokenService,
        IRoleRepository roleRepository,
        IPermissionRepository permissionRepository,
        IMapper mapper)
    {
        _userRepository = userRepository;
        _passwordHasher = passwordHasher;
        _tokenService = tokenService;
        _roleRepository = roleRepository;
        _permissionRepository = permissionRepository;
        _mapper = mapper;
    }

    public async Task<Result<LoginResultDto>> Handle(LoginCommand request, CancellationToken cancellationToken)
    {
        try
        {
            // Get user
            var user = await _userRepository.GetByEmailAsync(request.Email, cancellationToken);
            if (user == null)
            {
                throw new InvalidCredentialsException();
            }

            // Verify password
            if (!_passwordHasher.VerifyPassword(request.Password, user.PasswordHash))
            {
                user.RecordFailedLogin();
                await _userRepository.UpdateAsync(user, cancellationToken);
                throw new InvalidCredentialsException();
            }

            // Check user status
            if (user.IsLocked)
            {
                throw new UserDisabledException("Account is locked due to too many failed login attempts");
            }

            if (user.Status == UserStatus.Suspended)
            {
                throw new UserDisabledException("Account is suspended");
            }

            if (!user.EmailVerified)
            {
                throw new UserNotVerifiedException();
            }

            // Get roles and permissions
            var roles = await _roleRepository.GetByIdsAsync(user.RoleIds, cancellationToken);
            var roleNames = roles.Select(r => r.Name).ToList();
            
            var permissionIds = roles.SelectMany(r => r.PermissionIds).Distinct().ToList();
            var permissions = await _permissionRepository.GetByIdsAsync(permissionIds, cancellationToken);
            var permissionCodes = permissions.Select(p => p.Code).ToList();

            // Generate tokens
            var accessToken = _tokenService.GenerateAccessToken(user.Id, user.Email, roleNames, permissionCodes);
            var refreshToken = _tokenService.GenerateRefreshToken();
            var expiresAt = _tokenService.GetTokenExpiration();

            // Store refresh token
            var refreshTokenEntity = new RefreshToken(user.Id, refreshToken, expiresAt);

            // Update user login info
            user.RecordLogin(request.IpAddress ?? "unknown", request.DeviceInfo ?? "unknown");
            await _userRepository.UpdateAsync(user, cancellationToken);

            var userDto = _mapper.Map<UserDto>(user);
            var result = new LoginResultDto
            {
                User = userDto,
                Tokens = new AuthTokenDto
                {
                    AccessToken = accessToken,
                    RefreshToken = refreshToken,
                    ExpiresAt = expiresAt
                },
                RequiresMfa = user.MfaEnabled
            };

            return Result.Success(result);
        }
        catch (InvalidCredentialsException ex)
        {
            return Result.Failure<LoginResultDto>(ex.Message);
        }
        catch (UserDisabledException ex)
        {
            return Result.Failure<LoginResultDto>(ex.Message);
        }
        catch (UserNotVerifiedException ex)
        {
            return Result.Failure<LoginResultDto>(ex.Message);
        }
        catch (Exception ex)
        {
            return Result.Failure<LoginResultDto>($"Login failed: {ex.Message}");
        }
    }
}
