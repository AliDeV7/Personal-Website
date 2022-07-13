using Microsoft.AspNetCore.Http;
using Microsoft.AspNetCore.Localization;
using Microsoft.AspNetCore.Mvc;
using Microsoft.AspNetCore.Mvc.Filters;
using Microsoft.EntityFrameworkCore;
using Personal_Website.Helper;
using Personal_Website.Models;
using System;
using System.Threading.Tasks;

namespace Personal_Website.MiddleWare
{
    public class CheckIP : TypeFilterAttribute
    {
        public CheckIP() : base(typeof(CheckUserIP))
        {
        }

        private class CheckUserIP : IAsyncActionFilter
        {
            private readonly IHttpContextAccessor _accessor;
            private readonly PersonlWebsiteDbContext _db;
            public CheckUserIP(IHttpContextAccessor _accessor, PersonlWebsiteDbContext _db)
            {
                this._accessor = _accessor;
                this._db = _db;
            }
            public async Task OnActionExecutionAsync(ActionExecutingContext context, ActionExecutionDelegate next)
            {
                /// Get User Country by IP
                var CountryISO = await GetVisitorCountry();
                var visitorCountryCookie = _accessor.HttpContext.Request.Cookies["visitorCountry"];

                /// Is it First Time visit or not
                if (string.IsNullOrWhiteSpace(visitorCountryCookie))
                {
                    if (!string.IsNullOrWhiteSpace(CountryISO))
                    {
                        CreateVistorCookie(CountryISO);
                        CreateCultureByCountry(CountryISO);
                    }
                    else
                    {
                        CreateCultureByCountry("OTHER");
                    }
                }
                /// Not first time visit
                else
                {
                    if (!string.IsNullOrWhiteSpace(CountryISO))
                    {
                        if (visitorCountryCookie.ToUpper() != CountryISO.ToUpper())
                            CreateVistorCookie(CountryISO);
                    }
                    else
                    {
                        CreateCultureByCountry("OTHER");
                    }
                }

                await next();
            }

            private void CreateCultureByCountry(string CountryISO)
            {
                string Culture = string.Empty;
                switch (CountryISO.ToUpper())
                {
                    case "IR":
                        Culture = "fa";
                        break;

                    //Arabic Countries
                    case "OM":
                    case "QA":
                    case "AE":
                    case "SY":
                    case "SA":
                    case "LB":
                    case "BH":
                    case "IQ":
                    case "JO":
                    case "KW":
                    case "EG":
                        Culture = "en";
                        break;

                    case "OTHER":
                        Culture = "en";
                        break;

                    default:
                        Culture = "en";
                        break;
                }

                CreateCultureCookies.Create(Culture, _accessor.HttpContext.Response);

            }

            private void CreateVistorCookie(string ISO)
            {
                _accessor.HttpContext.Response.Cookies.Append("visitorCountry", ISO, new CookieOptions() { Expires = DateTimeOffset.UtcNow.AddDays(1) });
            }

            private async Task<string> GetVisitorCountry()
            {
                var VistorIP = _accessor.HttpContext.Connection.RemoteIpAddress.ToString();
                //var VistorIP = "79.127.83.207";
                var IntIPAddess = IPHelper.IpAddressToInteger(VistorIP);
                var IPCountry = await _db.IPRanges.Include(x => x.Country).FirstOrDefaultAsync(x => IntIPAddess >= x.BeginIPAddress && IntIPAddess <= x.EndIPAddress);
                if (IPCountry != null)
                {
                    return IPCountry.Country.ISO;
                }

                else
                {
                    return null;
                }

            }
        }
    }
}
