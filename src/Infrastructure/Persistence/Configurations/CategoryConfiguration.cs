using Microsoft.EntityFrameworkCore;
using Microsoft.EntityFrameworkCore.Metadata.Builders;
using Tecsys.Exercise.Infrastructure.Entities;

namespace Tecsys.Exercise.Infrastructure.Persistence.Configurations
{
    public class CategoryConfiguration : IEntityTypeConfiguration<Category>
    {
        public void Configure(EntityTypeBuilder<Category> builder)
        {
            builder.HasKey(t => t.CategoryID);

            builder.Property(t => t.CategoryName)
                .HasMaxLength(100)
                .IsRequired();
        }
    }
}