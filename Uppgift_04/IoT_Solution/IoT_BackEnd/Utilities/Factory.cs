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
    }
}
