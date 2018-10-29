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

		public Person getPerson(UserType pUserType, String pName, 
			String pLastname, String pPassword,
			String pEmail, int pId)
		{
			User person = null;
			if(pUserType == UserType.Administrator)
			{
				person = new Administrator();
				person.name = pName;
				person.lastname = pLastname;
				person.email = pEmail;
				person.password = pPassword;
			}
			else
			{
				person = new RegularUser();
				person.name = pName;
				person.lastname = pLastname;
				person.email = pEmail;
				person.password = pPassword;
			}
			return person;
		}
	}
}