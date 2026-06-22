using MediatR;
using UserService.Application.DTOs;

namespace UserService.Application.Commands.RegisterUser;

public record RegisterUserCommand(RegisterUserDTO Dto) : IRequest<int>;
