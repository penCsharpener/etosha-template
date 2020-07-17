using Microsoft.EntityFrameworkCore;
using Microsoft.EntityFrameworkCore.Metadata.Builders;
using Template.Persistence.Entities;

namespace Template.Persistence.Configurations
{
    public class AppUserTypeConfiguration : IEntityTypeConfiguration<AppUser>
    {
        public void Configure(EntityTypeBuilder<AppUser> builder)
        {
            builder.HasKey(p => p.Id);
            builder.Property(p => p.FirstName).IsRequired().HasMaxLength(256);
            builder.Property(p => p.FirstName).IsRequired().HasMaxLength(256);
            builder.Property(p => p.Email).IsRequired();
            builder.Property(p => p.NormalizedEmail).IsRequired();
            builder.Property(p => p.UserName).IsRequired();
            builder.Property(p => p.NormalizedUserName).IsRequired();
        }
    }
}
