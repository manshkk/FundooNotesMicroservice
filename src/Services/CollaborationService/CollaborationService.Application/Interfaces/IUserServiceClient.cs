using CollaborationService.Application.DTOs;

namespace CollaborationService.Application.Interfaces;

public interface IUserServiceClient
{
    Task<UserDto?> GetUserByEmailAsync(string email);
}