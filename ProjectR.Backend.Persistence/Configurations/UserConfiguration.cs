using Microsoft.EntityFrameworkCore;
using Microsoft.EntityFrameworkCore.Metadata.Builders;
using ProjectR.Backend.Domain;
using ProjectR.Backend.Domain.Entities;
using ProjectR.Backend.Shared.Enums;

namespace ProjectR.Backend.Persistence.Configurations
{
    public class UserConfiguration : IEntityTypeConfiguration<User>
    {
        public void Configure(EntityTypeBuilder<User> builder)
        {
            builder.ToTable("users");

            builder.HasKey(b => b.Id);

            builder.Property(b => b.PhoneNumber)
                .IsRequired();

            builder.Property(b => b.CreatedAt)
           .IsRequired();

            builder.Property(b => b.PhoneCode)
                .IsRequired().HasMaxLength(4);

            builder.Property(b => b.AccountType)
           .IsRequired().HasDefaultValue(AccountType.Business);

            builder.Property(b => b.RegistrationType)
           .IsRequired().HasDefaultValue(RegistrationType.PhoneNumber);
        }
    }
}
