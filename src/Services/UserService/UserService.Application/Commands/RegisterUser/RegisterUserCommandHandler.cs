using MediatR;
using UserService.Application.Interfaces;
using UserService.Domain.Entities;
using SharedLibrary.Messaging.Events;
using SharedLibrary.Messaging.Interfaces;

namespace UserService.Application.Commands.RegisterUser;

public class RegisterUserCommandHandler : IRequestHandler<RegisterUserCommand, int>
{
    private readonly IUserRepository _userRepository;
    private readonly IMessagePublisher _messagePublisher;

    public RegisterUserCommandHandler(
    IUserRepository userRepository,
    IMessagePublisher messagePublisher)
    {
        _userRepository = userRepository;
        _messagePublisher = messagePublisher;
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

        await _messagePublisher.PublishAsync(
    "fundoonotes.user.registered",
    new UserRegisteredEvent
    {
        UserId = userId,
        FirstName = user.FirstName,
        LastName = user.LastName,
        Email = user.Email
    },
    cancellationToken);

        return userId;
    }
}
