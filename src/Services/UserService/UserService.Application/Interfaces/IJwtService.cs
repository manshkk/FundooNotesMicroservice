using UserService.Domain.Entities;

namespace UserService.Application.Interfaces;

public interface IJwtService
{
    string GenerateToken(User user);
}