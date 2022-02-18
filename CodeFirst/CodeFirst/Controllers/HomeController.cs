using CodeFirst.Models;
using CodeFirst.Models.Managers;
using CodeFirst.Models.ViewModels;
using System;
using System.Collections.Generic;
using System.Linq;
using System.Web;
using System.Web.Mvc;

namespace CodeFirst.Controllers
{
    public class HomeController : Controller
    {
        // GET: Home
        public ActionResult homepage()
        {
            DatabaseContext db = new DatabaseContext();
            //List<Kisi> kisiler = db.Kisiler.ToList(); // SELECT * FROM Kisiler
            homepageViewModel model = new homepageViewModel();
            model.Kisiler = db.Kisiler.ToList();
            model.Adresler = db.Adresler.ToList();
            return View(model);
        }
    }
}