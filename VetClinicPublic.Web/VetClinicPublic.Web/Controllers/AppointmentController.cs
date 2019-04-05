using System;
using System.Web.Mvc;
using VetClinicPublic.Web.AppStart;
using VetClinicPublic.Web.Models;

namespace VetClinicPublic.Web.Controllers
{
    public class AppointmentController : Controller
    {
        public ActionResult Confirm(Guid id)
        {
            var messagingConfig = new MessagingConfig();
            messagingConfig.SendConfirmationMessageToScheduler(new AppointmentConfirmedEvent(id));
            return View();
        }
    }
}