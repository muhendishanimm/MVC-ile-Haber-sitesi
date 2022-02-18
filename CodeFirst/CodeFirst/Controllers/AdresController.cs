using CodeFirst.Models;
using CodeFirst.Models.Managers;
using System;
using System.Collections.Generic;
using System.Linq;
using System.Security.Cryptography;
using System.Web;
using System.Web.Mvc;

namespace CodeFirst.Controllers
{
    public class AdresController : Controller
    {
        // GET: Adres
        public ActionResult Yeni()
        {
            DatabaseContext db = new DatabaseContext();
            // Kişi bilgilerini listeleyip ViewBag içerisinde göndereceğiz - LINQ
            List<SelectListItem> kisilerList =
                (from kisi in db.Kisiler.ToList()
                 select new SelectListItem()
                 {
                     Text = kisi.Ad + " " + kisi.Soyad,
                     Value = kisi.ID.ToString()
                 }).ToList();

            TempData["kisiler"] = kisilerList;
            ViewBag.kisiler = kisilerList;

            return View();
        }

        [HttpPost]
        public ActionResult Yeni(Adres adresler)
        {
            DatabaseContext db = new DatabaseContext();
            Kisi kisiler = db.Kisiler.Where(x => x.ID == adresler.Kisiler.ID).FirstOrDefault();
            if (kisiler != null)
            {
                adresler.Kisiler = kisiler;
                db.Adresler.Add(adresler);
                int sonuc = db.SaveChanges();
                if (sonuc > 0)
                {
                    ViewBag.Result = "Adres Kaydedildi";
                    ViewBag.Status = "success";
                }
                else
                {
                    ViewBag.Result = "Kayıt Başarısız!";
                    ViewBag.Status = "danger";
                }
            }

            ViewBag.kisiler = TempData["kisiler"];
            return View();
        }

        public ActionResult Duzenle(int? adresID)
        {
            Adres adres = null;
            DatabaseContext db = new DatabaseContext();
            adres = db.Adresler.Where(x => x.ID == adresID).FirstOrDefault();
            // Kişi bilgilerini listeleyip ViewBag içerisinde göndereceğiz - LINQ
            List<SelectListItem> kisilerList =
                (from kisi in db.Kisiler.ToList()
                 select new SelectListItem()
                 {
                     Text = kisi.Ad + " " + kisi.Soyad,
                     Value = kisi.ID.ToString(),
                     Selected = (kisi.ID == adres.Kisiler.ID ? true : false)
                 }).ToList();

            TempData["kisiler"] = kisilerList;
            ViewBag.kisiler = kisilerList;
            return View(adres);
        }

        [HttpPost]
        public ActionResult Duzenle(Adres model, int? adresID)
        {
            DatabaseContext db = new DatabaseContext();
            Kisi kisi = db.Kisiler.Where(x => x.ID == model.Kisiler.ID).FirstOrDefault();
            Adres adres = db.Adresler.Where(x => x.ID == adresID).FirstOrDefault();

            if (kisi != null)
            {
                adres.AdresTanim = model.AdresTanim;
                adres.Sehir = model.Sehir;
                adres.Kisiler = kisi;
                int sonuc = db.SaveChanges();
                if (sonuc > 0)
                {
                    ViewBag.Result = "Adres Güncellendi";
                    ViewBag.Status = "success";
                }
                else
                {
                    ViewBag.Result = "Güncelleme Başarısız!";
                    ViewBag.Status = "danger";
                }
            }
            ViewBag.kisiler = TempData["kisiler"];
            return View();
        }

        public ActionResult Sil(int? adresID)
        {
            Adres adres = null;
            if (adresID != null)
            {
                DatabaseContext db = new DatabaseContext();
                adres = db.Adresler.Where(x => x.ID == adresID).FirstOrDefault();
            }
            return View(adres);
        }

        [HttpPost, ActionName("Sil")]
        public ActionResult SilOK(int? adresID)
        {
            if (adresID != null)
            {
                DatabaseContext db = new DatabaseContext();
                Adres adresNesnesi = db.Adresler.Where(x => x.ID == adresID).FirstOrDefault();
                db.Adresler.Remove(adresNesnesi);
                db.SaveChanges();
            }
            return RedirectToAction("homepage", "Home");
        }
    }
}