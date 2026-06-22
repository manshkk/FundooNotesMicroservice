using MediatR;
using NotesService.Application.Interfaces;

namespace NotesService.Application.Commands.PinNote;

public class PinNoteCommandHandler
    : IRequestHandler<PinNoteCommand, bool>
{
    private readonly INoteRepository _noteRepository;

    public PinNoteCommandHandler(INoteRepository noteRepository)
    {
        _noteRepository = noteRepository;
    }

    public async Task<bool> Handle(
        PinNoteCommand request,
        CancellationToken cancellationToken)
    {
        var note = await _noteRepository.GetByIdAsync(request.Id);

        if (note == null)
            return false;

        note.IsPinned = true;
        note.UpdatedAt = DateTime.UtcNow;

        await _noteRepository.UpdateAsync(note);

        return true;
    }
}