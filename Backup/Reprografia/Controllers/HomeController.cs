using System;
using System.Collections.Generic;
using System.Linq;
using System.Web;
using System.Web.Mvc;

namespace Reprografia.Controllers
{
    public class HomeController : Controller
    {
        public ActionResult Index()
        {
            if (User.Identity.IsAuthenticated)
            {
                ViewBag.Message = "Login realizado com sucesso";
                return RedirectToAction("Index", "Solicitacao");
            }
            ViewBag.Message = "Início";

            return View();
        }

        public ActionResult About()
        {
            return View();
        }
    }
}
