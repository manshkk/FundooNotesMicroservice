using MediatR;
using NotesService.Application.Interfaces;

namespace NotesService.Application.Commands.ChangeColor;

public class ChangeColorCommandHandler
    : IRequestHandler<ChangeColorCommand, bool>
{
    private readonly INoteRepository _noteRepository;

    public ChangeColorCommandHandler(INoteRepository noteRepository)
    {
        _noteRepository = noteRepository;
    }

    public async Task<bool> Handle(
        ChangeColorCommand request,
        CancellationToken cancellationToken)
    {
        var note = await _noteRepository.GetByIdAsync(request.Id);

        if (note == null)
            return false;

        if (note.UserId != request.UserId)
        {
            return false;
        }

        note.Color = request.Dto.Color;
        note.UpdatedAt = DateTime.UtcNow;

        await _noteRepository.UpdateAsync(note);

        return true;
    }
}