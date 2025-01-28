using Mango.Web.Models;

namespace Mango.Web.Services.IServices;

public interface IBaseService
{
    Task<ResponseDto?> SendAsync(RequestDto requestDto, bool withBearer = true);
}