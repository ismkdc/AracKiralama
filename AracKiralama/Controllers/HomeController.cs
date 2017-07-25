using AracKiralama.Models;
using System;
using System.Collections.Generic;
using System.Linq;
using System.Web;
using System.Web.Mvc;

namespace AracKiralama.Controllers
{
    public class HomeController : Controller
    {
        AccessManager manager;
        public HomeController()
        {
            manager = new AccessManager();
        }
        // GET: Home
        public ActionResult Index()
        {
            return View(manager.GetAllCars());
        }
    }
}