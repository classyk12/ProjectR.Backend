using Microsoft.EntityFrameworkCore;
using Microsoft.EntityFrameworkCore.Metadata.Builders;
using ProjectR.Backend.Domain.Entities;

namespace ProjectR.Backend.Persistence.Configurations
{
    public class BusinessAvailabilitySlotConfiguration : IEntityTypeConfiguration<BusinessAvailabilitySlot>
    {
        public void Configure(EntityTypeBuilder<BusinessAvailabilitySlot> builder)
        {
            builder.HasKey(x => x.Id);

            builder.Property(x => x.BusinessAvailabilityId)
                .IsRequired();

            builder.HasOne(x => x.BusinessAvailability)
                .WithMany(x => x.Slots)
                .HasForeignKey(x => x.BusinessAvailabilityId)
                .OnDelete(DeleteBehavior.Cascade);

            builder.Property(x => x.StartTime)
                .IsRequired();

            builder.Property(x => x.EndTime)
                .IsRequired();

            builder.Property(x => x.DayOfWeek)
                .IsRequired();

            builder.HasMany(x => x.Breaks)
                .WithOne(x => x.BusinessAvailabilitySlot)
                .HasForeignKey(x => x.BusinessAvailabilitySlotId)
                .OnDelete(DeleteBehavior.Cascade);
        }
    }
}
