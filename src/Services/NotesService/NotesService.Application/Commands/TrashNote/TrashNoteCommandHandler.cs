using MediatR;
using NotesService.Application.Interfaces;

namespace NotesService.Application.Commands.TrashNote;

public class TrashNoteCommandHandler
    : IRequestHandler<TrashNoteCommand, bool>
{
    private readonly INoteRepository _noteRepository;

    public TrashNoteCommandHandler(INoteRepository noteRepository)
    {
        _noteRepository = noteRepository;
    }

    public async Task<bool> Handle(
        TrashNoteCommand request,
        CancellationToken cancellationToken)
    {
        var note = await _noteRepository.GetByIdAsync(request.Id);

        if (note == null)
        {
            return false;
        }

        note.IsDeleted = true;
        note.UpdatedAt = DateTime.UtcNow;

        await _noteRepository.UpdateAsync(note);

        return true;
    }
}