using Aprende_Movil.Library;
using Aprende_Movil.Models;
using MySql.Data.MySqlClient;
using System;
using System.Collections.Generic;
using System.Linq;
using System.Web;

namespace Aprende_Movil.Domain
{
    public class ControllerPayment : Controller {
        private static ControllerPayment instance;
        public List<Payment> listPayment { get; set; }
        public List<Payment> listFuturePaymentsLuz { get; set; }
        public List<Payment> listFuturePaymentsAgua { get; set; }
        public Payment ultimoLuz;
        public Payment ultimoAgua;


        private ControllerPayment()
		{
            Random random = new Random();
            listFuturePaymentsLuz = new List<Payment>();
            
            listFuturePaymentsAgua = new List<Payment>();

            for (int i = 1; i < 10; i++) {
                Payment payment = new Payment();
                payment.amount = 10000 + 1237 * random.Next(0, 15);
                payment.name = "Luz";
                payment.payDate = new DateTime(2018, i, 13);
                listFuturePaymentsLuz.Add(payment);

                payment = new Payment();
                payment.amount = 7600 + 1712 * random.Next(0,15);
                payment.name = "Agua";
                payment.payDate = new DateTime(2018, i, 13);
                listFuturePaymentsAgua.Add(payment);
            }
		}

        public Payment getLuz() {
            Payment payment = listFuturePaymentsLuz[0];
            listFuturePaymentsLuz.Remove(payment);
            ultimoLuz = payment;
            return payment;
        }

        public Payment getAgua() {
            Payment payment = listFuturePaymentsAgua[0];
            listFuturePaymentsAgua.Remove(payment);
            ultimoAgua = payment;
            return payment;
        }

        public static ControllerPayment getInstance()
		{
			if(instance == null)
			{
				instance = new ControllerPayment();
			}
			return instance;
		}

		public override Boolean addPayment(Payment pPayment)
		{
			Connection connect = Connection.getInstance();
			List<Parameter> listParameter = new List<Parameter>();
			Parameter parameter1 = new Parameter();
			parameter1.field = IConstant.PARAMETER_NAME;
			parameter1.valueObject = pPayment.name;
			Parameter parameter2 = new Parameter();
			parameter2.field = IConstant.PARAMETER_AMOUNT;
			parameter2.valueObject = pPayment.amount;
			Parameter parameter3 = new Parameter();
			parameter3.field = IConstant.PARAMETER_EMAIL;
			parameter3.valueObject = user.email;
			listParameter.Add(parameter2);
			listParameter.Add(parameter1);
			listParameter.Add(parameter3);

			connect.request(IConstant.PROCEDURE_INSERT_PAYMENT, listParameter);
			listPayment.Add(pPayment);
			connect.CloseConnection();
			loadData();
			return true;
		}

		public override Boolean loadData()
		{
			Connection connection = Connection.getInstance();
			Boolean open = connection.OpenConnection();
			List<Parameter> listParameter = new List<Parameter>();
			Parameter parameter1 = new Parameter();
			parameter1.field = IConstant.PARAMETER_EMAIL;
			parameter1.valueObject = user.email;
            listParameter.Add(parameter1);
            MySqlDataReader reader = connection.request(IConstant.PROCEDURE_GET_PAYMENT, listParameter);
			List<Payment> listPayment = new List<Payment>();
			while (reader.Read())
			{
				Payment payment = new Payment();
				payment.name = reader.GetString("payment");
                payment.payDate = reader.GetDateTime("payday");
                payment.amount = reader.GetInt32("amount");
                listPayment.Add(payment);
			}
			this.listPayment = listPayment;
			reader.Close();

			return true;
		}
	}
}