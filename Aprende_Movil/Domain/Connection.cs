using Aprende_Movil.Models;
using System;
using System.Collections.Generic;
using System.Data;
using System.Data.SqlClient;

namespace Aprende_Movil.Domain
{
	public class Connection
	{
		private Connection connection { get; set; }
		private SqlConnection mySqlConnect;

		private Connection()
		{
			connection = new SqlConnection(Library.IDataInfo.CONNECTION);
		}

		public Connection getInstance()
		{
			if(connection == null)
			{
				connection = new Connection();
			}
			return connection;
		}

		public SqlTransaction request(String pProcedure, List<Parameter> pParameters)
		{
			SqlCommand command = new SqlCommand(pProcedure, this.mySqlConnect);
			command.CommandType = CommandType.StoredProcedure;
			foreach (var parameter in pParameters)
			{
				command.Parameters.AddWithValue(parameter.field, parameter.value);          // Set the parameter
			}
			SqlTransaction trx = this.mySqlConnect.BeginTransaction(); // Begin the transaction
			try
			{
				command.Prepare();
			}
			catch (Exception e)
			{
			}
			command.Transaction = trx;
			command.ExecuteNonQuery();
			trx.Commit();
			return trx;
		}

		public bool OpenConnection()
		{
			try
			{
				mySqlConnect.Open();
				return true;
			}
			catch (SqlException ex)
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

		public bool CloseConnection()
		{
			try
			{
				mySqlConnect.Close();
				return true;
			}
			catch (SqlException ex)
			{
				return false;
			}
		}

		public static implicit operator Connection(SqlConnection v)
		{
			throw new NotImplementedException();
		}
	}
}