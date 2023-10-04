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
        public static int TotalConnections { get; set; } = 0;

        public UnitHub(IUnitService unitService)
        {
            _unitService = unitService;
            _unitDto = new UnitDto();
        }

        // Skickar data till frontend systemet om antal online (när något connectar)
        public override Task OnConnectedAsync()
        {
            TotalConnections++;
            Clients.All.SendAsync("updateTotalConnections", TotalConnections).GetAwaiter().GetResult();
            return base.OnConnectedAsync();
        }

        // Skickar data till frontend systemet om antal online (när något disconnectar)
        public override Task OnDisconnectedAsync(Exception? exception)
        {
            TotalConnections--;
            Clients.All.SendAsync("updateTotalConnections", TotalConnections).GetAwaiter().GetResult();
            return base.OnDisconnectedAsync(exception);
        }

        // Skickar data till frontend om antal visningar av sidan
        public async Task NewWindowLoaded()
        {
            TotalViews++;
            await Clients.All.SendAsync("updateTotalViews", TotalViews);
        }

        // Vid applikations start skickar jag den senaste skapade simulerade enheten till frontend systemet
        public async Task NewUnitValues()
        {
            _unitDto = await _unitService.GetLastCreatedUnit();
            await Clients.All.SendAsync("updateUnit", _unitDto.Name, _unitDto.Description, _unitDto.Temperature);
        }
    }
}
