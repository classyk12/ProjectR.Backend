using Microsoft.EntityFrameworkCore;
using Microsoft.EntityFrameworkCore.Metadata.Builders;
using ProjectR.Backend.Domain.Entities;

namespace ProjectR.Backend.Persistence.Configurations
{
    public class BusinessConfiguration : IEntityTypeConfiguration<Business>
    {
        public void Configure(EntityTypeBuilder<Business> builder)
        {
            builder.ToTable("businesses");
            builder.HasKey(e => e.Id);
            builder.Property(e => e.Name).HasMaxLength(200);
            builder.Property(e => e.Type).HasMaxLength(100);
            builder.Property(e => e.PhoneCode).HasMaxLength(4);
            builder.Property(e => e.PhoneNumber).HasMaxLength(15);
            builder.Property(e => e.Industry).HasMaxLength(100);
            builder.Property(e => e.About).HasMaxLength(1000);
            builder.Property(e => e.Location).HasMaxLength(255);
            builder.Property(e => e.Longitude).HasMaxLength(10);
            builder.Property(e => e.Latitude).HasMaxLength(10);

            builder.HasOne(e => e.User)
                  .WithMany()
                  .HasForeignKey(e => e.UserId)
                  .OnDelete(DeleteBehavior.Cascade);
        }
    }
}
