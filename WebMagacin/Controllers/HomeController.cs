using LimitlessSoft.AspNet.Authorization;
using Microsoft.AspNetCore.Mvc;
using System;

namespace WebMagacin.Controllers
{
    public class HomeController : Controller
    {
        [Route("/")]
        [Authorization("Magacioner", "Administrator")]
        public IActionResult Index()
        {
            return View();
        }

        [Route("/Login")]
        public IActionResult Login()
        {
            return View();
        }

        [Route("/NotAuthorized")]
        public IActionResult NotAuthorized()
        {
            return View();
        }
    }
}
