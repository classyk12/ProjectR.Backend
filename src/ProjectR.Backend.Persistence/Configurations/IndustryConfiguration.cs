using Microsoft.EntityFrameworkCore;
using Microsoft.EntityFrameworkCore.Metadata.Builders;
using ProjectR.Backend.Domain;
using ProjectR.Backend.Domain.Entities;
using ProjectR.Backend.Shared.Enums;

namespace ProjectR.Backend.Persistence.Configurations
{
    public class IndustryConfiguration : IEntityTypeConfiguration<Industry>
    {
        public void Configure(EntityTypeBuilder<Industry> builder)
        {
            builder.HasKey(i => i.Id);
            builder.Property(i => i.Name)
                .IsRequired()
                .HasMaxLength(100);
            builder.Property(i => i.Description)
                .HasMaxLength(500);
        }
    }
}
