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

		public virtual Boolean loadData()
		{
			return true;
		}

		public virtual Boolean addUser(User pUser)
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

		public virtual Boolean checkUser(User user)
		{
			throw new NotImplementedException();
		}

		public virtual void updateUser(User pUser)
		{
			throw new NotImplementedException();
		}

		public virtual Boolean addActivity(Activity pActivity)
		{
			throw new NotImplementedException();
		}

		public virtual void updateActivity(Activity pActivity, String pReminder)
		{

		}

		public virtual Boolean deleteActivity(Activity pActivity)
		{
			throw new NotImplementedException();
		}

		public virtual Boolean updateMedicine(Recipe pRecipe)
		{
			throw new NotImplementedException();
		}

		public virtual Boolean addMedicine(Recipe pRecipe)
		{
			throw new NotImplementedException();
		}

		public virtual Boolean addPayment(Payment pPayment)
		{
			throw new NotImplementedException();
		}
	}
}