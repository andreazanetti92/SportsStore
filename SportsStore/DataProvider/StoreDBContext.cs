using Microsoft.EntityFrameworkCore;
using SportsStore.Models;

namespace SportsStore.DataProvider
{
    public class StoreDBContext : DbContext
    {
        public StoreDBContext(DbContextOptions<StoreDBContext> options) : base(options) { }

        public DbSet<Product> Products => Set<Product>();
    }
}
