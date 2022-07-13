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
                var visitorDirectionCookie = _accessor.HttpContext.Request.Cookies["visitorDirection"];

                /// Is it First Time visit or not
                if (string.IsNullOrWhiteSpace(visitorDirectionCookie))
                    CreateCultureDirectionByCountry(CountryISO);
                
                /// Not first time visit
                else
                {
                    ///// Trigger something for user to check if want to continue with last language
                    //if (!string.IsNullOrWhiteSpace(CountryISO))
                    //{
                    //    //var CultureModel = GetCultureModelByCountry(CountryISO);

                    //}
                }

                await next();
            }

            private void CreateCultureDirectionByCountry(string CountryISO)
            {
                var CultureModel = GetCultureModelByCountry(CountryISO);
                CreateCultureCookies.Create(CultureModel.Culture, _accessor.HttpContext.Response);
                CreateCultureCookies.CreateDirection(CultureModel.Direction, _accessor.HttpContext.Response);
            }

            public CultureModel GetCultureModelByCountry(string CountryISO)
            {
                string Culture;
                string Direction;
                switch (CountryISO)
                {
                    case "IR":
                        Culture = "fa";
                        Direction = "rtl";
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
                        Direction = "rtl";
                        break;

                    case "OTHER":
                        Culture = "en";
                        Direction = "ltr";
                        break;

                    case null:
                        Culture = "en";
                        Direction = "ltr";
                        break;

                    default:
                        Culture = "en";
                        Direction = "ltr";
                        break;
                }

                return new CultureModel() { Direction = Direction, Culture = Culture };
            }

            private async Task<string> GetVisitorCountry()
            {
                var VistorIP = _accessor.HttpContext.Connection.RemoteIpAddress.ToString();
                //var VistorIP = "79.127.83.207";
                var IntIPAddess = IPHelper.IpAddressToInteger(VistorIP);
                var IPCountry = await _db.IPRanges.Include(x => x.Country).FirstOrDefaultAsync(x => IntIPAddess >= x.BeginIPAddress && IntIPAddess <= x.EndIPAddress);
                if (IPCountry != null)
                    return IPCountry.Country.ISO;

                else
                    return null;
            }
            public class CultureModel
            {
                public string Culture { get; set; }
                public string Direction { get; set; }
            }
        }
    }
}
