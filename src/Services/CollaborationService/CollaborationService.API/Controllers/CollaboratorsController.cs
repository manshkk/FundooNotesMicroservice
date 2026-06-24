using CollaborationService.Application.Commands.AddCollaborator;
using CollaborationService.Application.DTOs;
using CollaborationService.Application.Queries.GetCollaborators;
using MediatR;
using Microsoft.AspNetCore.Mvc;
using CollaborationService.Application.Commands.RemoveCollaborator;
using Microsoft.AspNetCore.Authorization;
using System.Security.Claims;

namespace CollaborationService.API.Controllers;

[ApiController]
[Route("api/[controller]")]
[Authorize]
public class CollaboratorsController : ControllerBase
{
    private readonly IMediator _mediator;

    public CollaboratorsController(IMediator mediator)
    {
        _mediator = mediator;
    }

    [HttpPost]
    public async Task<IActionResult> AddCollaborator(
    AddCollaboratorRequestDto dto)
    {
        var userIdClaim =
            User.FindFirst("UserId")?.Value;

        if (string.IsNullOrEmpty(userIdClaim))
        {
            return Unauthorized();
        }

        var token =
            Request.Headers["Authorization"]
            .ToString()
            .Replace("Bearer ", "");

        var result =
            await _mediator.Send(
                new AddCollaboratorCommand(
                    dto,
                    int.Parse(userIdClaim),
                    token));

        if (!result)
        {
            return BadRequest(
                "Invalid note or collaborator.");
        }

        return Ok(
            "Collaborator added successfully.");
    }

    [HttpGet("{noteId}")]
    public async Task<IActionResult> GetCollaborators(
        int noteId)
    {
        var collaborators = await _mediator.Send(
            new GetCollaboratorsQuery(noteId));

        return Ok(collaborators);
    }
    [HttpDelete("{collaboratorId}")]
    public async Task<IActionResult> RemoveCollaborator(
    int collaboratorId)
    {
        var result = await _mediator.Send(
            new RemoveCollaboratorCommand(collaboratorId));

        if (!result)
        {
            return NotFound(
                "Collaborator not found.");
        }

        return Ok(
            "Collaborator removed successfully.");
    }
}