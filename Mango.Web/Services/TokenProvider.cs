using Mango.Web.Services.IServices;
using Mango.Web.Utility;

namespace Mango.Web.Services;

public class TokenProvider(IHttpContextAccessor httpContextAccessor) : ITokenProvider
{
    public void ClearToken()
    {
        httpContextAccessor.HttpContext?.Response.Cookies.Delete(StaticDetail.TokenCookie);
    }

    public string? GetToken()
    {
        string? token = null;

        bool? hasToken = httpContextAccessor.HttpContext?.Request.Cookies.TryGetValue(StaticDetail.TokenCookie, out token);

        return hasToken is true ? token : null;
    }

    public void SetToken(string token)
    {
        httpContextAccessor.HttpContext?.Response.Cookies.Append(StaticDetail.TokenCookie, token);
    }
}
