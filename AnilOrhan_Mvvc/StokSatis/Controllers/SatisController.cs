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
    public class SatisController : Controller
    {
        MagazamEntities2 db = new MagazamEntities2();
        [Authorize]
        // GET: Satis
        public ActionResult Index(int sayfa = 1)
        {
            var degerler = db.TBL_SATISLAR.ToList().ToPagedList(sayfa, 10);
            return View(degerler);
        }

        public ActionResult Sil(int id)
        {
            var satis = db.TBL_SATISLAR.Find(id);
            db.TBL_SATISLAR.Remove(satis);
            db.SaveChanges();
            return RedirectToAction("Index");
        }
        [HttpGet] //sayfa yüklendiğinde aşağıdaki gözükecek
        [Authorize]
        public ActionResult YeniSatis() //view ekleme kısmı bu kısma yapılıyor. AŞAĞIDAKİNDE DEĞİL
        {

            List<SelectListItem> musteriler = (from i in db.TBL_MUSTERILER.ToList()
                                          select new SelectListItem
                                          {
                                              Text = i.MUSTERIAD + " " + i.MUSTERISOYAD,
                                              Value = i.MUSTERIID.ToString()
                                          }
                                          ).ToList();
            List<SelectListItem> urunler = (from i in db.TBL_URUNLER.ToList()
                                         select new SelectListItem
                                         {
                                             Text = i.URUNAD,
                                             Value = i.URUNID.ToString()
                                         }
                                          ).ToList();
            ViewBag.musterilerr = musteriler;
            ViewBag.urunlerr = urunler; 
            return View();
        }
        [HttpPost]
        public ActionResult YeniSatis(TBL_SATISLAR satisEkle)
        {
            if (satisEkle.ADET == null || satisEkle.FIYAT == null)
            {
                List<SelectListItem> musteriler = (from i in db.TBL_MUSTERILER.ToList()
                                              select new SelectListItem
                                              {
                                                  Text = i.MUSTERIAD + " " + i.MUSTERISOYAD,
                                                  Value = i.MUSTERIID.ToString()
                                              }
                                          ).ToList();
                List<SelectListItem> urunler = (from i in db.TBL_URUNLER.ToList()
                                             select new SelectListItem
                                             {
                                                 Text = i.URUNAD,
                                                 Value = i.URUNID.ToString()
                                             }
                                              ).ToList();
                ViewBag.musterilerr = musteriler;
                ViewBag.urunlerr = urunler;
                return View();
            }
            else
            {
                var must = db.TBL_MUSTERILER.Where(m => m.MUSTERIID == satisEkle.TBL_MUSTERILER.MUSTERIID).FirstOrDefault();
                satisEkle.TBL_MUSTERILER = must;

                var urun = db.TBL_URUNLER.Where(m => m.URUNID == satisEkle.TBL_URUNLER.URUNID).FirstOrDefault();
                satisEkle.TBL_URUNLER = urun;

                ViewBag.mesaj = "SATILAN ÜRÜN ADETİ, ÜRÜN STOKTAN FAZLA OLAMAZ ! ";
                if (urun.STOK >= satisEkle.ADET)
                {
                    db.TBL_SATISLAR.Add(satisEkle);
                    db.SaveChanges();
                    return RedirectToAction("Index");
                }
                else
                {
                    ViewBag.mesaj = "Satılan ürün adeti, stoktan fazla olamaz !";
                    return View("Index2");
                }
          
            }

        }


        [Authorize]
        public ActionResult SatisGetir(int id) //View ekleniyor..
        {
            var sts = db.TBL_SATISLAR.Find(id);

            List<SelectListItem> musteriler3 = (from i in db.TBL_MUSTERILER.ToList()
                                          select new SelectListItem
                                          {
                                              Text = i.MUSTERIAD + " " + i.MUSTERISOYAD,
                                              Value = i.MUSTERIID.ToString()
                                          }
                                      ).ToList();
            List<SelectListItem> urunler3 = (from i in db.TBL_URUNLER.ToList()
                                         select new SelectListItem
                                         {
                                             Text = i.URUNAD,
                                             Value = i.URUNID.ToString()
                                         }
                                          ).ToList();
            ViewBag.musterilerr3 = musteriler3;
            ViewBag.urunlerr3 = urunler3;
            return View("SatisGetir", sts);

        }

        public ActionResult Guncelle(TBL_SATISLAR guncellenen)
        {
            var ur = db.TBL_SATISLAR.Find(guncellenen.SATISID);

            var urunler = db.TBL_URUNLER.Where(m => m.URUNID == guncellenen.TBL_URUNLER.URUNID).FirstOrDefault();
            ur.URUN = urunler.URUNID;

            var musteriler = db.TBL_MUSTERILER.Where(m => m.MUSTERIID == guncellenen.TBL_MUSTERILER.MUSTERIID).FirstOrDefault();
            ur.MUSTERI = musteriler.MUSTERIID;

            ur.FIYAT = guncellenen.FIYAT;
            ur.ADET = guncellenen.ADET;
            ur.TARIH = guncellenen.TARIH;
            db.SaveChanges();
            return RedirectToAction("Index");
        }
    }
}