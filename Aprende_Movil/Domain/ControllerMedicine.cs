using Aprende_Movil.Library;
using Aprende_Movil.Models;
using System;
using System.Collections.Generic;
using System.Linq;
using System.Web;

namespace Aprende_Movil.Domain
{
	public class ControllerMedicine: Controller
	{
		private static ControllerMedicine instance;
		public List<Recipe> listMedicine { get; set; }

		private ControllerMedicine()
		{
		}

		public static ControllerMedicine getInstance()
		{
			if (instance == null)
			{
				instance = new ControllerMedicine();
			}
			return instance;
		}

		/** Method addMedicine
		 * 
		 * 
		 */
		public override Boolean addMedicine(Recipe pRecipe)
		{
			Connection connect = Connection.getInstance();
			List<Parameter> listParameter = new List<Parameter>();

			Parameter parameter1 = new Parameter();
			parameter1.field = IConstant.PARAMETER_MEDICINE;
			parameter1.valueObject = pRecipe.medicine.name;

			Parameter parameter2 = new Parameter();
			parameter2.field = IConstant.PARAMETER_MEASURE;
			parameter2.valueObject = pRecipe.medicine.measure;

			Parameter parameter3 = new Parameter();
			parameter3.field = IConstant.PARAMETER_EMAIL;
			parameter3.valueObject = user.email ;

			Parameter parameter4 = new Parameter();
			parameter4.field = IConstant.PARAMETER_START_DATE;
			parameter4.valueObject = pRecipe.startDate;

			Parameter parameter5 = new Parameter();
			parameter5.field = IConstant.PARAMETER_FREQUENCY;
			parameter5.valueObject = pRecipe.frequency;

			Parameter parameter6 = new Parameter();
			parameter6.field = IConstant.PARAMETER_QUANTITY;
			parameter6.valueObject = pRecipe.quantity;

			listParameter.Add(parameter1);
			listParameter.Add(parameter2);
			listParameter.Add(parameter3);
			listParameter.Add(parameter4);
			listParameter.Add(parameter5);
			listParameter.Add(parameter6);
			listMedicine.Add(pRecipe);
			connect.request(IConstant.PROCEDURE_INSERT_MEDICINE, listParameter);
			return true;
		}

		public override Boolean updateMedicine(Recipe pRecipe)
		{
			foreach (Recipe recipe in listMedicine)
			{
				if (pRecipe.medicine.name == recipe.medicine.name)
				{
					listMedicine.Remove(recipe);
					listMedicine.Add(pRecipe);

					Connection connect = Connection.getInstance();
					List<Parameter> listParameter = new List<Parameter>();

					Parameter parameter1 = new Parameter();
					parameter1.field = IConstant.PARAMETER_EMAIL;
					parameter1.valueObject = user.email;

					Parameter parameter2 = new Parameter();
					parameter2.field = IConstant.PARAMETER_MEDICINE;
					parameter2.valueObject = pRecipe.medicine.name;

					Parameter parameter3 = new Parameter();
					parameter3.field = "@pNewQuantity";
					parameter3.valueObject = pRecipe.quantity;

					Parameter parameter4 = new Parameter();
					parameter4.field = "@pNewFrequency";
					parameter4.valueObject = pRecipe.frequency;

					Parameter parameter5 = new Parameter();
					parameter5.field = "@pNewStartDate";
					parameter5.valueObject = pRecipe.startDate;

					listParameter.Add(parameter1);
					listParameter.Add(parameter2);
					listParameter.Add(parameter3);
					listParameter.Add(parameter4);
					listParameter.Add(parameter5);
					listMedicine.Add(pRecipe);
					connect.request("updateMedication", listParameter);
					return true;
				}
			}
			return false;
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
			List<Recipe> listRecipe = new List<Recipe>();
			while (reader.Read())
			{
				Recipe recipe = new Recipe();
				Medicine medicine = new Medicine();
				medicine.name = reader.GetString("name");
				medicine.measure = reader.GetInt16("measure");
				recipe.medicine = medicine;

				recipe.quantity = reader.GetInt16("quantity");
				recipe.frequency = reader.GetInt16("frequency");
				recipe.startDate = reader.GetDateTime("startDate");
				recipe.endDate = reader.GetDateTime("endDate");
				listRecipe.Add(recipe);
			}
			this.listMedicine = listRecipe;
			return true;
		}
	}
}