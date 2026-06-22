using MediatR;
using UserService.Application.DTOs;
using UserService.Application.Interfaces;

namespace UserService.Application.Queries.LoginUser;

public class LoginUserQueryHandler : IRequestHandler<LoginUserQuery, AuthResponseDTO>
{
    private readonly IUserRepository _userRepository;
    private readonly IJwtService _jwtService;

    public LoginUserQueryHandler(
        IUserRepository userRepository,
        IJwtService jwtService)
    {
        _userRepository = userRepository;
        _jwtService = jwtService;
    }

    public async Task<AuthResponseDTO> Handle(LoginUserQuery request, CancellationToken cancellationToken)
    {
        var user = await _userRepository.GetByEmailAsync(request.Dto.Email)
            ?? throw new UnauthorizedAccessException("Invalid credentials.");

        if (!BCrypt.Net.BCrypt.Verify(request.Dto.Password, user.Password))
        {
            throw new UnauthorizedAccessException("Invalid credentials.");
        }

        return new AuthResponseDTO
        {
            Token = _jwtService.GenerateToken(user),
            Email = user.Email,
            FullName = $"{user.FirstName} {user.LastName}"
        };
    }
}
