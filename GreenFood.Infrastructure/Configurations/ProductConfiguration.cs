using GreenFood.Domain.Models;
using Microsoft.EntityFrameworkCore;
using Microsoft.EntityFrameworkCore.Metadata.Builders;

namespace GreenFood.Infrastructure.Configurations
{
    public class ProductConfiguration : IEntityTypeConfiguration<Product>
    {
        public void Configure(EntityTypeBuilder<Product> builder)
        {
            builder
                .HasOne(p => p.User)
                .WithMany(s => s.Products)
                .OnDelete(DeleteBehavior.NoAction);

            builder
                .HasOne(p => p.Type)
                .WithMany(t => t.Products);
        }
    }
}
