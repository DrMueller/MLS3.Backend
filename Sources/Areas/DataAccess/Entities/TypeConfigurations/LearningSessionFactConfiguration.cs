using Microsoft.EntityFrameworkCore;
using Microsoft.EntityFrameworkCore.Metadata.Builders;

namespace Mmu.Mls3.WebApi.Areas.DataAccess.Entities.TypeConfigurations
{
    public class LearningSessionFactConfiguration : IEntityTypeConfiguration<LearningSessionFact>
    {
        public void Configure(EntityTypeBuilder<LearningSessionFact> builder)
        {
            builder.HasKey(f => new { f.FactId, f.LearningSessionId });

            builder
                .HasOne(f => f.Fact)
                .WithMany(f => f.LearningSessionFacts)
                .HasForeignKey(f => f.FactId)
                .IsRequired();

            builder
                .HasOne(f => f.LearningSession)
                .WithMany(f => f.LearningSessionFacts)
                .HasForeignKey(f => f.LearningSessionId)
                .IsRequired();

            builder.ToTable("LearningSessionFact", "Core");
        }
    }
}