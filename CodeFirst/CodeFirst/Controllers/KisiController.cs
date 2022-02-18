using CodeFirst.Models;
using CodeFirst.Models.Managers;
using System;
using System.Collections.Generic;
using System.Data.Entity;
using System.Linq;
using System.Web;
using System.Web.Mvc;

namespace CodeFirst.Controllers
{
    public class KisiController : Controller
    {
        // GET: Kisi
        public ActionResult Yeni()
        {
            return View();
        }

        [HttpPost]
        public ActionResult Yeni(Kisi kisiler)
        {
            DatabaseContext db = new DatabaseContext();
            db.Kisiler.Add(kisiler);
            int sonuc = db.SaveChanges();
            if (sonuc > 0)
            {
                ViewBag.Result = "Kişi Kaydedildi";
                ViewBag.Status = "success";
            }
            else
            {
                ViewBag.Result = "Kayıt Başarısız!";
                ViewBag.Status = "danger";
            }

            return View();
        }

        public ActionResult Duzenle(int? kisiID)
        {
            Kisi kisi = null;
            if(kisiID != null)
            {
                DatabaseContext db = new DatabaseContext();
                kisi = db.Kisiler.Where(x => x.ID == kisiID).FirstOrDefault();
            }
            return View(kisi);
        }

        [HttpPost]
        public ActionResult Duzenle(Kisi model, int? kisiID)
        {
            DatabaseContext db = new DatabaseContext();
            Kisi kisi = db.Kisiler.Where(x => x.ID == kisiID).FirstOrDefault();
            if (kisi != null)
            {
                kisi.Ad = model.Ad;
                kisi.Soyad = model.Soyad;
                kisi.Yas = model.Yas;
                int sonuc = db.SaveChanges(); // update işlemi
                if (sonuc > 0)
                {
                    ViewBag.Result = "Kişi Güncellendi";
                    ViewBag.Status = "success";
                }
                else
                {
                    ViewBag.Result = "Güncelleme Başarısız!";
                    ViewBag.Status = "danger";
                }
            }
            return View();
        }

        public ActionResult Sil(int? kisiID)
        {
            Kisi kisi = null;
            if (kisiID != null)
            {
                DatabaseContext db = new DatabaseContext();
                kisi = db.Kisiler.Where(x => x.ID == kisiID).FirstOrDefault();
            }
            return View(kisi);
        }

        [HttpPost, ActionName("Sil")]
        public ActionResult SilOK(int? kisiID)
        {
            if (kisiID != null)
            {
                DatabaseContext db = new DatabaseContext();
                Kisi kisi = db.Kisiler.Where(x => x.ID == kisiID).FirstOrDefault();

                // Önce ilişkili(alt) tablomuzdaki kayıtları silelim
                List<Adres> adres = db.Adresler.Where(x => x.Kisiler.ID == kisiID).ToList();
                foreach (var item in adres)
                {
                    db.Adresler.Remove(item); // Adresler tablosundaki kişi ID si adres çubuğundan gelen kisiID'ye eşit olan kayıları teker teker sil
                }

                // Kişi verilerini silelim
                db.Kisiler.Remove(kisi);
                db.SaveChanges();
            }
            return RedirectToAction("homepage", "Home");
        }
    }
}