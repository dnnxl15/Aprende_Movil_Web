using System;
using System.Collections.Generic;
using System.Data.SqlClient;
using System.Linq;
using System.Web;
using MySql.Data;
using MySql.Data.MySqlClient;

namespace Aprende_Movil.Domain
{
	public class Connection
	{
		private Connection connection { get; set; }
		private MySqlConnection mySqlConnect;

		private Connection()
		{
			connection = new MySqlConnection(Library.IDataInfo.CONNECTION);
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
	}
}