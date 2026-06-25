using MediatR;
using NotesService.Application.Interfaces;
using SharedLibrary.Caching.Constants;
using SharedLibrary.Caching.Interfaces;

namespace NotesService.Application.Commands.UpdateNote;

public class UpdateNoteCommandHandler
    : IRequestHandler<UpdateNoteCommand, bool>
{
    private readonly INoteRepository _noteRepository;
    private readonly ICacheService _cacheService;

    public UpdateNoteCommandHandler(
    INoteRepository noteRepository,
    ICacheService cacheService)
    {
        _noteRepository = noteRepository;
        _cacheService = cacheService;
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
        if (note.UserId != request.UserId)
        {
            return false;
        }
        note.Title = request.Dto.Title;
        note.Content = request.Dto.Content;
        note.UpdatedAt = DateTime.UtcNow;

        await _noteRepository.UpdateAsync(note);

        await _cacheService.RemoveDataAsync(
            CacheKeys.UserNotes(request.UserId));

        await _cacheService.RemoveDataAsync(
            CacheKeys.NoteById(request.UserId, request.Id));

        return true;
    }
}