using MediatR;
using UserService.Application.DTOs;

namespace UserService.Application.Queries.GetUserByEmail;

public record GetUserByEmailQuery(string Email)
    : IRequest<UserDetailsDTO?>;