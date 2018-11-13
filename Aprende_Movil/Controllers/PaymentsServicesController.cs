using System;
using System.Collections.Generic;
using System.Linq;
using System.Web;
using System.Web.Mvc;
using Aprende_Movil.Domain;
using Aprende_Movil.Models;

namespace Aprende_Movil.Controllers
{
    public class PaymentsServicesController : System.Web.Mvc.Controller {
        // GET: PaymentsServices
        public ActionResult Index(string name, string payday, string amount)
        {
            ControllerPayment instance = ControllerPayment.getInstance();
            if (name != null) {
                Payment payment = new Payment();
                payment.name = name;
                payment.payDate = DateTime.Parse(payday);
                payment.amount = int.Parse(amount);
                instance.addPayment(payment);
                instance.loadData();
                return View(instance.listPayment);
            }
            else {
                instance.loadData();
                return View(instance.listPayment);
            }
            
            
        }

    }
}