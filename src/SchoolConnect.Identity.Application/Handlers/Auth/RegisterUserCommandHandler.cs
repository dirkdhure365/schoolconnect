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
using SchoolConnect.Identity.Domain.ValueObjects;

namespace SchoolConnect.Identity.Application.Handlers.Auth;

public class RegisterUserCommandHandler : IRequestHandler<RegisterUserCommand, Result<UserDto>>
{
    private readonly IUserRepository _userRepository;
    private readonly IPasswordHasher _passwordHasher;
    private readonly IMapper _mapper;

    public RegisterUserCommandHandler(
        IUserRepository userRepository,
        IPasswordHasher passwordHasher,
        IMapper mapper)
    {
        _userRepository = userRepository;
        _passwordHasher = passwordHasher;
        _mapper = mapper;
    }

    public async Task<Result<UserDto>> Handle(RegisterUserCommand request, CancellationToken cancellationToken)
    {
        try
        {
            // Validate email
            var email = Email.Create(request.Email);

            // Check if user exists
            if (await _userRepository.ExistsByEmailAsync(request.Email, cancellationToken))
            {
                throw new UserAlreadyExistsException(request.Email);
            }

            // Hash password
            var passwordHash = _passwordHasher.HashPassword(request.Password);

            // Parse user type
            if (!Enum.TryParse<UserType>(request.UserType, true, out var userType))
            {
                return Result.Failure<UserDto>("Invalid user type");
            }

            // Create user
            var user = new User(
                request.Email,
                passwordHash,
                userType,
                request.FirstName,
                request.LastName,
                request.PhoneNumber
            );

            await _userRepository.AddAsync(user, cancellationToken);

            var userDto = _mapper.Map<UserDto>(user);
            return Result.Success(userDto);
        }
        catch (UserAlreadyExistsException ex)
        {
            return Result.Failure<UserDto>(ex.Message);
        }
        catch (Exception ex)
        {
            return Result.Failure<UserDto>($"Failed to register user: {ex.Message}");
        }
    }
}
