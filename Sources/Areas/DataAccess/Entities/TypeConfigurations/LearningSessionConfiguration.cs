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

            builder.Property(f => f.SessionName).IsRequired();
            builder.Property(f => f.OneString).IsRequired();
            builder.Property(f => f.AnotherString).IsRequired(false);

            builder.ToTable("LearningSession", "Core");
        }
    }
}