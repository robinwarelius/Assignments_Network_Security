using IoT_BackEnd.Models;

namespace IoT_BackEnd.Repositories.IRepository
{
    public interface IAdminRepository
    {
        Task<bool> CreateAdvert(Advertising model);
        Task<Advertising> GetLatestAdvert();
    }
}
