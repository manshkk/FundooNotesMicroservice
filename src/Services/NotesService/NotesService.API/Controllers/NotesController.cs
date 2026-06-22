using MediatR;
using Microsoft.AspNetCore.Mvc;
using NotesService.Application.Commands.CreateNote;
using NotesService.Application.DTOs;

namespace NotesService.API.Controllers;

[ApiController]
[Route("api/[controller]")]
public class NotesController : ControllerBase
{
    private readonly IMediator _mediator;

    public NotesController(IMediator mediator)
    {
        _mediator = mediator;
    }

    [HttpPost]
    public async Task<IActionResult> CreateNote(CreateNoteDto dto)
    {
        var noteId = await _mediator.Send(
            new CreateNoteCommand(dto, 1));

        return CreatedAtAction(
            nameof(CreateNote),
            new { id = noteId },
            new { noteId });
    }
}