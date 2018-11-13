using Aprende_Movil.Domain;
using Aprende_Movil.Models;
using System;
using System.Collections.Generic;
using System.Diagnostics;
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
                ControllerMedicine instance = ControllerMedicine.getInstance();

                if (Request.Form["option"].Equals("edit")) {
                    

                    Recipe recipe = new Recipe();
                    Medicine medicine = new Medicine();
                    medicine.name = Request.Form["medName"];
                    medicine.measure = Request.Form["medMsr"];
                    recipe.medicine = medicine;

                    recipe.quantity = int.Parse(Request.Form["medQty"]);
                    recipe.frequency = int.Parse(Request.Form["medFreq"]);
                    recipe.startDate = DateTime.ParseExact(Request.Form["medStrD"], "yyyy-mm-dd",
                                       System.Globalization.CultureInfo.InvariantCulture);
                    recipe.endDate = DateTime.ParseExact(Request.Form["medEndD"], "yyyy-mm-dd",
                                       System.Globalization.CultureInfo.InvariantCulture);

                    instance.updateMedicine(recipe);
                    return RedirectToAction("Index", "Medicines");
                }
                if (Request.Form["option"].Equals("delete")) {
                    
                    return RedirectToAction("Index", "Medicines");
                }
                if (Request.Form["option"].Equals("add")) {
                    Recipe recipe = new Recipe();
                    Medicine medicine = new Medicine();
                    medicine.name = Request.Form["medName"];
                    medicine.measure = Request.Form["medMsr"];
                    recipe.medicine = medicine;

                    recipe.quantity = int.Parse(Request.Form["medQty"]);
                    recipe.frequency = int.Parse(Request.Form["medFreq"]);

                    Debug.Print(instance.user.email);

                    Debug.Print(Request.Form["medStrD"]);
                    recipe.startDate = DateTime.ParseExact(Request.Form["medStrD"], "yyyy-mm-dd",
                                       System.Globalization.CultureInfo.InvariantCulture);
                    recipe.endDate = DateTime.ParseExact(Request.Form["medEndD"], "yyyy-mm-dd",
                                       System.Globalization.CultureInfo.InvariantCulture);

                    instance.addMedicine(recipe);
                    return RedirectToAction("Index", "Medicines");
                }
            }

            if (id == "new") {
                ViewBag.addNew = true;
                ViewBag.id = null;
            }
            else {
                ViewBag.addNew = false;
                ViewBag.id = id;
            }

            ViewBag.name = name;
            ViewBag.freq = freq;
            ViewBag.qty = qty;
            ViewBag.msr = msr;
            ViewBag.srtD = srtD;
            ViewBag.endD = endD;

            return View();

        }
    }
}