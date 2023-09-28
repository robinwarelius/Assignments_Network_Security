using IoT_BackEnd.Models;
using System.Threading.Tasks;

namespace IoT_BackEnd.Repositories.IRepository
{
    public interface IUnitRepository
    {
        Task<Unit> CreateUnit(Unit unit);
        Task<Unit> UpdateUnit(Unit unit);
        Task <Unit> GetUnitByName(string identifier);
        Task<Unit> GetUnitById(int Id);
        Task<bool> DeleteUnitById(int Id);
    }
}
