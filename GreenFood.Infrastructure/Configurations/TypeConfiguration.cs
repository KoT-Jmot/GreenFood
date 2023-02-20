using GreenFood.Domain.Models;
using Microsoft.EntityFrameworkCore;
using Microsoft.EntityFrameworkCore.Metadata.Builders;

namespace GreenFood.Infrastructure.Configurations
{
    public class TypeConfiguration : IEntityTypeConfiguration<TypeOfProduct>
    {
        public void Configure(EntityTypeBuilder<TypeOfProduct> builder)
        {
            builder
                .HasKey(t => t.Id);
        }
    }
}
