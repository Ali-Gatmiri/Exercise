using Microsoft.EntityFrameworkCore;
using Microsoft.EntityFrameworkCore.Metadata.Builders;
using Tecsys.Exercise.Infrastructure.Entities;

namespace Tecsys.Exercise.Infrastructure.Persistence.Configurations
{
    public class ProductConfiguration : IEntityTypeConfiguration<Product>
    {
        public void Configure(EntityTypeBuilder<Product> builder)
        {
            builder.HasKey(t => t.ProductID);

            builder.HasOne(t => t.Category);

            builder.Property(t => t.ProductName)
                .HasMaxLength(100)
                .IsRequired();

            builder.Property(t => t.Description)
                .IsRequired();
        }
    }
}