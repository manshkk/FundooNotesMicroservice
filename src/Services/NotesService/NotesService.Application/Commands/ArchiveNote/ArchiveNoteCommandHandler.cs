using MediatR;
using NotesService.Application.Interfaces;

namespace NotesService.Application.Commands.ArchiveNote;

public class ArchiveNoteCommandHandler
    : IRequestHandler<ArchiveNoteCommand, bool>
{
    private readonly INoteRepository _noteRepository;

    public ArchiveNoteCommandHandler(INoteRepository noteRepository)
    {
        _noteRepository = noteRepository;
    }

    public async Task<bool> Handle(
        ArchiveNoteCommand request,
        CancellationToken cancellationToken)
    {
        var note = await _noteRepository.GetByIdAsync(request.Id);

        if (note == null)
            return false;

        if (note.UserId != request.UserId)
        {
            return false;
        }

        note.IsArchived = true;
        note.UpdatedAt = DateTime.UtcNow;

        await _noteRepository.UpdateAsync(note);

        return true;
    }
}