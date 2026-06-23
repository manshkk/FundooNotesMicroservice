using MediatR;
using NotesService.Application.Interfaces;

namespace NotesService.Application.Commands.UnarchiveNote;

public class UnarchiveNoteCommandHandler
    : IRequestHandler<UnarchiveNoteCommand, bool>
{
    private readonly INoteRepository _noteRepository;

    public UnarchiveNoteCommandHandler(INoteRepository noteRepository)
    {
        _noteRepository = noteRepository;
    }

    public async Task<bool> Handle(
        UnarchiveNoteCommand request,
        CancellationToken cancellationToken)
    {
        var note = await _noteRepository.GetByIdAsync(request.Id);

        if (note == null)
            return false;
        if (note.UserId != request.UserId)
        {
            return false;
        }
        note.IsArchived = false;
        note.UpdatedAt = DateTime.UtcNow;

        await _noteRepository.UpdateAsync(note);

        return true;
    }
}