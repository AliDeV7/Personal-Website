﻿using Microsoft.AspNetCore.Http;
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
                /// Get Device Ip in accessor

                CookieOptions CookieOptions = new CookieOptions() { Expires = DateTime.Now.AddDays(7) };
                var CountryISO = await CreateVistorCookie(CookieOptions);
                CreateCultureByCountry(CountryISO,CookieOptions);

                await next();
            }

            private void CreateCultureByCountry(string CountryISO, CookieOptions CookieOptions)
            {
                switch (CountryISO.ToUpper())
                {
                    case "IR":
                        _accessor.HttpContext.Response.Cookies.Append(
                            CookieRequestCultureProvider.DefaultCookieName,
                            CookieRequestCultureProvider.MakeCookieValue(new RequestCulture("fa")),
                            CookieOptions);
                        break;
                    case "OTHER":
                        _accessor.HttpContext.Response.Cookies.Append(
                            CookieRequestCultureProvider.DefaultCookieName,
                            CookieRequestCultureProvider.MakeCookieValue(new RequestCulture("en")),
                            CookieOptions);
                        break;
                    default:
                        break;
                }
            }

            private async Task<string> CreateVistorCookie(CookieOptions CookieOptions)
            {
                //var VistorIP = _accessor.HttpContext.Connection.RemoteIpAddress.ToString();
                var VistorIP = "79.127.83.207";
                var IntIPAddess = IPHelper.IpAddressToInteger(VistorIP);
                var IpFromIran = await _db.IPRanges.AnyAsync(x => IntIPAddess >= x.BeginIPAddress && IntIPAddess <= x.EndIPAddress);
                if (IpFromIran)
                {
                    _accessor.HttpContext.Response.Cookies.Append("visitorCountry", "IR", CookieOptions);
                    return "IR";
                }

                else
                {
                    _accessor.HttpContext.Response.Cookies.Append("visitorCountry", "Other", CookieOptions);
                    return "OTHER";
                }

            }
        }
    }
}
