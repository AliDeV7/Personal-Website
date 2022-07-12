using System;
using System.Collections.Generic;
using System.Diagnostics;
using System.Linq;
using System.Threading.Tasks;
using Microsoft.AspNetCore.Http;
using Microsoft.AspNetCore.Localization;
using Microsoft.AspNetCore.Mvc;
using Microsoft.Extensions.Logging;
using Personal_Website.Helper;
using Personal_Website.MiddleWare;
using Personal_Website.Models;
using Personal_Website.ViewModel;

namespace Personal_Website.Controllers
{
    [VisitCounter]
    public class HomeController : Controller
    {
        private readonly ILogger<HomeController> _logger;
        private readonly PersonlWebsiteDbContext _db;
        private string cultureName;
        public HomeController(ILogger<HomeController> logger, PersonlWebsiteDbContext db)
        {
            _logger = logger;
            cultureName = System.Threading.Thread.CurrentThread.CurrentUICulture.Name.Substring(startIndex: 0, length: 2).ToLower();
            _db = db;
        }

        [CheckIP]
        public IActionResult Start()
        {
            return RedirectToAction("Home");
        }
        [Route("Home")]
        public IActionResult Home()
        {
            switch (cultureName)
            {
                case "fa":
                case "ar":
                    {
                        return View("Rtl/Home");
                    }

                default:
                    {
                        return View("Ltr/Home");
                    }
            }
        }

        [Route("Resume")]
        public IActionResult Resume()
        {
            switch (cultureName)
            {
                case "fa":
                case "ar":
                    {
                        return View("Rtl/Resume");
                    }

                default:
                    {
                        return View("Ltr/Resume");
                    }
            }
        }

        //public IActionResult Blog()
        //{
        //    switch (cultureName)
        //    {
        //        case "fa":
        //        case "ar":
        //            {
        //                return View("Rtl/Blog");
        //            }

        //        default:
        //            {
        //                return View("Ltr/Blog");
        //            }
        //    }
        //}

        [Route("Contact")]
        public IActionResult Contact()
        {
            switch (cultureName)
            {
                case "fa":
                case "ar":
                    {
                        return View("Rtl/Contact");
                    }

                default:
                    {
                        return View("Ltr/Contact");
                    }
            }
        }

        [Route("Projects")]
        public IActionResult Projects()
        {
            switch (cultureName)
            {
                case "fa":
                case "ar":
                    {
                        return View("Rtl/Projects");
                    }

                default:
                    {
                        return View("Ltr/Projects");
                    }
            }
        }
        public IActionResult SelectedPage(string page)
        {
            switch (page)
            {
                case "home":
                    return RedirectToAction("Home");
                case "resume":
                    return RedirectToAction("Resume");
                case "contact":
                    return RedirectToAction("Contact");
                case "projects":
                    return RedirectToAction("Projects");
                case "blog":
                    return RedirectToAction("Blog");
                default:
                    return RedirectToAction("Home");
            }
        }

        [Route("Home/SendTicket")]
        public async Task<IActionResult> SendTicket(TicketViewModel model)
        {
            try
            {
                var NewTicket = new Ticket()
                {
                    Email = model.Email,
                    Name = model.Name,
                    Text = model.message,
                    Status = TicketStatus.Open
                };
                await _db.AddAsync(NewTicket);
                await _db.SaveChangesAsync();

                // TODO : Add Successful Message for User
            }
            catch (Exception)
            {
                // TODO : Add Error Message
                throw;
            }
            return RedirectToAction("Contact");
        }
        public IActionResult ChangeLanguage(string lang, string returnURL)
        {
            CreateCultureCookies.Create(lang, Response);
            return Redirect(returnURL);
        }

        public IActionResult Error404()
        {
            switch (cultureName)
            {
                case "fa":
                case "ar":
                    {
                        return View("Rtl/Error404");
                    }

                default:
                    {
                        return View("Ltr/Error404");
                    }
            }
        }
    }
}
