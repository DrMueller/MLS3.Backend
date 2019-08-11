using Microsoft.EntityFrameworkCore;
using Microsoft.EntityFrameworkCore.Metadata.Builders;

namespace Mmu.Mls3.WebApi.Areas.DataAccess.Entities.TypeConfigurations
{
    public class LearningSessionConfiguration : IEntityTypeConfiguration<LearningSession>
    {
        public void Configure(EntityTypeBuilder<LearningSession> builder)
        {
            builder.HasKey(f => f.Id);
            builder.Property(f => f.Id).IsRequired().ValueGeneratedOnAdd();

            builder.Property(f => f.SessionName).IsRequired().HasMaxLength(256);

            builder.ToTable("LearningSession", "Core");
        }
    }
}