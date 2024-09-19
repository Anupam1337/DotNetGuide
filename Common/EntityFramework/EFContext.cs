using Microsoft.EntityFrameworkCore;

namespace Common.EntityFramework
{
    public class EFContext : DbContext
    {
        public EFContext(DbContextOptions<EFContext> options) : base(options)
        {
        }

        protected override void OnModelCreating(ModelBuilder modelBuilder)
        {
            // Configure EF models here
        }
    }
}
