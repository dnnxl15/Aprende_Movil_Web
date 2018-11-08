using System;
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