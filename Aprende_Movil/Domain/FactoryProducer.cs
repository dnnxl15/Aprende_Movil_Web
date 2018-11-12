using Aprende_Movil.Library;
using Aprende_Movil.Models;
using System;
using System.Collections.Generic;
using System.Linq;
using System.Web;

namespace Aprende_Movil.Domain
{
	public class FactoryProducer
	{
		private static FactoryProducer instance;

		private FactoryProducer()
		{

		}

		public static FactoryProducer getInstance()
		{
			if (instance == null)
			{
				instance = new FactoryProducer();
			}
			return instance;
		}

		public AbstractFactory getFactory (FactoryType pType)
		{
			AbstractFactory factory = null;
			if (pType == FactoryType.ControlFactoryType)
			{
				factory = ControlFactory.getInstance();
			}
			else if (pType == FactoryType.PersonFactoryType)
			{
				factory = PersonFactory.getInstance();
			}
			else
			{
				factory = null;
			}
			return factory;
		}
	}
}