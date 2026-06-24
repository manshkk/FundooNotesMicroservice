using MediatR;
using Microsoft.AspNetCore.Mvc;
using UserService.Application.Commands.RegisterUser;
using UserService.Application.DTOs;
using UserService.Application.Queries.GetUserByEmail;
using UserService.Application.Queries.LoginUser;
using UserService.Application.Queries.GetUserByEmail;

namespace UserService.API.Controllers;

[ApiController]
[Route("api/[controller]")]
public class UsersController : ControllerBase
{
    private readonly IMediator _mediator;

    public UsersController(IMediator mediator)
    {
        _mediator = mediator;
    }

    [HttpPost("register")]
    [ProducesResponseType(typeof(object), StatusCodes.Status201Created)]
    [ProducesResponseType(StatusCodes.Status400BadRequest)]
    public async Task<IActionResult> Register([FromBody] RegisterUserDTO dto)
    {
        var userId = await _mediator.Send(new RegisterUserCommand(dto));
        return CreatedAtAction(nameof(Register), new { id = userId }, new { userId });
    }

    [HttpPost("login")]
    [ProducesResponseType(typeof(AuthResponseDTO), StatusCodes.Status200OK)]
    [ProducesResponseType(StatusCodes.Status401Unauthorized)]
    public async Task<IActionResult> Login([FromBody] LoginUserDTO dto)
    {
        var result = await _mediator.Send(new LoginUserQuery(dto));
        return Ok(result);
    }
    [HttpGet("email/{email}")]
    public async Task<IActionResult> GetUserByEmail(string email)
    {
        var user = await _mediator.Send(
            new GetUserByEmailQuery(email));

        if (user == null)
        {
            return NotFound();
        }

        return Ok(user);
    }
}
