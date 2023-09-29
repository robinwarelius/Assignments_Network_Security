using IoT_BackEnd.Models.Dto;
using IoT_BackEnd.Services.IServices;
using Microsoft.AspNetCore.SignalR;

namespace IoT_BackEnd.Hubs
{
    public class UnitHub : Hub
    {
        private readonly IUnitService _unitService;
        UnitDto _unitDto;
        public static int TotalViews { get; set; } = 0;

        public UnitHub(IUnitService unitService)
        {
            _unitService = unitService;
            _unitDto = new UnitDto();
        }

        public async Task NewWindowLoaded()
        {
            TotalViews++;
            await Clients.All.SendAsync("updateTotalViews", TotalViews);
        }

        public async Task NewUnitValues()
        {
            _unitDto = await _unitService.GetUnitById(1);
            await Clients.All.SendAsync("updateUnit", _unitDto.Name, _unitDto.Description, _unitDto.Temperature);
        }
    }
}
