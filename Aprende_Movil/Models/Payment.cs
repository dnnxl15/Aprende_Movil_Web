using System;
using System.Collections.Generic;
using System.Linq;
using System.Web;

namespace Aprende_Movil.Models
{
	public class Payment
	{
		public int paymentID { get; set; }
		public String name { get; set; }
		public int frequency { get; set; }
		public float amount { get; set; }
		public DateTime payDate { get; set; }
	}
}