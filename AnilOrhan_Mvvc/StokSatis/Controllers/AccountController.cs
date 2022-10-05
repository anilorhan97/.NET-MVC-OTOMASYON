using System;
using System.Collections.Generic;
using System.Linq;
using System.Web;
using System.Web.Mvc;
using System.Web.Security;
using StokSatis.Models.Entity;

namespace StokSatis.Controllers
{
    public class AccountController : Controller
    {
        // GET: Account
        MagazamEntities2 db = new MagazamEntities2();
        public ActionResult Index()
        {
            return View();
        }
        [HttpGet]
        public ActionResult Login()
        {
            return View();
        }
        
        public ActionResult Logout()
        {
            FormsAuthentication.SignOut();
            return RedirectToAction("Login", "Account");
        }
        [HttpPost]
        [AllowAnonymous]
        public ActionResult Login(TBL_GIRIS girisParametreleri)
        {
            var bilgiler = db.TBL_GIRIS.FirstOrDefault(x => x.KULLANICIAD == girisParametreleri.KULLANICIAD && x.KULLANICISIFRE == girisParametreleri.KULLANICISIFRE); //kullanıcı tablomdan kayıtları ara
            if(bilgiler!=null)
            {
                FormsAuthentication.SetAuthCookie(girisParametreleri.KULLANICIAD, false);
                return RedirectToAction("Index", "Home");
            }
        
                ViewBag.mesaj = "Kullanıcı adı veya şifre hatalı !";
                return View();
            
        }

    }
}