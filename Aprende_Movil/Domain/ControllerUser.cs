using Aprende_Movil.Models;
using MySql.Data.MySqlClient;
using System;
using System.Collections.Generic;
using System.Linq;
using System.Web;

namespace Aprende_Movil.Domain
{
	public class ControllerUser: Controller
	{
		public User user { get; set; }

		private static ControllerUser instance;

		private ControllerUser()
		{

		}

		public static ControllerUser getInstance()
		{
			if (instance == null)
			{
				instance = new ControllerUser();
			}
			return instance;
		}

		private void loadData(User pUser)
		{
			user = pUser;
			ControllerPayment.getInstance().user = this.user;
			ControllerCalendar.getInstance().user = this.user;
			ControllerMedicine.getInstance().user = this.user;
		}

		public override Boolean checkUser(User pUser)
		{
			Connection connect = Connection.getInstance();
			List<Parameter> listParameter = new List<Parameter>();

			Parameter parameter1 = new Parameter();
			parameter1.field = "@pEmail";
			parameter1.valueObject = pUser.email;

			Parameter parameter2 = new Parameter();
			parameter2.field = "@pPassword";
			parameter2.valueObject = pUser.password;

			listParameter.Add(parameter1);
			listParameter.Add(parameter2);

			MySqlParameter myRetParam = connect.requestFunction("validateUser", listParameter);
			int value = Int32.Parse(myRetParam.Value.ToString());
			if (value == 1)
			{
				loadData(pUser);
				return true;
			}
			else
			{
				return false;
			}
		}

		
		public override Boolean addUser(User pUser)
		{
			Connection connect = Connection.getInstance();
			List<Parameter> listParameter = new List<Parameter>();

			Parameter parameter1 = new Parameter();
			parameter1.field = "@pName";
			parameter1.valueObject = pUser.name;

			Parameter parameter2 = new Parameter();
			parameter2.field = "@pLastname";
			parameter2.valueObject = pUser.lastname;

			Parameter parameter3 = new Parameter();
			parameter3.field = "@pEmail";
			parameter3.valueObject = pUser.email;

			Parameter parameter4 = new Parameter();
			parameter4.field = "@pPassword";
			parameter4.valueObject = pUser.password;

			Parameter parameter5 = new Parameter();
			parameter5.field = "@pAddress";
			parameter5.valueObject = pUser.address;

			Parameter parameter6 = new Parameter();
			parameter6.field = "@pIdentification";
			parameter6.valueObject = pUser.identification;

			Parameter parameter7 = new Parameter();
			parameter7.field = "@pDateOfBirth";
			parameter7.valueObject = pUser.dateOfBirth;


			listParameter.Add(parameter1);
			listParameter.Add(parameter2);
			listParameter.Add(parameter3);
			listParameter.Add(parameter4);
			listParameter.Add(parameter5);
			listParameter.Add(parameter6);
			listParameter.Add(parameter7);

			connect.request("insertUser", listParameter);
			return true;
		}

		public override void updateUser(User pUser)
		{
			Connection connect = Connection.getInstance();
			List<Parameter> listParameter = new List<Parameter>();

			Parameter parameter1 = new Parameter();
			parameter1.field = "@pNewName";
			parameter1.valueObject = pUser.name;

			Parameter parameter2 = new Parameter();
			parameter2.field = "@pNewLastname";
			parameter2.valueObject = pUser.lastname;

			Parameter parameter3 = new Parameter();
			parameter3.field = "@pEmail";
			parameter3.valueObject = user.email;

			Parameter parameter4 = new Parameter();
			parameter4.field = "@pNewPassword";
			parameter4.valueObject = pUser.password;

			Parameter parameter5 = new Parameter();
			parameter5.field = "@pNewAddress";
			parameter5.valueObject = pUser.address;

			Parameter parameter6 = new Parameter();
			parameter6.field = "@pNewIdentification";
			parameter6.valueObject = pUser.identification;

			Parameter parameter7 = new Parameter();
			parameter7.field = "@pNewDateOfBirth";
			parameter7.valueObject = pUser.dateOfBirth;


			listParameter.Add(parameter1);
			listParameter.Add(parameter2);
			listParameter.Add(parameter3);
			listParameter.Add(parameter4);
			listParameter.Add(parameter5);
			listParameter.Add(parameter6);
			listParameter.Add(parameter7);
			user = pUser;
			connect.request("updateUser", listParameter);
		}
	}
}