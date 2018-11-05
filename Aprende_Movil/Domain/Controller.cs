using Aprende_Movil.Models;
using System;
using System.Collections.Generic;
using System.Linq;
using System.Web;

namespace Aprende_Movil.Domain
{
	public class Controller
	{
		public User user { get; set; }
		
		public List<Object> loadData()
		{
			return null;
		}

		public Boolean add(Object pObject)
		{
			return true;
		}

		public Boolean update(int pId, Object pObject)
		{
			return true;
		}

		public Boolean delete(int pId)
		{
			return true;
		}
	}
}