using Aprende_Movil.Models;
using System;
using System.Collections.Generic;
using System.Linq;
using System.Web;

namespace Aprende_Movil.Domain
{
	public class ControllerUser
	{
		public User user { get; set; }

		private void loadData(User pUser)
		{
			user = pUser;
		}

		public Boolean checkUser()
		{
			return true;
		}
	}
}