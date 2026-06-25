using Microsoft.AspNetCore.Mvc;
using SharedLibrary.Messaging.Events;
using SharedLibrary.Messaging.Interfaces;

namespace UserService.API.Controllers;

[ApiController]
[Route("api/[controller]")]
public class RabbitMqController : ControllerBase
{
    private readonly IMessagePublisher _publisher;

    public RabbitMqController(IMessagePublisher publisher)
    {
        _publisher = publisher;
    }

    [HttpPost("publish")]
    public async Task<IActionResult> Publish()
    {
        var message = new UserRegisteredEvent
        {
            UserId = 1,
            FirstName = "Manish",
            LastName = "Kaushal",
            Email = "test@test.com"
        };

        await _publisher.PublishAsync(
            "fundoonotes.user.registered",
            message);

        return Ok("Message Published Successfully");
    }
}