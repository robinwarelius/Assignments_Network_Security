using Microsoft.EntityFrameworkCore;
using ServiceBusConsumer.Models;

namespace ServiceBusConsumer.Data
{
    public class AppDbContext : DbContext
    {
        public AppDbContext(DbContextOptions <AppDbContext> options) : base(options)
        {
        }

        public DbSet<Registration> Registrations { get; set; }
    }
}
