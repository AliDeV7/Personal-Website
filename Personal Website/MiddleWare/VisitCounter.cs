using Microsoft.AspNetCore.Http;
using Microsoft.AspNetCore.Mvc;
using Microsoft.AspNetCore.Mvc.Filters;
using Microsoft.EntityFrameworkCore;
using Personal_Website.Helper;
using Personal_Website.Models;
using System;
using System.Collections.Generic;
using System.Linq;
using System.Threading.Tasks;

namespace Personal_Website.MiddleWare
{
    public class VisitCounter : TypeFilterAttribute
    {
        public VisitCounter() : base(typeof(Counter))
        {
        }

        private class Counter : IAsyncActionFilter
        {
            private readonly IHttpContextAccessor _accessor;
            private readonly PersonlWebsiteDbContext _db;
            public Counter(IHttpContextAccessor _accessor, PersonlWebsiteDbContext _db)
            {
                this._accessor = _accessor;
                this._db = _db;
            }
            public async Task OnActionExecutionAsync(ActionExecutingContext context, ActionExecutionDelegate next)
            {

               await IncreaseVisitCount();
                await next();
            }

            private async Task IncreaseVisitCount()
            {
                var VistorIP = _accessor.HttpContext.Connection.RemoteIpAddress.ToString();
                //var VistorIP = "79.127.83.207";

                var Visitor = await _db.Visitors.Include(x => x.VisitDetails).ThenInclude(x => x.VisitDateDetails).FirstOrDefaultAsync(x => x.IP == VistorIP);
                if (Visitor == null)
                {
                    Visitor = new Visitor() { IP = VistorIP, VisitDetails = new List<VisitDetail>() };

                    var IntIPAddess = IPHelper.IpAddressToInteger(VistorIP);
                    var IPFromCountry = await _db.IPRanges.FirstOrDefaultAsync(x => IntIPAddess >= x.BeginIPAddress && IntIPAddess <= x.EndIPAddress);

                    if (IPFromCountry != null)
                        Visitor.CountryId = IPFromCountry.CountryId;

                    await _db.AddAsync(Visitor);
                }

                var TodayDate = DateTime.UtcNow.Date;
                var VisitDetail = Visitor.VisitDetails.FirstOrDefault(x => x.Date == TodayDate);
                if (VisitDetail == null)
                {
                    VisitDetail = new VisitDetail()
                    {
                        Date = TodayDate,
                        VisitDateDetails = new List<VisitDateDetail>()
                    };
                    Visitor.VisitDetails.Add(VisitDetail);
                }

                var NewVisitDateDetail = new VisitDateDetail() { FullDate = DateTimeOffset.UtcNow.ToUnixTimeSeconds(), };
                VisitDetail.VisitDateDetails.Add(NewVisitDateDetail);

                await _db.SaveChangesAsync();

            }
        }
    }
}
