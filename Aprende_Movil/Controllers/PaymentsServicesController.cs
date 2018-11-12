using System;
using System.Collections.Generic;
using System.Linq;
using System.Web;
using System.Web.Mvc;
using Aprende_Movil.Models;

namespace Aprende_Movil.Controllers
{
    public class PaymentsServicesController : Controller
    {
        // GET: PaymentsServices
        public ActionResult Index()
        {
            ViewBag.UserName = LoginSingleton.getUserName();
            return View();
        }
    }
}