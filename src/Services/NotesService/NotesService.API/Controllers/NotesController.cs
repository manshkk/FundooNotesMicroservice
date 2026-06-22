using MediatR;
using Microsoft.AspNetCore.Mvc;
using NotesService.Application.Commands.CreateNote;
using NotesService.Application.DTOs;
using NotesService.Application.Queries.GetAllNotes;
using NotesService.Application.Commands.UpdateNote;
using NotesService.Application.Commands.TrashNote;

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
    [HttpGet]
    public async Task<IActionResult> GetAllNotes()
    {
        var notes = await _mediator.Send(new GetAllNotesQuery());

        return Ok(notes);
    }
    [HttpPut("{id}")]
    public async Task<IActionResult> UpdateNote(
    int id,
    UpdateNoteDto dto)
    {
        var result = await _mediator.Send(
            new UpdateNoteCommand(id, dto));

        if (!result)
        {
            return NotFound();
        }

        return Ok(new
        {
            message = "Note updated successfully"
        });
    }
    [HttpDelete("{id}")]
    public async Task<IActionResult> TrashNote(int id)
    {
        var result = await _mediator.Send(
            new TrashNoteCommand(id));

        if (!result)
        {
            return NotFound();
        }

        return Ok(new
        {
            message = "Note moved to trash successfully"
        });
    }
}