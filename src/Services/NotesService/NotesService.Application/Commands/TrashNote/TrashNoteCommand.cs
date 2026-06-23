using MediatR;

namespace NotesService.Application.Commands.TrashNote;

public record TrashNoteCommand(
    int Id,
    int UserId)
    : IRequest<bool>;