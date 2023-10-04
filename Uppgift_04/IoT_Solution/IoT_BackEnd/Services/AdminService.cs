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

        // Inloggad användare (som admin) kan skapa en annons
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

        // Hämtar senaste annonsen baserat på datum
        public async Task<Advertising> GetLatestAdvert()
        {
            return await _repoAdmin.GetLatestAdvert();
        }
    }
}
