using Aprende_Movil.Domain;
using Aprende_Movil.Models;
using System;
using System.Collections.Generic;
using System.Linq;
using System.Web;
using System.Web.Mvc;

namespace Aprende_Movil.Controllers
{
    public class MedicinesController : System.Web.Mvc.Controller {
        // GET: Medicines
        public ActionResult Index(string name)
        {
            ControllerMedicine instance = ControllerMedicine.getInstance();
            instance.loadData();
            //ViewBag.MedicineList = instance.listMedicine;
            return View(instance.listMedicine);
        }


    }
}