using Aprende_Movil.Library;
using System;
using System.Collections.Generic;
using System.Linq;
using System.Web;

namespace Aprende_Movil.Domain
{
	public class ControlFactory:AbstractFactory
	{
		private static ControlFactory instance;

		private ControlFactory()
		{
		}

		public static ControlFactory getInstance()
		{
			if (instance == null)
			{
				instance = new ControlFactory();
			}
			return instance;
		}

		
		public Controller getControl(ControlType pType)
		{
			if(pType == ControlType.ControlCalendar)
			{
				Controller controlCalendar = ControllerCalendar.getInstance();
				return controlCalendar;
			}
			else if (pType == ControlType.ControlMedicine)
			{
				Controller controlMedicine = ControllerMedicine.getInstance();
				return controlMedicine;
			}
			else if (pType == ControlType.ControlPayment)
			{
				Controller controlPayment = ControllerPayment.getInstance();
				return controlPayment;
			}
			else if (pType == ControlType.ControlUser)
			{
				Controller controlUser = ControllerUser.getInstance();
				return controlUser;
			}
			else
			{
				return null;
			}

		}
	}
}