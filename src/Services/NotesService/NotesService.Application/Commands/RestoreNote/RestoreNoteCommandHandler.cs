using MediatR;
using NotesService.Application.Interfaces;

namespace NotesService.Application.Commands.RestoreNote;

public class RestoreNoteCommandHandler
    : IRequestHandler<RestoreNoteCommand, bool>
{
    private readonly INoteRepository _noteRepository;

    public RestoreNoteCommandHandler(INoteRepository noteRepository)
    {
        _noteRepository = noteRepository;
    }

    public async Task<bool> Handle(
        RestoreNoteCommand request,
        CancellationToken cancellationToken)
    {
        var note = await _noteRepository.GetByIdAsync(request.Id);

        if (note == null)
        {
            return false;
        }

        if (note.UserId != request.UserId)
        {
            return false;
        }

        note.IsDeleted = false;
        note.UpdatedAt = DateTime.UtcNow;

        await _noteRepository.UpdateAsync(note);

        return true;
    }
}