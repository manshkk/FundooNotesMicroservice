using Microsoft.EntityFrameworkCore;
using NotesService.Domain.Entities;

namespace NotesService.Infrastructure.Context;

public class FundooDbContext : DbContext
{
    public FundooDbContext(DbContextOptions<FundooDbContext> options)
        : base(options)
    {
    }

    public DbSet<Note> Notes { get; set; }
}