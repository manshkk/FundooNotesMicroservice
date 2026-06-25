using MediatR;
using NotesService.Application.Interfaces;
using SharedLibrary.Caching.Constants;
using SharedLibrary.Caching.Interfaces;

namespace NotesService.Application.Commands.RestoreNote;

public class RestoreNoteCommandHandler
    : IRequestHandler<RestoreNoteCommand, bool>
{
    private readonly INoteRepository _noteRepository;
    private readonly ICacheService _cacheService;

    public RestoreNoteCommandHandler(
    INoteRepository noteRepository,
    ICacheService cacheService)
    {
        _noteRepository = noteRepository;
        _cacheService = cacheService;
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

        await _cacheService.RemoveDataAsync(
        CacheKeys.UserNotes(request.UserId));

        await _cacheService.RemoveDataAsync(
            CacheKeys.TrashNotes(request.UserId));

        await _cacheService.RemoveDataAsync(
            CacheKeys.NoteById(request.UserId, request.Id));

        return true;
    }
}