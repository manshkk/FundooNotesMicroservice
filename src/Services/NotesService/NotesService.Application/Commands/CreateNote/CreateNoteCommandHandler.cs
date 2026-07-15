using MediatR;
using NotesService.Application.Interfaces;
using NotesService.Domain.Entities;
using SharedLibrary.Caching.Constants;
using SharedLibrary.Caching.Interfaces;

namespace NotesService.Application.Commands.CreateNote;

public class CreateNoteCommandHandler : IRequestHandler<CreateNoteCommand, int>
{
    private readonly INoteRepository _noteRepository;
    private readonly ICacheService _cacheService;

    public CreateNoteCommandHandler(
    INoteRepository noteRepository,
    ICacheService cacheService)
    {
        _noteRepository = noteRepository;
        _cacheService = cacheService;
    }

    public async Task<int> Handle(
        CreateNoteCommand request,
        CancellationToken cancellationToken)
    {
        var note = new Note
        {
            Title = request.Dto.Title,
            Content = request.Dto.Content,
            UserId = request.UserId,
            CreatedAt = DateTime.UtcNow,
            UpdatedAt = DateTime.UtcNow
        };

        var noteId = await _noteRepository.AddAsync(note);

        await _cacheService.RemoveDataAsync(
            CacheKeys.UserNotes(request.UserId));
        await _cacheService.RemoveDataAsync(
             CacheKeys.TrashNotes(request.UserId));

        return noteId;
    }
}