using System;
using System.Collections.Generic;
using System.Linq;
using System.Web;

namespace Aprende_Movil.Library
{
	public class IConstant
	{
		public const string PROCEDURE_INSERT_PAYMENT = "insertPaysByUser";
		public const string PARAMETER_FREQUENCY = "@pFrequency";
		public const string PARAMETER_NAME= "@pPayment";

		public const string PROCEDURE_GET_PAYMENT = "getPaymentsByUser";
		public const string PARAMETER_EMAIL = "@pEmail";

		public const string VALUE_NAME = "name";

		public const string PARAMETER_AMOUNT = "@pAmount";

		public const string PROCEDURE_INSERT_MEDICINE = "insertMedication"; 
		public const string PARAMETER_QUANTITY = "@pQuantity";
		public const string PARAMETER_START_DATE = "@pStartDate";
		public const string PARAMETER_MEDICINE = "@pMedicine";
		public const string PARAMETER_MEASURE = "@pTypeOfMeasure";
		public const string PROCEDURE_GET_MEDICINE = "getMeticationByUser";

	}
}