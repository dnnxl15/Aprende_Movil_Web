using Aprende_Movil.Library;
using Aprende_Movil.Models;
using MySql.Data.MySqlClient;
using System;
using System.Collections.Generic;
using System.Linq;
using System.Web;

namespace Aprende_Movil.Domain
{
	public class ControllerPayment: Controller
	{
		private static ControllerPayment instance;
		public List<Payment> listPayment { get; set; }

		private ControllerPayment()
		{
		}

		public static ControllerPayment getInstance()
		{
			if(instance == null)
			{
				instance = new ControllerPayment();
			}
			return instance;
		}

		public Boolean addPayment(Payment pPayment)
		{
			Connection connect = Connection.getInstance();
			List<Parameter> listParameter = new List<Parameter>();
			Parameter parameter1 = new Parameter();
			parameter1.field = IConstant.PARAMETER_NAME;
			parameter1.valueObject = pPayment.name;
			Parameter parameter2 = new Parameter();
			parameter2.field = IConstant.PARAMETER_FREQUENCY;
			parameter2.valueObject = pPayment.frequency;
			listParameter.Add(parameter2);
			listParameter.Add(parameter1);
			connect.request(IConstant.PROCEDURE_INSERT_PAYMENT, listParameter);
			listPayment.Add(pPayment);
			return true;
		}

		public Boolean loadData()
		{
			Connection connection = Connection.getInstance();
			Boolean open = connection.OpenConnection();
			MySqlDataReader reader = connection.request("getPayment", new List<Parameter>());
			List<Payment> listPayment = new List<Payment>();
			while (reader.Read())
			{
				Payment payment = new Payment();
				payment.name = reader.GetString("name");
				listPayment.Add(payment);
			}
			this.listPayment = listPayment;
			return true;
		}
	}
}