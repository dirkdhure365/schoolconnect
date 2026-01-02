using AutoMapper;
using MediatR;
using SchoolConnect.Identity.Application.DTOs;
using SchoolConnect.Identity.Application.Queries.Users;
using SchoolConnect.Identity.Domain.Interfaces;

namespace SchoolConnect.Identity.Application.Handlers.Users;

public class GetUserByIdQueryHandler : IRequestHandler<GetUserByIdQuery, UserDto?>
{
    private readonly IUserRepository _userRepository;
    private readonly IMapper _mapper;

    public GetUserByIdQueryHandler(IUserRepository userRepository, IMapper mapper)
    {
        _userRepository = userRepository;
        _mapper = mapper;
    }

    public async Task<UserDto?> Handle(GetUserByIdQuery request, CancellationToken cancellationToken)
    {
        var user = await _userRepository.GetByIdAsync(request.UserId, cancellationToken);
        return user != null ? _mapper.Map<UserDto>(user) : null;
    }
}
