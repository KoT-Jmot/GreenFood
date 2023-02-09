﻿using GreenFood.Domain.Models;
using Microsoft.EntityFrameworkCore;
using Microsoft.EntityFrameworkCore.Metadata.Builders;

namespace GreenFood.Infrastructure.Configurations
{
    public class OrderConfiguration : IEntityTypeConfiguration<Order>
    {
        public void Configure(EntityTypeBuilder<Order> builder)
        {
            builder
                .HasOne(o => o.User)
                .WithMany(u=>u.Orders).OnDelete(DeleteBehavior.NoAction);

            builder
                .HasOne(o => o.Product)
                .WithMany(p=>p.Orders);
        }
    }
}
