using Microsoft.EntityFrameworkCore;
using NotesService.Application.Interfaces;
using NotesService.Domain.Entities;
using NotesService.Infrastructure.Context;

namespace NotesService.Infrastructure.Repositories;

public class NoteRepository : INoteRepository
{
    private readonly FundooDbContext _context;

    public NoteRepository(FundooDbContext context)
    {
        _context = context;
    }

    public async Task<int> AddAsync(Note note)
    {
        await _context.Notes.AddAsync(note);
        await _context.SaveChangesAsync();

        return note.Id;
    }
    public async Task<List<Note>> GetAllAsync()
    {
        return await _context.Notes.ToListAsync();
    }
}