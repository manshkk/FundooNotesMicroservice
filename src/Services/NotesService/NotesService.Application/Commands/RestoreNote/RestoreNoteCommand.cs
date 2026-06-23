using MediatR;

namespace NotesService.Application.Commands.RestoreNote;

public record RestoreNoteCommand(
    int Id,
    int UserId)
    : IRequest<bool>;