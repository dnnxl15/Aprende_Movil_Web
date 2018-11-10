using Aprende_Movil.Library;
using System;
using System.Collections.Generic;
using System.Linq;
using System.Web;

namespace Aprende_Movil.Models
{
	public class PersonFactory
	{
		private static PersonFactory instance;

		private PersonFactory()
		{

		}

		public static PersonFactory getInstance()
		{
			if(instance == null)
			{
				instance = new PersonFactory();
			}
			return instance;
		}

		public User getPerson(UserType pUser, String pName, String pLastname, String pPassword, String pEmail)
		{
			User user = null;
			if (pUser == UserType.Administrator)
			{
				user = new Administrator(pName, pLastname, pPassword, pEmail);
			}
			else if(pUser == UserType.RegularUser)
			{
				user = new RegularUser(pName, pLastname, pPassword, pEmail);
			}
			return user;
		}
	}
}