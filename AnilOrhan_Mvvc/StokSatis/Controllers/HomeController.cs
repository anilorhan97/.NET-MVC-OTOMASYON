using System;
using System.Collections.Generic;
using System.Linq;
using System.Web;
using System.Web.Mvc;

namespace StokSatis.Controllers
{
    public class HomeController : Controller
    {
        [Authorize] //Burayı authorize et yani Loginurl burada çalıştır demek..
        public ActionResult Index()
        {
            return View();
        }

    }
}