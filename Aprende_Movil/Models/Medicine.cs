using System;
using System.Collections.Generic;
using System.Linq;
using System.Web;

namespace Aprende_Movil.Models
{
	public class Medicine
	{
		public int medicineID { get; set; }
		public String name { get; set; }
		public int quantity { get; set; }
		public String measure { get; set; }
	}
}