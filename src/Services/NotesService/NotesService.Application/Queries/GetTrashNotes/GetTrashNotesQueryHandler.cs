using MediatR;
using NotesService.Application.DTOs;
using NotesService.Application.Interfaces;
using SharedLibrary.Caching.Constants;
using SharedLibrary.Caching.Interfaces;

namespace NotesService.Application.Queries.GetTrashNotes;

public class GetTrashNotesQueryHandler
    : IRequestHandler<GetTrashNotesQuery, List<NoteResponseDto>>
{
    private readonly INoteRepository _noteRepository;
    private readonly ICacheService _cacheService;

    public GetTrashNotesQueryHandler(
    INoteRepository noteRepository,
    ICacheService cacheService)
    {
        _noteRepository = noteRepository;
        _cacheService = cacheService;
    }

    public async Task<List<NoteResponseDto>> Handle(
    GetTrashNotesQuery request,
    CancellationToken cancellationToken)
    {
        var cacheKey = CacheKeys.TrashNotes(request.UserId);

        var cachedNotes =
            await _cacheService.GetDataAsync<List<NoteResponseDto>>(cacheKey);

        if (cachedNotes != null)
        {
            return cachedNotes;
        }

        var notes = await _noteRepository
            .GetTrashNotesByUserIdAsync(request.UserId);

        var noteDtos = notes.Select(note => new NoteResponseDto
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

        await _cacheService.SetDataAsync(
            cacheKey,
            noteDtos,
            TimeSpan.FromMinutes(10));

        return noteDtos;
    }
}