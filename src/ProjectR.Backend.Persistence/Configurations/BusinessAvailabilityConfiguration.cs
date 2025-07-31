using Microsoft.EntityFrameworkCore;
using Microsoft.EntityFrameworkCore.Metadata.Builders;
using ProjectR.Backend.Domain.Entities;

namespace ProjectR.Backend.Persistence.Configurations
{
    public class BusinessAvailabilityConfiguration : IEntityTypeConfiguration<BusinessAvailability>
    {
        public void Configure(EntityTypeBuilder<BusinessAvailability> builder)
        {
            builder.HasKey(e => e.Id);
            builder.Property(e => e.BusinessId).IsRequired();
            builder.Property(e => e.StartDate).IsRequired();
            builder.Property(e => e.EndDate).IsRequired();
            builder.Property(e => e.ValidFrom);
            builder.Property(e => e.ValidTo);

            builder.HasOne(e => e.Business)
                  .WithMany()
                  .HasForeignKey(e => e.BusinessId)
                  .OnDelete(DeleteBehavior.Cascade);

            builder.HasMany(e => e.Slots)
                  .WithOne()
                  .HasForeignKey(e => e.BusinessAvailabilityId);
        }
    }
}
