using CollaborationService.Application.DTOs;

namespace CollaborationService.Application.Interfaces;

public interface INotesServiceClient
{
    Task<NoteDto?> GetNoteByIdAsync(
        int noteId,
        string token);
}