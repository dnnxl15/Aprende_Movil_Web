using Aprende_Movil.Library;
using Aprende_Movil.Models;
using System;
using System.Collections.Generic;
using System.Linq;
using System.Web;

namespace Aprende_Movil.Domain
{
	public class ControllerSystem
	{
		private static ControllerSystem instance;

		private ControllerSystem()
		{

		}

		public static ControllerSystem getInstance()
		{
			if (instance == null)
			{
				instance = new ControllerSystem();
			}
			return instance;
		}

		public Boolean checkUser(String pEmail, String pPassword)
		{
			FactoryProducer factory = FactoryProducer.getInstance();
			AbstractFactory controlFactory = factory.getFactory(Library.FactoryType.ControlFactoryType);
			Controller control = controlFactory.getControl(Library.ControlType.ControlUser);
            RegularUser user = new RegularUser();
			user.email = pEmail;
			user.password = pPassword;
			Boolean value = control.checkUser(user);
			return value;
		}

		public void loadData()
		{
			FactoryProducer factory = FactoryProducer.getInstance();
			AbstractFactory controlFactory = factory.getFactory(Library.FactoryType.ControlFactoryType);
			Controller calendar = controlFactory.getControl(Library.ControlType.ControlCalendar);
			Controller medicine = controlFactory.getControl(Library.ControlType.ControlMedicine);
			Controller payment = controlFactory.getControl(Library.ControlType.ControlPayment);
			calendar.loadData();
			medicine.loadData();
			payment.loadData();
		}

		public Controller getController(ControlType pControlType)
		{
			FactoryProducer factory = FactoryProducer.getInstance();
			AbstractFactory controlFactory = factory.getFactory(Library.FactoryType.ControlFactoryType);
			Controller control = controlFactory.getControl(pControlType);
			return control;
		}

		public List<Activity> loadDataByMonth(int pMonth)
		{
			FactoryProducer factory = FactoryProducer.getInstance();
			AbstractFactory controlFactory = factory.getFactory(Library.FactoryType.ControlFactoryType);
			Controller calendar = controlFactory.getControl(Library.ControlType.ControlCalendar);
			List<Activity>  listActivityByMonth = calendar.loadDataMonth(pMonth);
			return listActivityByMonth;
		}

		public Boolean addUser(User pUser)
		{
			FactoryProducer factory = FactoryProducer.getInstance();
			AbstractFactory controlFactory = factory.getFactory(Library.FactoryType.ControlFactoryType);
			Controller control = controlFactory.getControl(Library.ControlType.ControlUser);
			Boolean value = control.addUser(pUser);
			return value;
		}

		public void updateUser(User pUser)
		{
			FactoryProducer factory = FactoryProducer.getInstance();
			AbstractFactory controlFactory = factory.getFactory(Library.FactoryType.ControlFactoryType);
			Controller control = controlFactory.getControl(Library.ControlType.ControlUser);
			control.updateUser(pUser);
		}

		public Boolean addActivity(Activity pActivity)
		{
			FactoryProducer factory = FactoryProducer.getInstance();
			AbstractFactory controlFactory = factory.getFactory(Library.FactoryType.ControlFactoryType);
			Controller control = controlFactory.getControl(Library.ControlType.ControlCalendar);
			control.addActivity(pActivity);
			return true;
		}

		public virtual void updateActivity(Activity pActivity, String pReminder)
		{
			FactoryProducer factory = FactoryProducer.getInstance();
			AbstractFactory controlFactory = factory.getFactory(Library.FactoryType.ControlFactoryType);
			Controller control = controlFactory.getControl(Library.ControlType.ControlCalendar);
			control.updateActivity(pActivity, pReminder);
		}

		public virtual Boolean deleteActivity(Activity pActivity)
		{
			FactoryProducer factory = FactoryProducer.getInstance();
			AbstractFactory controlFactory = factory.getFactory(Library.FactoryType.ControlFactoryType);
			Controller control = controlFactory.getControl(Library.ControlType.ControlCalendar);
			control.deleteActivity(pActivity);
			return true;
		}

		public virtual Boolean updateMedicine(Recipe pRecipe)
		{
			FactoryProducer factory = FactoryProducer.getInstance();
			AbstractFactory controlFactory = factory.getFactory(Library.FactoryType.ControlFactoryType);
			Controller control = controlFactory.getControl(Library.ControlType.ControlMedicine);
			control.updateMedicine(pRecipe);
			return true;
		}

		public virtual Boolean addMedicine(Recipe pRecipe)
		{
			FactoryProducer factory = FactoryProducer.getInstance();
			AbstractFactory controlFactory = factory.getFactory(Library.FactoryType.ControlFactoryType);
			Controller control = controlFactory.getControl(Library.ControlType.ControlMedicine);
			control.addMedicine(pRecipe);
			return true;
		}

		public virtual Boolean addPayment(Payment pPayment)
		{
			FactoryProducer factory = FactoryProducer.getInstance();
			AbstractFactory controlFactory = factory.getFactory(Library.FactoryType.ControlFactoryType);
			Controller control = controlFactory.getControl(Library.ControlType.ControlPayment);
			control.addPayment(pPayment);
			return true;
		}


	}
}