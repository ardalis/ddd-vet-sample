using System;
using System.Linq;
using System.Web.Mvc;

namespace FrontDesk.Web.Controllers
{
    public class HomeController : Controller
    {
        public ActionResult Index()
        {
            return View();
        }

        public ActionResult Doctors()
        {
            return View();
        }

        public ActionResult Rooms()
        {
            return View();
        }

        public ActionResult Clients()
        {
            return View();
        }
    }
}
