using Microsoft.EntityFrameworkCore;
using Microsoft.EntityFrameworkCore.Metadata.Builders;
using ProjectR.Backend.Domain.Entities;

namespace ProjectR.Backend.Persistence.Configurations
{
    public class BreakConfiguration : IEntityTypeConfiguration<Break>
    {
        public void Configure(EntityTypeBuilder<Break> builder)
        {
            builder.HasKey(x => x.Id);

            builder.Property(x => x.BusinessAvailabilitySlotId)
                .IsRequired();

            builder.HasOne(x => x.BusinessAvailabilitySlot)
                .WithMany(x => x.Breaks)
                .HasForeignKey(x => x.BusinessAvailabilitySlotId)
                .OnDelete(DeleteBehavior.Cascade);

            builder.Property(x => x.StartTime)
                .IsRequired();

            builder.Property(x => x.EndTime)
                .IsRequired();
        }
    }
}
