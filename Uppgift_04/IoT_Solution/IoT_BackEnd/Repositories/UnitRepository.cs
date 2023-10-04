using IoT_BackEnd.Data;
using IoT_BackEnd.Models;
using IoT_BackEnd.Repositories.IRepository;
using Microsoft.AspNetCore.Razor.TagHelpers;
using Microsoft.EntityFrameworkCore;

namespace IoT_BackEnd.Repositories
{
    public class UnitRepository : IUnitRepository
    {
        private readonly AppDbContext _db;

        public UnitRepository(AppDbContext app)
        {
            _db = app;
        }

        // Skapar enhet
        public async Task<Unit> CreateUnit(Unit unit)
        {          
             _db.Units.Add(unit);
             await _db.SaveChangesAsync();
             return unit;                                    
        }

        // Uppdaterar enhet
        public async Task<Unit> UpdateUnit(Unit unit)
        {
            Unit existingUnit = await _db.Units.FirstOrDefaultAsync(item => item.Name == unit.Name);
            existingUnit.Temperature = unit.Temperature;
            existingUnit.Description = unit.Description;
            existingUnit.Name = unit.Name;
            existingUnit.DateTime = DateTime.Now;
            await _db.SaveChangesAsync();
            return unit;
        }

        // Hämtar senaste skapade enhet
        public async Task<Unit> GetLastCreatedUnit()
        {
            Unit? unit = await _db.Units.OrderByDescending(item => item.DateTime).FirstAsync();
            return unit;
        }

        // Hämta enhet baserat på id
        public async Task<Unit> GetUnitById(int Id)
        {
            Unit? unit = await _db.Units.Where(item => item.UnitId == Id).FirstOrDefaultAsync();
            return unit;
        }
        public async Task<Unit> GetUnitByName(string identifier)
        {
            Unit? unit = await _db.Units.Where(item => item.Name == identifier).FirstOrDefaultAsync();
            return unit;
        }

        public async Task<bool> DeleteUnitById(int Id)
        {
            _db.Units.RemoveRange(_db.Units.Where(item => item.UnitId == Id));
            await _db.SaveChangesAsync();
            return true;

        }
    }
}
