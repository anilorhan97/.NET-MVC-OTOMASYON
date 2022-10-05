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
    public class MusteriController : Controller
    {
        MagazamEntities2 db = new MagazamEntities2();
        [Authorize]
        // GET: Musteri
        public ActionResult Index(int sayfa=1)
        {
            var degerler = db.TBL_MUSTERILER.ToList().ToPagedList(sayfa, 10);
            return View(degerler);
        }

        public ActionResult Sil(int id)
        {
            var musteriSil = db.TBL_MUSTERILER.Find(id);
            db.TBL_MUSTERILER.Remove(musteriSil);
            db.SaveChanges();
            return RedirectToAction("Index");
        }

        [HttpGet] //httpget ile Viewi dönüdrmesini istiyoruz.
        [Authorize]
        public ActionResult MusteriEkle()
        {
            return View();
        }
        [HttpPost] //İşlem olacaksa http post çalışır.
        public ActionResult MusteriEkle(TBL_MUSTERILER ekle)
        {
            if (!ModelState.IsValid) //Artık if ile böyle kotnrol edilir. Modeldeki değere göre duruma göre musteriekle view döndürür.
            {
                return View("MusteriEkle");
            }
            var mus = db.TBL_MUSTERILER.Add(ekle);
            db.SaveChanges();
            return RedirectToAction("Index");
        }
        [Authorize]
        public ActionResult musteriGetir(int id)
        {
            var mGetir = db.TBL_MUSTERILER.Find(id);
            return View("musteriGetir", mGetir);
        }

        public ActionResult musteriGuncelle(TBL_MUSTERILER guncellenen)
        {
            var mGuncelle = db.TBL_MUSTERILER.Find(guncellenen.MUSTERIID);  //p1'den gelecek musteriid bulduracak
            mGuncelle.MUSTERIAD = guncellenen.MUSTERIAD;
            mGuncelle.MUSTERISOYAD = guncellenen.MUSTERISOYAD;
            db.SaveChanges();
            return RedirectToAction("Index");
        }
    }
}