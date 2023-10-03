using IoT_BackEnd.Models;
using IoT_BackEnd.Models.Dto;
using IoT_BackEnd.Repositories.IRepository;
using IoT_BackEnd.Services.IServices;

namespace IoT_BackEnd.Services
{
    public class AdminService : IAdminService
    {
        private readonly IAdminRepository _repoAdmin;
        public AdminService(IAdminRepository repoAdmin)
        {
            _repoAdmin = repoAdmin;
        }

        public async Task<bool> CreateAdvert(AdvertDto advertDto)
        {
            if(advertDto != null) 
            {
                return await _repoAdmin.CreateAdvert(new Advertising()
                {
                    Title = advertDto.Title,
                    Description = advertDto.Description,
                    ImageUrl = advertDto.ImageUrl,
                });           
            }

            return false;
        }

        public async Task<Advertising> GetLatestAdvert()
        {
            return await _repoAdmin.GetLatestAdvert();
        }
    }
}
