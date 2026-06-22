using NotesService.Domain.Entities;

namespace NotesService.Application.Interfaces;

public interface INoteRepository
{
    Task<int> AddAsync(Note note);
}