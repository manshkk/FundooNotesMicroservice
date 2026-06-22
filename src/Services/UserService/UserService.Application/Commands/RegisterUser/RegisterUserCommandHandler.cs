using MediatR;
using UserService.Application.Interfaces;
using UserService.Domain.Entities;

namespace UserService.Application.Commands.RegisterUser;

public class RegisterUserCommandHandler : IRequestHandler<RegisterUserCommand, int>
{
    private readonly IUserRepository _userRepository;
    private readonly IEmailService _emailService;

    public RegisterUserCommandHandler(
        IUserRepository userRepository,
        IEmailService emailService)
    {
        _userRepository = userRepository;
        _emailService = emailService;
    }

    public async Task<int> Handle(RegisterUserCommand request, CancellationToken cancellationToken)
    {
        var existingUser = await _userRepository.GetByEmailAsync(request.Dto.Email);

        if (existingUser != null)
        {
            throw new InvalidOperationException("User with this email already exists.");
        }

        var user = new User
        {
            FirstName = request.Dto.FirstName,
            LastName = request.Dto.LastName,
            Email = request.Dto.Email,
            Password = BCrypt.Net.BCrypt.HashPassword(request.Dto.Password),
            CreatedAt = DateTime.UtcNow
        };

        var userId = await _userRepository.AddAsync(user);

        await _emailService.SendWelcomeEmailAsync(user.Email, user.FirstName);

        return userId;
    }
}
