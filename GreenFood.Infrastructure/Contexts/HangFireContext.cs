using Microsoft.EntityFrameworkCore;

namespace GreenFood.Infrastructure.Contexts
{
    public class HangFireContext : DbContext
    {
        public HangFireContext(DbContextOptions<HangFireContext> options) : base(options)
        {
        }

        protected override void OnModelCreating(ModelBuilder modelBuilder)
        {
            base.OnModelCreating(modelBuilder);
        }
    }
}
