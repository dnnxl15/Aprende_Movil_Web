using Aprende_Movil.Domain;
using Aprende_Movil.Models;
using System;
using System.Collections.Generic;
using System.Linq;
using System.Web;
using System.Web.Mvc;

namespace Aprende_Movil.Controllers
{
    public class MedicineController : System.Web.Mvc.Controller {
        // GET: Medicine
        public ActionResult Index(string name, string id, string freq, string qty, string msr, string srtD, string endD)
        {
            if (Request.Form["option"] != null) {


                if (Request.Form["option"].Equals("edit")) {
                    ControllerMedicine instance = ControllerMedicine.getInstance();

                    Recipe recipe = new Recipe();
                    Medicine medicine = new Medicine();
                    medicine.name = Request.Form["medName"];
                    medicine.measure = Request.Form["medMsr"];
                    recipe.medicine = medicine;

                    recipe.quantity = int.Parse(Request.Form["medQty"]);
                    recipe.frequency = int.Parse(Request.Form["medFreq"]);
                    recipe.startDate = DateTime.ParseExact(Request.Form["medStrD"], "dd-mm-yyyy",
                                       System.Globalization.CultureInfo.InvariantCulture);
                    recipe.endDate = DateTime.ParseExact(Request.Form["medEndD"], "dd-mm-yyyy",
                                       System.Globalization.CultureInfo.InvariantCulture);

                    instance.updateMedicine(recipe);
                    return RedirectToAction("Index", "Medicines");
                }

                if (Request.Form["option"].Equals("delete")) {
                    return View();
                }

            }

            ViewBag.name = name;
            ViewBag.id = id;
            ViewBag.freq = freq;
            ViewBag.qty = qty;
            ViewBag.msr = msr;
            ViewBag.srtD = srtD;
            ViewBag.endD = endD;

            return View();

        }
    }
}