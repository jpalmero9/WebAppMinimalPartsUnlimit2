using Microsoft.EntityFrameworkCore;
using WebAppMinimalPartsUnlimit2.Entities.Models;

namespace WebAppMinimalPartsUnlimit2.Entities
{
    public class PartsBdContext : DbContext
    {
        public PartsBdContext(DbContextOptions<PartsBdContext> options) : base(options) { }

        public DbSet<Product> Products { get; set; }
        public DbSet<Category> Categories { get; set; }
    }
}
