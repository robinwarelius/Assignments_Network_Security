using IoT_BackEnd.Models;
using IoT_BackEnd.Models.Dto;

namespace IoT_BackEnd.Services.IServices
{
    public interface IUnitService
    {
        Task<UnitDto> CreateUnit(UnitDto unitDto);
        Task<UnitDto> GetUnitByName (string identifier);
        Task<UnitDto> GetUnitById(int Id);
        Task<UnitDto> GetLastCreatedUnit();
        Task<bool> DeleteUnitById (int Id);     
    }
}
