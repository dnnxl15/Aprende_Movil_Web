﻿using Aprende_Movil.Library;
using System;
using System.Collections.Generic;
using System.Linq;
using System.Web;

namespace Aprende_Movil.Models
{
	public class RegularUser:User
	{
		public RegularUser(String pName, String pLastname, String pPassword, String pEmail)
		{
			this.name = pName;
			this.lastname = pLastname;
			this.password = pPassword;
			this.email = pEmail;
		}

		override
		public UserType getType()
		{
			return UserType.RegularUser;
		}
	}
}