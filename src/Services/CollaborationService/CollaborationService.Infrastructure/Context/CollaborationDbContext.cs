using CollaborationService.Domain.Entities;
using Microsoft.EntityFrameworkCore;
using System.Reflection.Emit;

namespace CollaborationService.Infrastructure.Context;

public class CollaborationDbContext : DbContext
{
    public CollaborationDbContext(DbContextOptions<CollaborationDbContext> options)
        : base(options)
    {
    }

    public DbSet<Collaborator> Collaborators { get; set; }

    protected override void OnModelCreating(ModelBuilder modelBuilder)
    {
        modelBuilder.Entity<Collaborator>(entity =>
        {
            entity.HasKey(c => c.CollaboratorId);

            entity.Property(c => c.CollaboratorEmail)
                  .IsRequired()
                  .HasMaxLength(100);
        });

        base.OnModelCreating(modelBuilder);
    }
}