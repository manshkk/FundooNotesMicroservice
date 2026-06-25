using MediatR;
using NotesService.Application.DTOs;
using NotesService.Application.Interfaces;
using SharedLibrary.Caching.Interfaces;
using SharedLibrary.Caching.Constants;

namespace NotesService.Application.Queries.GetNoteById;

public class GetNoteByIdQueryHandler
    : IRequestHandler<GetNoteByIdQuery, NoteResponseDto?>
{
    private readonly INoteRepository _noteRepository;
    private readonly ICacheService _cacheService;

    public GetNoteByIdQueryHandler(
    INoteRepository noteRepository,
    ICacheService cacheService)
    {
        _noteRepository = noteRepository;
        _cacheService = cacheService;
    }

    public async Task<NoteResponseDto?> Handle(
    GetNoteByIdQuery request,
    CancellationToken cancellationToken)
    {
        var cacheKey = CacheKeys.NoteById(request.UserId, request.Id);

        var cachedNote =
            await _cacheService.GetDataAsync<NoteResponseDto>(cacheKey);

        if (cachedNote != null)
        {
            return cachedNote;
        }

        var note = await _noteRepository.GetByIdAsync(request.Id);

        if (note == null)
            return null;

        if (note.UserId != request.UserId)
            return null;

        var response = new NoteResponseDto
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

        await _cacheService.SetDataAsync(
            cacheKey,
            response,
            TimeSpan.FromMinutes(10));

        return response;
    }
}