using Microsoft.EntityFrameworkCore;
using ProjectR.Backend.Domain.Entities;

namespace ProjectR.Backend.Persistence.DatabaseContext
{
    public class AppDbContext : DbContext
    {
        public DbSet<User> Users { get; set; } = default!;

        public AppDbContext(DbContextOptions<AppDbContext> options) : base(options) { }
    }
}

