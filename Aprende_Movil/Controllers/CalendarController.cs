﻿using System;
using System.Collections.Generic;
using System.Linq;
using System.Web;
using System.Web.Mvc;
using Aprende_Movil.Models;

namespace Aprende_Movil.Controllers
{
    public class CalendarController : Controller
    {
        // GET: Calendar
        public ActionResult Index()
        {
            ViewBag.UserName = LoginSingleton.getUserName();
            return View();
        }
    }
}