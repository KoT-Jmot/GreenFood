using GreenFood.Domain.Models;
using Microsoft.EntityFrameworkCore;

namespace GreenFood.Infrastructure
{
    public class HangFireContext : DbContext
    {

        public HangFireContext(DbContextOptions<HangFireContext> options) : base(options)
        {
            Database.EnsureCreated();
        }
        protected override void OnModelCreating(ModelBuilder modelBuilder)
        {
            base.OnModelCreating(modelBuilder);
        }
    }
}
