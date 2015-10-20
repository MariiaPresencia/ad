using System;
using Gtk;
using System.Data;
using System.Collections;
using System.Collections.Generic;
using System.Data.Common;

namespace SerpisAd{
	public class PersisterHelper{
		public static QueryResult Get(string selectText) {
			IDbConnection dbConnection = App.Instance.DbConnection;
			IDbCommand dbCommand = dbConnection.CreateCommand ();
			dbCommand.CommandText = selectText;
			IDataReader dataReader = dbCommand.ExecuteReader ();
			//TODO completar

			QueryResult queryResult = new QueryResult ();
			queryResult.ColumnNames = getColumnNames (dataReader);
			List<IList> rows = new List<IList> ();
			while (dataReader.Read()) {
				IList row = getRows (dataReader);
				rows.Add (row);
			}

			queryResult.Rows = rows;
			dataReader.Close ();
			return queryResult;
		}
		private static string[] getColumnNames(IDataReader dataReader) {
			int count = dataReader.FieldCount;
			List<string> columNames = new List<string> ();
			for (int index = 0; index < count; index++)
				columNames.Add (dataReader.GetName (index));
			return columNames.ToArray ();
		}
		private static IList getRows(IDataReader dataReader){
			List<object> values = new List<object> ();
			int count = dataReader.FieldCount;
			for (int i = 0; i < count; i++) {
				values.Add(dataReader[i]);
			}
			return values;
		}
	}

}

