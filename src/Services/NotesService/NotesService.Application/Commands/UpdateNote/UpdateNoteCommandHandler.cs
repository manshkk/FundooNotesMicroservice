using MediatR;
using NotesService.Application.Interfaces;

namespace NotesService.Application.Commands.UpdateNote;

public class UpdateNoteCommandHandler
    : IRequestHandler<UpdateNoteCommand, bool>
{
    private readonly INoteRepository _noteRepository;

    public UpdateNoteCommandHandler(INoteRepository noteRepository)
    {
        _noteRepository = noteRepository;
    }

    public async Task<bool> Handle(
        UpdateNoteCommand request,
        CancellationToken cancellationToken)
    {
        var note = await _noteRepository.GetByIdAsync(request.Id);

        if (note == null)
        {
            return false;
        }

        note.Title = request.Dto.Title;
        note.Content = request.Dto.Content;
        note.UpdatedAt = DateTime.UtcNow;

        await _noteRepository.UpdateAsync(note);

        return true;
    }
}