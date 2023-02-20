using HardCode.WebAPI.Entities;
using Microsoft.EntityFrameworkCore;

namespace HardCode.WebAPI.Data
{
    public class HardCodeContext : DbContext
    {
        public HardCodeContext(DbContextOptions<HardCodeContext> options)
            : base(options)
        {

        }

        public DbSet<Product> Products { get; set; }
        public DbSet<Category> Categories { get; set; }
        public DbSet<Image> Images { get; set; }

        protected override void OnModelCreating(ModelBuilder modelBuilder)
        {
            base.OnModelCreating(modelBuilder);
        }
    }
}
