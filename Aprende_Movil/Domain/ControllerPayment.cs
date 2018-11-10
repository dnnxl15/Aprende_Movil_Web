using Aprende_Movil.Models;
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
			listPayment.Add(pPayment);
			return true;
		}

		public Boolean deletePayment(int pId)
		{
			foreach (var parameter in listPayment)
			{
				int id = parameter.paymentID;
				if(id == pId)
				{
					listPayment.Remove(parameter);
					return true;
				}
				
			}
			return false;
		}

		public Boolean updatePayment(int pId, Payment pPayment)
		{ 
			foreach (var parameter in listPayment)
			{
				int id = parameter.paymentID;
				if (id == pId)
				{
					listPayment.Remove(parameter);
					listPayment.Add(pPayment);
					return true;
				}

			}
			return false;
		}

		// FALTA 
		private Boolean loadData()
		{
			return true;
		}
	}
}