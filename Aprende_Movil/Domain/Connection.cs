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

		public MySqlDataReader request(String pProcedure, List<Parameter> pParameters)
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

			MySqlDataReader reader = null;
			try
			{
				command.Prepare();
				command.Transaction = trx;
				trx.Commit();
				reader = command.ExecuteReader();
			}
			catch (Exception e)
			{
			}

			return reader;
		}

		public MySqlParameter requestFunction(String pFunction, List<Parameter> pParameters)
		{
			if (mySqlConnect.State == ConnectionState.Closed)
			{
				mySqlConnect.Open();
			}
			MySqlCommand command = new MySqlCommand(pFunction, this.mySqlConnect);
			command.CommandType = CommandType.StoredProcedure;
			MySqlParameter myRetParam = new MySqlParameter();
			myRetParam.Direction = System.Data.ParameterDirection.ReturnValue;
			command.Parameters.Add(myRetParam);
			foreach (Parameter parameter in pParameters)
			{
				command.Parameters.AddWithValue(parameter.field, parameter.valueObject);          // Set the parameter
				Console.WriteLine(parameter.field);
				Console.WriteLine(parameter.valueObject);
			}
			MySqlTransaction trx = mySqlConnect.BeginTransaction(); // Begin the transaction

			MySqlDataReader reader = null;
			try
			{
				command.Prepare();
				command.Transaction = trx;
				trx.Commit();
				reader = command.ExecuteReader();
			}
			catch (Exception e)
			{
			}
			return myRetParam;
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