using IoT_BackEnd.Models;
using Microsoft.AspNetCore.Identity.EntityFrameworkCore;
using Microsoft.EntityFrameworkCore;

namespace IoT_BackEnd.Data
{
    public class AppDbContext : IdentityDbContext<ApplicationUser>
    {
        public AppDbContext(DbContextOptions <AppDbContext> options) : base(options)
        {
        }
        public DbSet<Unit> Units { get; set; }
        public DbSet<Advertising> Advertisings { get; set; }
        public DbSet<ApplicationUser> ApplicationUsers { get; set; }
    }
}
