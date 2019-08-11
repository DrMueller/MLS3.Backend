using Microsoft.EntityFrameworkCore;
using Microsoft.EntityFrameworkCore.Metadata.Builders;

namespace Mmu.Mls3.WebApi.Infrastructure.Security.DataAccess.Entities.TypeConfigurations
{
    public class AppUserConfiguration : IEntityTypeConfiguration<AppUser>
    {
        public void Configure(EntityTypeBuilder<AppUser> builder)
        {
            builder.HasKey(f => f.Id);
            builder.Property(f => f.Id).IsRequired().ValueGeneratedOnAdd();

            builder.Property(f => f.NormalizedUserName).IsRequired().HasMaxLength(256);
            builder.Property(f => f.UserName).IsRequired().HasMaxLength(256);
            builder.Property(f => f.HashedPassword).IsRequired().HasMaxLength(256);

            builder.ToTable("AppUser", "Security");
        }
    }
}