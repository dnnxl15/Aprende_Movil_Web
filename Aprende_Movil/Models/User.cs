﻿using System;
using System.Collections.Generic;
using System.Linq;
using System.Web;

namespace Aprende_Movil.Models
{
	public class User:Person
	{
		private String email { get; set; }
		private String password { get; set; }
	}
}