using IoT_FrontEnd.Models;
using IoT_FrontEnd.Models.Dtos;

namespace IoT_Frontend.Services.IServices;

public interface IBaseService
{
    Task<ResponseDto?> SendAsync(RequestDto requestDto, bool withBearer = true);

}
