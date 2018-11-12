using Aprende_Movil.Library;
using Aprende_Movil.Models;
using System;
using System.Collections.Generic;
using System.Linq;
using System.Web;

namespace Aprende_Movil.Domain
{
	public class ControllerCalendar:Controller
	{
		private static ControllerCalendar instance;
		public List<Activity> listActivity { get; set; }

		private ControllerCalendar()
		{
		}

		public static ControllerCalendar getInstance()
		{
			if (instance == null)
			{
				instance = new ControllerCalendar();
			}
			return instance;
		}

		public override Boolean addActivity(Activity pActivity)
		{
			Connection connect = Connection.getInstance();
			List<Parameter> listParameter = new List<Parameter>();

			Parameter parameter1 = new Parameter();
			parameter1.field = "@pReminder";
			parameter1.valueObject = pActivity.reminder;

			Parameter parameter2 = new Parameter();
			parameter2.field = "@pStartTime";
			parameter2.valueObject = pActivity.startTime;

			Parameter parameter3 = new Parameter();
			parameter3.field = "@pEmail";
			parameter3.valueObject = user.email;

			Parameter parameter4 = new Parameter();
			parameter4.field = "@pNotice";
			parameter4.valueObject = pActivity.notice;

			Parameter parameter5 = new Parameter();
			parameter5.field = "@pEndTime";
			parameter5.valueObject = pActivity.endTime;


			listParameter.Add(parameter1);
			listParameter.Add(parameter2);
			listParameter.Add(parameter3);
			listParameter.Add(parameter4);
			listParameter.Add(parameter5);

			listActivity.Add(pActivity);
			connect.request("insertCalendar", listParameter);
			return true;
		}

		public override void updateActivity(Activity pActivity, String pReminder)
		{
			foreach (Activity activity in listActivity)
			{
				if (pActivity.reminder == pReminder)
				{
					listActivity.Remove(activity);
					listActivity.Add(pActivity);

					Connection connect = Connection.getInstance();
					List<Parameter> listParameter = new List<Parameter>();

					Parameter parameter1 = new Parameter();
					parameter1.field = "@pReminder";
					parameter1.valueObject = pReminder;

					Parameter parameter2 = new Parameter();
					parameter2.field = "@pNewStartTime";
					parameter2.valueObject = pActivity.startTime;

					Parameter parameter3 = new Parameter();
					parameter3.field = "@pEmail";
					parameter3.valueObject = user.email;

					Parameter parameter4 = new Parameter();
					parameter4.field = "@pNewNotice";
					parameter4.valueObject = pActivity.notice;

					Parameter parameter5 = new Parameter();
					parameter5.field = "@pNewEndTime";
					parameter5.valueObject = pActivity.endTime;

					Parameter parameter6 = new Parameter();
					parameter6.field = "@pNewReminder";
					parameter6.valueObject = pActivity.reminder;

					listParameter.Add(parameter1);
					listParameter.Add(parameter2);
					listParameter.Add(parameter3);
					listParameter.Add(parameter4);
					listParameter.Add(parameter5);
					listParameter.Add(parameter6);
					connect.request("updateCalendar", listParameter);
				}
			}
		}

		public override Boolean deleteActivity(Activity pActivity)
		{
			Connection connect = Connection.getInstance();
			List<Parameter> listParameter = new List<Parameter>();

			Parameter parameter1 = new Parameter();
			parameter1.field = "@pEmail";
			parameter1.valueObject = user.name;

			Parameter parameter2 = new Parameter();
			parameter2.field = "@pReminder";
			parameter2.valueObject = pActivity.reminder;

			listParameter.Add(parameter1);
			listParameter.Add(parameter2);
			foreach (Activity activity in listActivity)
			{
				if (pActivity.reminder == activity.reminder)
				{
					listActivity.Remove(activity);
					listActivity.Add(pActivity);
				}
			}

					connect.request("deleteReminder", listParameter);
			return true;
		}

		public override Boolean loadData()
		{
			Connection connection = Connection.getInstance();
			Boolean open = connection.OpenConnection();
			List<Parameter> listParamenter = new List<Parameter>();
			Parameter parameter = new Parameter();
			parameter.field = IConstant.PARAMETER_EMAIL;
			parameter.valueObject = user.email;
			listParamenter.Add(parameter);

			MySql.Data.MySqlClient.MySqlDataReader reader = connection.request(IConstant.PROCEDURE_GET_MEDICINE, listParamenter);
			List<Activity> listActivity = new List<Activity>();
			while (reader.Read())
			{
				Activity activity = new Activity();

				activity.reminder = reader.GetString("reminder");
				activity.notice = reader.GetInt16("notice");
				activity.startTime = reader.GetDateTime("startTime");
				activity.endTime = reader.GetDateTime("endTime");
				listActivity.Add(activity);
			}
			this.listActivity = listActivity;
			return true;
		}
	}




}