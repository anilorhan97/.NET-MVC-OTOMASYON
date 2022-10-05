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
    
    public class KategoriController : Controller
    {
        MagazamEntities2 db = new MagazamEntities2();
        [Authorize]
        // GET: Kategori
        public ActionResult Index(int sayfa=1)
        {
            var degerler = db.TBL_KATEGORILER.ToList().ToPagedList(sayfa, 10);  //İlk değer sayfa numarası nereden başlasın. 2. değer kaç tane öğe olmasını soruyor..
            return View(degerler);
        }
        [HttpGet]
        [Authorize]
        public ActionResult YeniKategori()
        {
            return View();
        }
        [HttpPost]
        public ActionResult YeniKategori(TBL_KATEGORILER kategoriEkle) //Kategoriye ulaşmak için parametre verdik.
        {
            if (!ModelState.IsValid)
            {
                return View("YeniKategori");
            }
            var ktgEkle = db.TBL_KATEGORILER.Add(kategoriEkle);
            db.SaveChanges();
            return RedirectToAction("Index");
        }
        [Authorize]
        public ActionResult kategoriGetir(int id) //Bu kısıma add viewe basıp kategoriGetir adında bir sayfa oluşturduk. Bulunan id'e göre oradaki verileri gategoriGetir'e getirecek. KTGR'DE ki verielri..
        {
            var ktgr = db.TBL_KATEGORILER.Find(id);
            return View("kategoriGetir", ktgr);
        }
        public ActionResult Guncelle(TBL_KATEGORILER guncellenen) //TBLKATEGORİDE GÜNCELLEME YAPACAK. p1 ile PARAMETRE ALDIK..
        {
            var kategori = db.TBL_KATEGORILER.Find(guncellenen.KATEGORIID); //guncellenenden'den gelecek kategoriid bulduracağız.
            kategori.KATEGORIAD = guncellenen.KATEGORIAD; //guncellenen.KATEGORIAD olan değer.. p1 yani kullanıcıdan alacağımız KATEGORIAD'ı mevcut değere eşitle yani yerine onu yap der..
            db.SaveChanges();
            return RedirectToAction("Index");
        }

        public ActionResult Sil(int id)
        {
            var katSil = db.TBL_KATEGORILER.Find(id);
            db.TBL_KATEGORILER.Remove(katSil);
            db.SaveChanges();
            return RedirectToAction("Index");
        }

    }
}