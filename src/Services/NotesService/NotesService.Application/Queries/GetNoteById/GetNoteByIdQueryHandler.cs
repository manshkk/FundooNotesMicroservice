using MediatR;
using NotesService.Application.DTOs;
using NotesService.Application.Interfaces;

namespace NotesService.Application.Queries.GetNoteById;

public class GetNoteByIdQueryHandler
    : IRequestHandler<GetNoteByIdQuery, NoteResponseDto?>
{
    private readonly INoteRepository _noteRepository;

    public GetNoteByIdQueryHandler(INoteRepository noteRepository)
    {
        _noteRepository = noteRepository;
    }

    public async Task<NoteResponseDto?> Handle(
        GetNoteByIdQuery request,
        CancellationToken cancellationToken)
    {
        var note = await _noteRepository.GetByIdAsync(request.Id);

        if (note == null)
            return null;

        return new NoteResponseDto
        {
            Id = note.Id,
            Title = note.Title,
            Content = note.Content,
            Color = note.Color,
            UserId = note.UserId,
            IsPinned = note.IsPinned,
            IsArchived = note.IsArchived,
            IsDeleted = note.IsDeleted,
            CreatedAt = note.CreatedAt
        };
    }
}