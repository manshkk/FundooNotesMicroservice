using MediatR;
using NotesService.Application.DTOs;
using NotesService.Application.Interfaces;

namespace NotesService.Application.Queries.GetTrashNotes;

public class GetTrashNotesQueryHandler
    : IRequestHandler<GetTrashNotesQuery, List<NoteResponseDto>>
{
    private readonly INoteRepository _noteRepository;

    public GetTrashNotesQueryHandler(INoteRepository noteRepository)
    {
        _noteRepository = noteRepository;
    }

    public async Task<List<NoteResponseDto>> Handle(
        GetTrashNotesQuery request,
        CancellationToken cancellationToken)
    {
        var notes = await _noteRepository.GetTrashNotesAsync();

        return notes.Select(note => new NoteResponseDto
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
        }).ToList();
    }
}