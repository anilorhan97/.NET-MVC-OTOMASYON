using System;
using System.Collections.Generic;
using System.Linq;
using System.Web;
using System.Web.Mvc;
using StokSatis.Models.Entity;
using PagedList;
using PagedList.Mvc;


namespace StokSatis.Controllers
{
    public class AlanController : Controller
    {
        MagazamEntities2 db = new MagazamEntities2();
        [Authorize]
        // GET: Kategori
        public ActionResult Index(int sayfa = 1)
        {
            var degerler = db.TBL_ALANLAR.ToList().ToPagedList(sayfa, 10); 
            return View(degerler);
        }
        [HttpGet]
        [Authorize]
        public ActionResult YeniAlan()
        {
            return View();
        }
        [HttpPost]
        public ActionResult YeniAlan(TBL_ALANLAR alanEkle) 
        {
            if (!ModelState.IsValid)
            {
                return View("YeniAlan");
            }
            var alnEkle = db.TBL_ALANLAR.Add(alanEkle);
            db.SaveChanges();
            return RedirectToAction("Index");
        }
        [Authorize]
        public ActionResult alanGetir(int id)
        {
            var aln = db.TBL_ALANLAR.Find(id);
            return View("alanGetir", aln);
        }
        public ActionResult Guncelle(TBL_ALANLAR guncellenen) 
        {
            var alan = db.TBL_ALANLAR.Find(guncellenen.ALANID); 
            alan.ALANAD = guncellenen.ALANAD;
            db.SaveChanges();
            return RedirectToAction("Index");
        }

        public ActionResult Sil(int id)
        {
            var alanSil = db.TBL_ALANLAR.Find(id);
            db.TBL_ALANLAR.Remove(alanSil);
            db.SaveChanges();
            return RedirectToAction("Index");
        }
    }
}