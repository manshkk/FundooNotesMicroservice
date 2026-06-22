namespace UserService.Application.Interfaces;

public interface IEmailService
{
    Task SendWelcomeEmailAsync(
        string email,
        string firstName);
}