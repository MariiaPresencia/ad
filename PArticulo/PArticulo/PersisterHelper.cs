using System;
using System.Collections;
using System.Collections.Generic;
using System.Data;

namespace PArticulo{
	public class PersisterHelper{
		public static QueryResult Get(string selectText) {
			IDbConnection dbConnection = App.Instance.DbConnection;
			IDbCommand dbCommand = dbConnection.CreateCommand ();
			dbCommand.CommandText = selectText; 
			//TODO completar
			return null;
		}
	}
}

