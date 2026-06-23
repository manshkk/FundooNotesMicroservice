using MediatR;

namespace NotesService.Application.Commands.ArchiveNote;

public record ArchiveNoteCommand(
    int Id,
    int UserId) : IRequest<bool>;