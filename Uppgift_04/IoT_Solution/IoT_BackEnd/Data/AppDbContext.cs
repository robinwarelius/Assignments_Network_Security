using IoT_BackEnd.Models;
using Microsoft.EntityFrameworkCore;

namespace IoT_BackEnd.Data
{
    public class AppDbContext : DbContext
    {
        public AppDbContext(DbContextOptions <AppDbContext> options) : base(options)
        {
        }

        public DbSet<Unit> Units { get; set; }
    }
}
