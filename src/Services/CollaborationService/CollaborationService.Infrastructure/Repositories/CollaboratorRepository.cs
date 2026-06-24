using CollaborationService.Application.Interfaces;
using CollaborationService.Domain.Entities;
using CollaborationService.Infrastructure.Context;
using Microsoft.EntityFrameworkCore;

namespace CollaborationService.Infrastructure.Repositories;

public class CollaboratorRepository : ICollaboratorRepository
{
    private readonly CollaborationDbContext _context;

    public CollaboratorRepository(CollaborationDbContext context)
    {
        _context = context;
    }

    public async Task AddCollaboratorAsync(Collaborator collaborator)
    {
        await _context.Collaborators.AddAsync(collaborator);
        await _context.SaveChangesAsync();
    }

    public async Task<List<Collaborator>> GetCollaboratorsByNoteIdAsync(int noteId)
    {
        return await _context.Collaborators
            .Where(x => x.NoteId == noteId)
            .ToListAsync();
    }

    public async Task<bool> RemoveCollaboratorAsync(int collaboratorId)
    {
        var collaborator = await _context.Collaborators
            .FirstOrDefaultAsync(x => x.CollaboratorId == collaboratorId);

        if (collaborator == null)
            return false;

        _context.Collaborators.Remove(collaborator);

        await _context.SaveChangesAsync();

        return true;
    }
}