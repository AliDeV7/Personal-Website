using Microsoft.AspNetCore.Http;
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
                var VistorIP = _accessor.HttpContext.Connection.RemoteIpAddress.ToString();
                var IntIPAddess = IPHelper.IpAddressToInteger(VistorIP);
                var IpFromIran = await _db.iPRanges.AnyAsync(x => IntIPAddess >= x.BeginIPAddress && IntIPAddess <= x.EndIPAddress);
                CookieOptions CookieOptions = new CookieOptions() { Expires = DateTime.Now.AddDays(7) };
                if (IpFromIran) // Is from IRAN
                    _accessor.HttpContext.Response.Cookies.Append("visitorCountry", "IR", CookieOptions);
                
                else
                    _accessor.HttpContext.Response.Cookies.Append("visitorCountry", "Other", CookieOptions);
                
                await next();
            }
        }
    }
}
