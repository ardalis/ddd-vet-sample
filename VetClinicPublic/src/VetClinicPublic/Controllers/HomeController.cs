

using Microsoft.AspNetCore.Mvc;

namespace VetClinicPublic.Web.Controllers
{
    public class HomeController : Controller
    {
        public ActionResult Index()
        {
            return View();
        }

    }
}