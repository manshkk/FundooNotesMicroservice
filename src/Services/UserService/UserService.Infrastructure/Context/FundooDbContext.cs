using Microsoft.EntityFrameworkCore;
using UserService.Domain.Entities;

namespace UserService.Infrastructure.Context;

public class FundooDbContext : DbContext
{
    public FundooDbContext(DbContextOptions<FundooDbContext> options)
        : base(options)
    {
    }

    public DbSet<User> Users { get; set; }
}