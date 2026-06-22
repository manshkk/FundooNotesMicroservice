using MediatR;
using NotesService.Application.Interfaces;

namespace NotesService.Application.Commands.UnpinNote;

public class UnpinNoteCommandHandler
    : IRequestHandler<UnpinNoteCommand, bool>
{
    private readonly INoteRepository _noteRepository;

    public UnpinNoteCommandHandler(INoteRepository noteRepository)
    {
        _noteRepository = noteRepository;
    }

    public async Task<bool> Handle(
        UnpinNoteCommand request,
        CancellationToken cancellationToken)
    {
        var note = await _noteRepository.GetByIdAsync(request.Id);

        if (note == null)
            return false;

        note.IsPinned = false;
        note.UpdatedAt = DateTime.UtcNow;

        await _noteRepository.UpdateAsync(note);

        return true;
    }
}