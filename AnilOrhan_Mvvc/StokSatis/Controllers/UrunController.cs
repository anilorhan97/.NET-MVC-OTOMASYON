using System;
using System.Collections.Generic;
using System.Linq;
using System.Web;
using System.Web.Mvc;
using StokSatis.Models.Entity;
using PagedList;
using PagedList.Mvc;
using System.IO;

namespace StokSatis.Controllers
{
    public class UrunController : Controller
    {
        
        MagazamEntities2 db = new MagazamEntities2();
        [Authorize]
        // GET: Urunler
        public ActionResult Index(int sayfa = 1)
        {
            var degerler = db.TBL_URUNLER.ToList().ToPagedList(sayfa, 7);
            return View(degerler);
        }
        [HttpGet]
        [Authorize]
        public ActionResult UrunEkle()
        {
            List<SelectListItem> kategori = (from i in db.TBL_KATEGORILER.ToList()
                                             select new SelectListItem
                                             {
                                                 Text = i.KATEGORIAD,
                                                 Value = i.KATEGORIID.ToString()
                                             }
                                         ).ToList();
            List<SelectListItem> alan = (from i in db.TBL_ALANLAR.ToList()
                                         select new SelectListItem
                                         {
                                             Text = i.ALANAD,
                                             Value = i.ALANID.ToString()
                                         }
                                          ).ToList();

            ViewBag.kategorii = kategori;
            ViewBag.alann = alan;
            return View();
        }
        [HttpPost]
        public ActionResult UrunEkle(TBL_URUNLER urunEkle,HttpPostedFileBase File ) //Görsel eklemek için httppostedfilebase file kullanılır
        {
            if (urunEkle.URUNAD == null || urunEkle.URUNFIYAT == null || urunEkle.MARKA == null || urunEkle.STOK == null)
            {
                List<SelectListItem> kategori = (from i in db.TBL_KATEGORILER.ToList()
                                              select new SelectListItem
                                              {
                                                  Text = i.KATEGORIAD,
                                                  Value = i.KATEGORIID.ToString()
                                              }
                                          ).ToList();
                List<SelectListItem> alan = (from i in db.TBL_ALANLAR.ToList()
                                             select new SelectListItem
                                             {
                                                 Text = i.ALANAD,
                                                 Value = i.ALANID.ToString()
                                             }
                              ).ToList();
          


                ViewBag.kategorii = kategori; //dgr diğer sayfada kullanılacak.. Üst taraftan gelecek değere eşitliyoruz.
                ViewBag.alann = alan;
                return View();
            }
            else
            { // => öyle ki demek.. Kategoriid p1,tblkategoriye eşit ki.. Firstordefault ile ilk parametreyi al.. ve ktgr değişkenine at.
                var ktgr = db.TBL_KATEGORILER.Where(m => m.KATEGORIID == urunEkle.TBL_KATEGORILER.KATEGORIID).FirstOrDefault();
                urunEkle.TBL_KATEGORILER = ktgr;
                var urnalan = db.TBL_ALANLAR.Where(m => m.ALANID == urunEkle.TBL_ALANLAR.ALANID).FirstOrDefault();
                urunEkle.TBL_ALANLAR = urnalan;
                var urun = db.TBL_URUNLER.Add(urunEkle);
                string path = Path.Combine("~/Content/Image" + File.FileName);
                File.SaveAs(Server.MapPath(path)); //path değişkeninden gelen yolu kaydetmek için
                urunEkle.RESIMM = File.FileName.ToString();
                db.TBL_URUNLER.Add(urunEkle);
                db.SaveChanges();
                return RedirectToAction("Index");
            }
        }

        public ActionResult Sil(int id)
        {
            var urunSil = db.TBL_URUNLER.Find(id);
            db.TBL_URUNLER.Remove(urunSil);
            db.SaveChanges();
            return RedirectToAction("Index");
        }
        [Authorize]
        [HttpGet]
        public ActionResult UrunGetir(int id)
        {
            var bul = db.TBL_URUNLER.Where(m=>m.URUNID==id).FirstOrDefault();
            {
                List<SelectListItem> deger2 = (from i in db.TBL_KATEGORILER.ToList()
                                              select new SelectListItem
                                              {
                                                  Text = i.KATEGORIAD,
                                                  Value = i.KATEGORIID.ToString()
                                              }
                                          ).ToList();
                List<SelectListItem> alan2 = (from i in db.TBL_ALANLAR.ToList()
                                              select new SelectListItem
                                              {
                                                  Text = i.ALANAD,
                                                  Value = i.ALANID.ToString()
                                              }
                                         ).ToList();
                ViewBag.degerr2 = deger2;
                ViewBag.alann2 = alan2;
                return View("UrunGetir", bul);
            }
        }
        [HttpPost]
        public ActionResult UrunGetir(TBL_URUNLER guncellenen, HttpPostedFileBase File)
        {
            var ur = db.TBL_URUNLER.Find(guncellenen.URUNID);

            if(File==null)
            {
                ur.URUNAD = guncellenen.URUNAD;
                ur.MARKA = guncellenen.MARKA;
                var ktgr = db.TBL_KATEGORILER.Where(m => m.KATEGORIID == guncellenen.TBL_KATEGORILER.KATEGORIID).FirstOrDefault();
                ur.URUNKATEGORI = ktgr.KATEGORIID;
                var aln = db.TBL_ALANLAR.Where(m => m.ALANID == guncellenen.TBL_ALANLAR.ALANID).FirstOrDefault();
                ur.URUNALAN = aln.ALANID;
                ur.STOK = guncellenen.STOK;
                ur.URUNFIYAT = guncellenen.URUNFIYAT;
                db.SaveChanges();
                return RedirectToAction("Index");
            }
            else
            {
                ur.URUNAD = guncellenen.URUNAD;
                ur.MARKA = guncellenen.MARKA;
                var ktgr = db.TBL_KATEGORILER.Where(m => m.KATEGORIID == guncellenen.TBL_KATEGORILER.KATEGORIID).FirstOrDefault();
                ur.URUNKATEGORI = ktgr.KATEGORIID;
                var aln = db.TBL_ALANLAR.Where(m => m.ALANID == guncellenen.TBL_ALANLAR.ALANID).FirstOrDefault();
                ur.URUNALAN = aln.ALANID;
                ur.STOK = guncellenen.STOK;
                ur.URUNFIYAT = guncellenen.URUNFIYAT;
                ur.RESIMM = File.FileName.ToString();
                db.SaveChanges();
                return RedirectToAction("Index");
            }      
        }

        public ActionResult KritikStok()
        {
            var kritik = db.TBL_URUNLER.Where(m => m.STOK <= 20).ToList();
            return View(kritik);
        }
    }
}