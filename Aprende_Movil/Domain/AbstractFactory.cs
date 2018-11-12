using Aprende_Movil.Library;
using Aprende_Movil.Models;
using System;
using System.Collections.Generic;
using System.Linq;
using System.Web;

namespace Aprende_Movil.Domain
{
	public class AbstractFactory
	{
		public Controller getControl(ControlType pType)
		{
			return null;
		}

		public User getPerson(UserType pUser, String pName, String pLastname, String pPassword, String pEmail)
		{
			return null;
		}


	}
}