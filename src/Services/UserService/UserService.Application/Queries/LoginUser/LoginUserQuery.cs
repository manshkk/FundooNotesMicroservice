using MediatR;
using UserService.Application.DTOs;

namespace UserService.Application.Queries.LoginUser;

public record LoginUserQuery(LoginUserDTO Dto) : IRequest<AuthResponseDTO>;
