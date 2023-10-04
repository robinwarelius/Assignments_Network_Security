using IoT_BackEnd.Models;
using IoT_BackEnd.Models.Dto;
using IoT_BackEnd.Repositories.IRepository;
using IoT_BackEnd.Services.IServices;
using IoT_BackEnd.Utilities;

namespace IoT_BackEnd.Services
{
    public class UnitService : IUnitService
    {
        private readonly IUnitRepository _repo;

        public UnitService(IUnitRepository repo)
        {
            _repo = repo;
        }

        // Skickar simulerad enhet till repo som sen ansvarar för databaskommunikationen
        public async Task<UnitDto> CreateUnit(UnitDto? unitDto)
        {
            if(unitDto != null)
            {
                Unit matchingUnit = await _repo.GetUnitByName(unitDto.Name);
                if(matchingUnit != null)
                {
                    unitDto.UnitId = matchingUnit.UnitId;
                    Unit updatedUnit = await _repo.UpdateUnit(await Factory.Create_Unit(unitDto));

                    if(updatedUnit != null)
                    {
                        return await Factory.Create_UnitDto(updatedUnit);
                    }
                }
                else
                {                   
                    Unit createUnit = await Factory.Create_Unit(unitDto);
                    Unit createdUnit = await _repo.CreateUnit(createUnit);
                    if (createdUnit != null)
                    {
                        return await Factory.Create_UnitDto(createdUnit);
                    }
                   
                }
            }
            return null;                         
        }


        //<--**--> Metoderna nedanför används ej <--**-->

        public async Task<bool> DeleteUnitById(int Id)
        {
            bool isSuccess = await _repo.DeleteUnitById(Id);
            return isSuccess;
        }

        public async Task<UnitDto> GetUnitById(int Id)
        {          
            Unit unit = await _repo.GetUnitById(Id);
            if(unit != null)
            {
                return await Factory.Create_UnitDto(unit);
            }
            return null;              
        }

        public async Task<UnitDto> GetLastCreatedUnit()
        {
            Unit unit = await _repo.GetLastCreatedUnit();
            if (unit != null)
            {
                return await Factory.Create_UnitDto(unit);
            }
            return null;
        }

        public async Task<UnitDto> GetUnitByName(string identifier)
        {
            Unit unit = await _repo.GetUnitByName(identifier);
            if (unit != null)
            {
                return await Factory.Create_UnitDto(unit);
            }
            return null;
        }
    }
}
