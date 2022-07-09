using System;
using System.Collections.Generic;
using System.Diagnostics;
using System.Linq;
using System.Threading.Tasks;
using Microsoft.AspNetCore.Mvc;
using Microsoft.Extensions.Logging;
using Personal_Website.MiddleWare;
using Personal_Website.Models;

namespace Personal_Website.Controllers
{
    [CheckIP]
    [VisitCounter]
    public class HomeController : Controller
    {
        private readonly ILogger<HomeController> _logger;
        private string cultureName;
        public HomeController(ILogger<HomeController> logger)
        {
            _logger = logger;
            cultureName = System.Threading.Thread.CurrentThread.CurrentUICulture.Name.Substring(startIndex: 0, length: 2).ToLower();
        }

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

        public IActionResult Blog()
        {
            switch (cultureName)
            {
                case "fa":
                case "ar":
                    {
                        return View("Rtl/Blog");
                    }

                default:
                    {
                        return View("Ltr/Blog");
                    }
            }
        }

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
                    return  RedirectToAction("Home");
            }
        }

        [ResponseCache(Duration = 0, Location = ResponseCacheLocation.None, NoStore = true)]
        public IActionResult Error()
        {
            return View(new ErrorViewModel { RequestId = Activity.Current?.Id ?? HttpContext.TraceIdentifier });
        }
    }
}
