using IoT_BackEnd.Models;
using IoT_BackEnd.Models.Dto;

namespace IoT_BackEnd.Services.IServices
{
    public interface IAdminService
    {
        Task <bool> CreateAdvert(AdvertDto advertDto);
        Task<Advertising> GetLatestAdvert();
    }
}
