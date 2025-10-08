using Microsoft.EntityFrameworkCore;
using Microsoft.EntityFrameworkCore.Metadata.Builders;
using ProjectR.Backend.Domain.Entities;

namespace ProjectR.Backend.Persistence.Configurations
{
    public class BusinessAvailabilityConfiguration : IEntityTypeConfiguration<BusinessAvailability>
    {
        public void Configure(EntityTypeBuilder<BusinessAvailability> builder)
        {
            builder.HasKey(x => x.Id);

            builder.Property(x => x.BusinessId)
                .IsRequired();

            builder.HasOne(x => x.Business)
                .WithMany()
                .HasForeignKey(x => x.BusinessId)
                .OnDelete(DeleteBehavior.Cascade);

            builder.Property(x => x.StartDate)
                .IsRequired();

            builder.Property(x => x.EndDate)
                .IsRequired();

            builder.HasMany(x => x.Slots)
                .WithOne(x => x.BusinessAvailability)
                .HasForeignKey(x => x.BusinessAvailabilityId)
                .OnDelete(DeleteBehavior.Cascade);
        }
    }
}
