using Microsoft.EntityFrameworkCore;
using Microsoft.EntityFrameworkCore.Metadata.Builders;

namespace Mmu.Mls3.WebApi.Areas.DataAccess.Entities.TypeConfigurations
{
    public class FactConfiguration : IEntityTypeConfiguration<Fact>
    {
        public void Configure(EntityTypeBuilder<Fact> builder)
        {
            builder.HasKey(f => f.Id);
            builder.Property(f => f.Id).IsRequired().ValueGeneratedOnAdd();

            builder.Property(f => f.AnswerText).IsRequired();
            builder.Property(f => f.CreationDate).IsRequired();
            builder.Property(f => f.QuestionText).IsRequired();

            builder.ToTable("Fact", "Core");
        }
    }
}