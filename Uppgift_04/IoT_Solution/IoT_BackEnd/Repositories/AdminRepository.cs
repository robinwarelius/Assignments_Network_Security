using IoT_BackEnd.Data;
using IoT_BackEnd.Models;
using IoT_BackEnd.Repositories.IRepository;
using IoT_BackEnd.Services.IServices;
using System.Linq;

namespace IoT_BackEnd.Repositories
{
    public class AdminRepository : IAdminRepository
    {
        private readonly AppDbContext _db;
        public AdminRepository(AppDbContext db)
        {
            _db = db;
        }

        // Skapa annons (admin)
        public async Task<bool> CreateAdvert(Advertising model)
        {
            _db.Advertisings.Add(model);
            await _db.SaveChangesAsync();
            return true;
        }

        // Hämta senaste annons baserat på datum
        public async Task<Advertising> GetLatestAdvert()
        {
           return _db.Advertisings.OrderByDescending(item => item.CreatedDate).FirstOrDefault()!;                   
        }
    }
}
