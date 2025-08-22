using Microsoft.EntityFrameworkCore;
using ProjectR.Backend.Domain.Entities;

namespace ProjectR.Backend.Persistence.DatabaseContext
{
    public class AppDbContext : DbContext
    {
        public DbSet<User> Users => Set<User>();
        public DbSet<AppTheme> AppThemes => Set<AppTheme>();
        public DbSet<Business> Businesses => Set<Business>();
        public DbSet<BusinessAvailability> BusinessAvailabilities => Set<BusinessAvailability>();
        public DbSet<BusinessAvailabilitySlot> BusinessAvailabilitySlots => Set<BusinessAvailabilitySlot>();
        public DbSet<Otp> Otps => Set<Otp>();
        public DbSet<Industry> Industries { get; set; }

        public AppDbContext(DbContextOptions<AppDbContext> options) : base(options) { }

        protected override void OnModelCreating(ModelBuilder modelBuilder)
        {
            base.OnModelCreating(modelBuilder);

            modelBuilder.ApplyConfigurationsFromAssembly(typeof(AppDbContext).Assembly);
        }
    }
}

