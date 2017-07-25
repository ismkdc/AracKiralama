using AracKiralama.Models;
using System;
using System.Collections.Generic;
using System.Linq;
using System.Web;
using System.Web.Mvc;

namespace AracKiralama.Controllers
{
    public class AdminController : Controller
    {
        AccessManager manager;
        // GET: Admin
        public AdminController()
        {
            manager = new AccessManager();
        }
        public ActionResult Index()
        {
            if (Session["login"] == null)
            {
                return RedirectToAction("Login");
            }
            return View(manager.GetAllCars());
        }

        [HttpPost]
        public ActionResult Index(int updateid, string brand, string model, int productionyear, decimal price, HttpPostedFileBase file)
        {
            var car = new Car
            {
                Id = updateid,
                Brand = brand,
                Model = model,
                ProductionYear = productionyear,
                Price = price
            };
            if (file != null)
            {
                file.SaveAs(HttpContext.Server.MapPath("~/Content/")
                                                      + file.FileName);
                car.ImageUrl = $"/Content/{file.FileName}";
            }
            manager.UpdateCar(car);
            return RedirectToAction("Index", manager.GetAllCars());
        }

        public ActionResult Login()
        {
            return View();
        }

        [HttpPost]
        public ActionResult Login(string username, string password)
        {
            if (username == "admin" && password == "password")
            {
                Session["Login"] = "1";
                return RedirectToAction("Index");
            }
            return View();
        }


        public ActionResult AddNewCar()
        {
            return View();
        }

        public ActionResult DeleteCar(int id)
        {
            manager.DeleteCar(id);
            return RedirectToAction("Index", manager.GetAllCars());
        }

        [HttpPost]
        public ActionResult AddNewCar(string brand, string model, int productionyear, decimal price, HttpPostedFileBase file)
        {
            var car = new Car
            {
                Brand = brand,
                Model = model,
                ProductionYear = productionyear,
                Price = price
            };
            if (file != null)
            {
                file.SaveAs(HttpContext.Server.MapPath("~/Content/")
                                                      + file.FileName);
                car.ImageUrl = $"/Content/{file.FileName}";
            }
            manager.InsertCar(car);
            return RedirectToAction("Index", manager.GetAllCars());
        }

    }
}