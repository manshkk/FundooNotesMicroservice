using MediatR;
using Microsoft.AspNetCore.Mvc;
using NotesService.Application.Commands.CreateNote;
using NotesService.Application.DTOs;
using NotesService.Application.Queries.GetAllNotes;
using NotesService.Application.Commands.UpdateNote;
using NotesService.Application.Commands.TrashNote;
using NotesService.Application.Commands.RestoreNote;
using NotesService.Application.Queries.GetTrashNotes;
using NotesService.Application.Commands.ArchiveNote;
using NotesService.Application.Commands.UnarchiveNote;
using NotesService.Application.Commands.PinNote;
using NotesService.Application.Commands.UnpinNote;
using NotesService.Application.Commands.ChangeColor;
using NotesService.Application.Queries.GetNoteById;
using Microsoft.AspNetCore.Authorization;
using System.Security.Claims;

namespace NotesService.API.Controllers;

[ApiController]
[Route("api/[controller]")]
[Authorize]
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
        var userId = int.Parse(
            User.FindFirst("UserId")!.Value);

        var noteId = await _mediator.Send(
            new CreateNoteCommand(dto, userId));

        return CreatedAtAction(
            nameof(CreateNote),
            new { id = noteId },
            new { noteId });
    }
    [HttpGet]
    public async Task<IActionResult> GetAllNotes()
    {
        var userId = int.Parse(
            User.FindFirst("UserId")!.Value);

        var notes = await _mediator.Send(
            new GetAllNotesQuery(userId));

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
    [HttpGet("trash")]
    public async Task<IActionResult> GetTrashNotes()
    {
        var notes = await _mediator.Send(
            new GetTrashNotesQuery());

        return Ok(notes);
    }

    [HttpPatch("{id}/restore")]
    public async Task<IActionResult> RestoreNote(int id)
    {
        var result = await _mediator.Send(
            new RestoreNoteCommand(id));

        if (!result)
        {
            return NotFound();
        }

        return Ok(new
        {
            message = "Note restored successfully"
        });
    }
    [HttpPatch("{id}/archive")]
    public async Task<IActionResult> ArchiveNote(int id)
    {
        var result = await _mediator.Send(
            new ArchiveNoteCommand(id));

        if (!result)
            return NotFound();

        return Ok(new
        {
            message = "Note archived successfully"
        });
    }

    [HttpPatch("{id}/unarchive")]
    public async Task<IActionResult> UnarchiveNote(int id)
    {
        var result = await _mediator.Send(
            new UnarchiveNoteCommand(id));

        if (!result)
            return NotFound();

        return Ok(new
        {
            message = "Note unarchived successfully"
        });
    }
    [HttpPatch("{id}/pin")]
    public async Task<IActionResult> PinNote(int id)
    {
        var result = await _mediator.Send(
            new PinNoteCommand(id));

        if (!result)
            return NotFound();

        return Ok(new
        {
            message = "Note pinned successfully"
        });
    }

    [HttpPatch("{id}/unpin")]
    public async Task<IActionResult> UnpinNote(int id)
    {
        var result = await _mediator.Send(
            new UnpinNoteCommand(id));

        if (!result)
            return NotFound();

        return Ok(new
        {
            message = "Note unpinned successfully"
        });
    }
    [HttpPatch("{id}/color")]
    public async Task<IActionResult> ChangeColor(
    int id,
    ChangeColorDto dto)
    {
        var result = await _mediator.Send(
            new ChangeColorCommand(id, dto));

        if (!result)
            return NotFound();

        return Ok(new
        {
            message = "Color updated successfully"
        });
    }
    [HttpGet("{id}")]
    public async Task<IActionResult> GetNoteById(int id)
    {
        var note = await _mediator.Send(
            new GetNoteByIdQuery(id));

        if (note == null)
            return NotFound();

        return Ok(note);
    }
}