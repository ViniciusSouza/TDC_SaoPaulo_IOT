using System;
using System.Collections.Generic;
using System.Linq;
using System.Web;
using System.Web.Mvc;

namespace iot4dx.Controllers
{
    public class HomeController : Controller
    {
        public ActionResult Index()
        {
           
            return View();
        }

        public ActionResult About()
        {
            ViewBag.Message = "MS @TDC 2014 - É uma ação desenvolvida pelos evangelistas da Microsoft especialmente para o The Developers Conference 2014. Nesta ação estamos trabalhando com diversas tecnologias Microsot Aplicativos, Azure, IoT";

            return View();
        }

        
    }
}