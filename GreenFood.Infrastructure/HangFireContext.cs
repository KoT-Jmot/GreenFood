using Microsoft.EntityFrameworkCore;

namespace GreenFood.Infrastructure
{
    public class HangFireContext : DbContext
    {
        public HangFireContext(DbContextOptions<HangFireContext> options) : base(options)
        {
            Database.EnsureCreated();
        }
    }
}
