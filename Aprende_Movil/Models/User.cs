using Aprende_Movil.Library;
using System;
using System.Collections.Generic;
using System.Linq;
using System.Web;

namespace Aprende_Movil.Models
{
	public class User:Person
	{
		public String email { get; set; }
		public String password { get; set; }

		public abstract UserType getType()
		{
			return UserType.Administrator;
		}
	}
}