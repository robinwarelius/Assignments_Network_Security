using IoT_FrontEnd.Models.Dtos;

namespace IoT_FrontEnd.Services.IServices
{
    public interface IAdvertService
    {
        Task<ResponseDto> CreateAdvertAsync(AdvertDto advertDto);
        Task<ResponseDto> GetLatestAdvertAsync();
    }
}
