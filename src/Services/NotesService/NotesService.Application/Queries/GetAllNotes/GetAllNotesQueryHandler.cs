using MediatR;
using NotesService.Application.DTOs;
using NotesService.Application.Interfaces;
using SharedLibrary.Caching.Constants;
using SharedLibrary.Caching.Interfaces;

namespace NotesService.Application.Queries.GetAllNotes;

public class GetAllNotesQueryHandler
    : IRequestHandler<GetAllNotesQuery, List<NoteResponseDto>>
{
    private readonly INoteRepository _noteRepository;
    private readonly ICacheService _cacheService;

    public GetAllNotesQueryHandler(
    INoteRepository noteRepository,
    ICacheService cacheService)
    {
        _noteRepository = noteRepository;
        _cacheService = cacheService;
    }

    public async Task<List<NoteResponseDto>> Handle(
    GetAllNotesQuery request,
    CancellationToken cancellationToken)
    {
        var cacheKey = CacheKeys.UserNotes(request.UserId);

        // 1. Check Redis
        var cachedNotes = await _cacheService
            .GetDataAsync<List<NoteResponseDto>>(cacheKey);

        if (cachedNotes is not null)
        {
            return cachedNotes;
        }

        // 2. Get data from SQL Server
        var notes = await _noteRepository
            .GetAllByUserIdAsync(request.UserId);

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

        // 3. Store in Redis for 10 minutes
        await _cacheService.SetDataAsync(
            cacheKey,
            noteDtos,
            TimeSpan.FromMinutes(10));

        return noteDtos;
    }
}