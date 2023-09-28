using IoT_BackEnd.Models;
using IoT_BackEnd.Models.Dto;

namespace IoT_BackEnd.Utilities
{
    public static class Factory
    {
        public static async Task <UnitDto> Create_UnitDto(string[] values)
        {
            return new UnitDto()
            {
                Name = values[0],
                Description = values[1],
                Temperature = values[2]
            };
        }

        public static async Task<UnitDto> Create_UnitDto(Unit unit)
        {
            return new UnitDto()
            {
                UnitId = unit.UnitId,
                Name = unit.Name,
                Description = unit.Description,
                Temperature = unit.Temperature
            };
        }

        public static async Task<Unit> Create_Unit(UnitDto unitDto)
        {
            return new Unit()
            {
                UnitId = (int)unitDto.UnitId,
                Name = unitDto.Name,
                Description = unitDto.Description,
                Temperature = unitDto.Temperature
            };
        }
    }
}
