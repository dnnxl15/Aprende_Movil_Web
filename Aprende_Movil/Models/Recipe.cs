using System;
using System.Collections.Generic;
using System.Linq;
using System.Web;

namespace Aprende_Movil.Models
{
	public class Recipe
	{
		public float quantity { get; set; }
		public int frequency { get; set; }
		public DateTime startDate { get; set; }
		public DateTime endDate { get; set; }
		public Medicine medicine { get; set; }
	}
}