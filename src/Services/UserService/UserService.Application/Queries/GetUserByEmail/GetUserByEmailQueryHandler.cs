using MediatR;
using UserService.Application.DTOs;
using UserService.Application.Interfaces;

namespace UserService.Application.Queries.GetUserByEmail;

public class GetUserByEmailQueryHandler
    : IRequestHandler<GetUserByEmailQuery, UserDetailsDTO?>
{
    private readonly IUserRepository _userRepository;

    public GetUserByEmailQueryHandler(
        IUserRepository userRepository)
    {
        _userRepository = userRepository;
    }

    public async Task<UserDetailsDTO?> Handle(
        GetUserByEmailQuery request,
        CancellationToken cancellationToken)
    {
        var user = await _userRepository.GetByEmailAsync(request.Email);

        if (user == null)
        {
            return null;
        }

        return new UserDetailsDTO
        {
            UserId = user.UserId,
            Email = user.Email
        };
    }
}