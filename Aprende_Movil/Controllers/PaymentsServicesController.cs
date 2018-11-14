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
        public ActionResult Index(string name, string payday, string amount, string lxz)
        {
            ControllerPayment instance = ControllerPayment.getInstance();
            if (name != null) {
                Payment payment = new Payment();
                payment.name = name;
                payment.payDate = DateTime.Parse(payday);
                payment.amount = int.Parse(amount);
                instance.addPayment(payment);
                instance.loadData();
                string myLXZ;
                if (name.Equals("Luz"))
                    myLXZ = "1";
                else
                    myLXZ = "2";
                return RedirectToAction("Index", "PaymentsServices", new {lxz= myLXZ });
            }
            else {
                instance.loadData();
                if(lxz == null) {
                    Payment paymentLuz = instance.getLuz();
                    ViewBag.nameLuz = paymentLuz.name;
                    ViewBag.amountLuz = paymentLuz.amount;
                    ViewBag.payDayLuz = paymentLuz.payDate;

                    Payment paymentAgua = instance.getAgua();
                    ViewBag.nameAgua = paymentAgua.name;
                    ViewBag.amountAgua = paymentAgua.amount;
                    ViewBag.payDayAgua = paymentAgua.payDate;
                }
                else {
                    if (lxz.Equals("1")) {
                        Payment paymentLuz = instance.getLuz();
                        ViewBag.nameLuz = paymentLuz.name;
                        ViewBag.amountLuz = paymentLuz.amount;
                        ViewBag.payDayLuz = paymentLuz.payDate;

                        Payment paymentAgua = instance.ultimoAgua;
                        ViewBag.nameAgua = paymentAgua.name;
                        ViewBag.amountAgua = paymentAgua.amount;
                        ViewBag.payDayAgua = paymentAgua.payDate;
                    }
                    if (lxz.Equals("2")) {
                        Payment paymentLuz = instance.ultimoLuz;
                        ViewBag.nameLuz = paymentLuz.name;
                        ViewBag.amountLuz = paymentLuz.amount;
                        ViewBag.payDayLuz = paymentLuz.payDate;

                        Payment paymentAgua = instance.getAgua();
                        ViewBag.nameAgua = paymentAgua.name;
                        ViewBag.amountAgua = paymentAgua.amount;
                        ViewBag.payDayAgua = paymentAgua.payDate;
                    }
                }
                
                

                return View(instance.listPayment);
            }
            
            
        }

    }
}