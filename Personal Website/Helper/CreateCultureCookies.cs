using Microsoft.AspNetCore.Http;
using Microsoft.AspNetCore.Localization;
using System;

namespace Personal_Website.Helper
{
    public static class CreateCultureCookies
    {
        public static void Create(string culture, HttpResponse Response)
        {
            Response.Cookies.Append(
                    CookieRequestCultureProvider.DefaultCookieName,
                    CookieRequestCultureProvider.MakeCookieValue(new RequestCulture(culture)),
                    new CookieOptions { Expires = DateTimeOffset.UtcNow.AddYears(7) });
        }
    }
}
