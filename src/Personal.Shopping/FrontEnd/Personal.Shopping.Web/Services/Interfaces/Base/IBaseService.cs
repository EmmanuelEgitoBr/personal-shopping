using Personal.Shopping.Web.Models;

namespace Personal.Shopping.Web.Services.Interfaces.Base;

public interface IBaseService
{
    Task<ResponseDto?> SendAsync(RequestDto requestDto, bool withBearer = true);
}
