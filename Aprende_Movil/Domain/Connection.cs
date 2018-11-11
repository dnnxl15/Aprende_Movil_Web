using Aprende_Movil.Library;
using Aprende_Movil.Models;
using MySql.Data.MySqlClient;
using System;
using System.Collections.Generic;
using System.Data;
using System.Data.SqlClient;

namespace Aprende_Movil.Domain
{
	public class Connection
	{
		private static Connection connection { get; set; }
		private MySqlConnection mySqlConnect;

		private Connection()
		{
			mySqlConnect = new MySqlConnection(IDataInfo.CONNECTION);
		}

		public static Connection getInstance()
		{
			if (connection == null)
			{
				connection = new Connection();
			}
			return connection;
		}

		public MySqlTransaction request(String pProcedure, List<Parameter> pParameters)
		{
			if (mySqlConnect.State == ConnectionState.Closed)
			{
				mySqlConnect.Open();
			}
			MySqlCommand command = new MySqlCommand(pProcedure, this.mySqlConnect);
			command.CommandType = CommandType.StoredProcedure;
			foreach (Parameter parameter in pParameters)
			{
				command.Parameters.AddWithValue(parameter.field, parameter.valueObject);          // Set the parameter
				Console.WriteLine(parameter.field);
				Console.WriteLine(parameter.valueObject);
			}
			MySqlTransaction trx = mySqlConnect.BeginTransaction(); // Begin the transaction
			try
			{
				command.Prepare();
				command.Transaction = trx;
				command.ExecuteNonQuery();
			}
			catch (Exception e)
			{
			}

			trx.Commit();
			return trx;
		}

		public bool OpenConnection()
		{
			if(mySqlConnect.State == ConnectionState.Open)
			{
				return true;
			}
			else
			{
				try
				{
					mySqlConnect.Open();
					return true;
				}
				catch (MySqlException ex)
				{
					switch (ex.Number)
					{
						case 0:
							break;

						case 1045:
							break;
					}
					return false;
				}
			}
			
		}

		public bool CloseConnection()
		{
			try
			{
				mySqlConnect.Close();
				return true;
			}
			catch (MySqlException ex)
			{
				return false;
			}
		}

		public static implicit operator Connection(MySqlConnection v)
		{
			throw new NotImplementedException();
		}
	}
}