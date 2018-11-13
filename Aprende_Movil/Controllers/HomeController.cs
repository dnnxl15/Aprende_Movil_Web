using Aprende_Movil.Domain;
using Aprende_Movil.Models;
using System;
using System.Collections.Generic;
using System.Linq;
using System.Web;
using System.Web.Mvc;

namespace Aprende_Movil.Controllers
{
	public class HomeController : System.Web.Mvc.Controller {

        public ActionResult Index(string name) {
            User user = new User();
            user.email = Request.Form["email"];
            user.password = Request.Form["userPassword"];

            if (ControllerUser.getInstance().checkUser(user)) { //ControllerUser.getInstance().checkUser(user)) {
                //Login Successful
                LoginSingleton.setUser(Request.Form["email"]);
            }
            ViewBag.UserName = LoginSingleton.getUserName();
            return View();
        }

        public ActionResult About()
		{
			ViewBag.Message = "Your application description page.";

			return View();
		}

		public ActionResult Contact()
		{
			ViewBag.Message = "Your contact page.";

			return View();
		}
	}
}