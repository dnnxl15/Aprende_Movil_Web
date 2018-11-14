using System;
using System.Collections.Generic;
using System.Linq;
using System.Web;
using System.Web.Mvc;
using Aprende_Movil.Domain;
using Aprende_Movil.Models;

namespace Aprende_Movil.Controllers
{
    public class CalendarController : System.Web.Mvc.Controller {
        // GET: Calendar
        public ActionResult Index(string name, string rmdD, string srtD, string endD)
        {

            ControllerCalendar instance = ControllerCalendar.getInstance();
            if (name != null) {
                Activity activity = new Activity();
                activity.reminder = name;
                activity.startTime = DateTime.ParseExact(srtD.Split('a')[0].Trim(), "d/M/yyyy hh:mm:ss",
                                       System.Globalization.CultureInfo.InvariantCulture);
                activity.endTime = DateTime.ParseExact(endD.Split('a')[0].Trim(), "d/M/yyyy hh:mm:ss",
                                       System.Globalization.CultureInfo.InvariantCulture);

                instance.deleteActivity(activity);
                return RedirectToAction("Index", "Calendar");
            }

            instance.loadData();
            return View(instance.listActivity);
        }
    }
}