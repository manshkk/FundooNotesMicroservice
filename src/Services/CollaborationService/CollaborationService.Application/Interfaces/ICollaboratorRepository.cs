using CollaborationService.Domain.Entities;

namespace CollaborationService.Application.Interfaces;

public interface ICollaboratorRepository
{
    Task AddCollaboratorAsync(Collaborator collaborator);

    Task<List<Collaborator>> GetCollaboratorsByNoteIdAsync(int noteId);

    Task<bool> RemoveCollaboratorAsync(int collaboratorId);
}