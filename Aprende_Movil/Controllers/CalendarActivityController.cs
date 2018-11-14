using Aprende_Movil.Domain;
using Aprende_Movil.Models;
using System;
using System.Collections.Generic;
using System.Linq;
using System.Web;
using System.Web.Mvc;

namespace Aprende_Movil.Controllers
{
    public class CalendarActivityController : System.Web.Mvc.Controller {
        // GET: CalendarActivity
        public ActionResult Index()
        {
            if(Request.Form["reminder"] != null) {
                ControllerCalendar instance = ControllerCalendar.getInstance();
                Activity activity = new Activity();

                activity.reminder = Request.Form["reminder"];
                activity.startTime = DateTime.ParseExact(Request.Form["startTime"], "yyyy-mm-dd",
                                       System.Globalization.CultureInfo.InvariantCulture);
                activity.endTime = DateTime.ParseExact(Request.Form["endTime"], "yyyy-mm-dd",
                                       System.Globalization.CultureInfo.InvariantCulture);
                activity.notice = int.Parse(Request.Form["noticeTime"]);

                instance.addActivity(activity);
                return RedirectToAction("Index", "Calendar");
            }
            else {
                return View();
            }
            
        }
    }
}