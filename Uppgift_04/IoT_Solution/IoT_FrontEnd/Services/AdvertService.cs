using IoT_FrontEnd.Models;
using IoT_Frontend.Services.IServices;
using IoT_FrontEnd.Models.Dtos;
using IoT_FrontEnd.Services.IServices;
using IoT_FrontEnd.Utilities;

namespace IoT_FrontEnd.Services
{
    public class AdvertService : IAdvertService
    {
        private readonly IBaseService _baseService;

        public AdvertService(IBaseService baseService)
        {
            _baseService = baseService;
        }

        public async Task<ResponseDto> CreateAdvertAsync(AdvertDto advertDto)
        {
            return await _baseService.SendAsync(new RequestDto()
            {
                ApiType = SD.ApiType.POST,
                Data = advertDto,
                Url = $"{SD.AuthApiUrl}/api/admin/CreateAdvert"
            });
        }

        public async Task<ResponseDto> GetLatestAdvertAsync()
        {
            return await _baseService.SendAsync(new RequestDto()
            {
                ApiType = SD.ApiType.GET,
                Url = $"{SD.AuthApiUrl}/api/admin/GetLatestAdvert"
            }, withBearer: false);
        }
    }
}
