using MediatR;
using NotesService.Application.Interfaces;
using NotesService.Domain.Entities;

namespace NotesService.Application.Commands.CreateNote;

public class CreateNoteCommandHandler : IRequestHandler<CreateNoteCommand, int>
{
    private readonly INoteRepository _noteRepository;

    public CreateNoteCommandHandler(INoteRepository noteRepository)
    {
        _noteRepository = noteRepository;
    }

    public async Task<int> Handle(
        CreateNoteCommand request,
        CancellationToken cancellationToken)
    {
        var note = new Note
        {
            Title = request.Dto.Title,
            Content = request.Dto.Content,
            Color = request.Dto.Color,
            UserId = request.UserId,
            CreatedAt = DateTime.UtcNow,
            UpdatedAt = DateTime.UtcNow
        };

        return await _noteRepository.AddAsync(note);
    }
}