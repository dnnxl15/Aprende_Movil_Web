using System;
using System.Collections.Generic;
using System.Linq;
using System.Web;

namespace Aprende_Movil.Models
{
	public class Activity
	{
		public int activityID { get; set; }
		public String reminder { get; set; }
		public DateTime startTime { get; set; }
		public DateTime endTime { get; set; }
		public int notice { get; set; }
		public DateTime noticeTime { get; set; }

	}
}